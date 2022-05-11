using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Dnn;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using DarknetYolo;
using DarknetYolo.Models;
using System.Timers;

namespace YOLOv4_TEST
{
    class Program
    {
        public static bool saveEnabled = false;
        public static void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            saveEnabled = false;
        }
        static void Main(string[] args)
        {
            string labels = @"..\..\NetworkModels\coco.names";
            string weights = @"..\..\NetworkModels\yolov4-custom_best.weights";
            string cfg = @"..\..\NetworkModels\yolov4-custom.cfg";
            string video = @"..\..\Resources\robbery.mp4";

            VideoCapture cap = new VideoCapture(video);
            Console.WriteLine("[INFO] Loading Model...");
            DarknetYOLO model = new DarknetYOLO(labels, weights, cfg, PreferredBackend.Cuda, PreferredTarget.Cuda);
            model.NMSThreshold = 0.4f;
            model.ConfidenceThreshold = 0.5f;

            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            myTimer.Interval = 500;
            myTimer.Start();


            while (true)
            {
                Mat frame = new Mat();
                try
                {
                    cap.Read(frame);
                    CvInvoke.Resize(frame, frame, new Size(160, 120));
                }
                catch (Exception e)
                {
                    Console.WriteLine("VideoEnded");
                    frame = null;
                }
                if (frame == null)
                    break;
                Stopwatch watch = new Stopwatch();
                watch.Start();
                List<YoloPrediction> results = model.Predict(frame.ToBitmap(), 416, 416);

                watch.Stop();
                Console.WriteLine($"Frame Processing time: {watch.ElapsedMilliseconds} ms." + $" FPS: {1000f / watch.ElapsedMilliseconds}");
                var lowConfidence = results.Where(item => item.Confidence * 100 < 60).ToList();
                if (lowConfidence.Count > 0 && saveEnabled) 
                {
                    string folder = "frames5";
                    string id = $"{DateTime.Now.Ticks}-{Guid.NewGuid()}";
                    bool shouldSave = true;
                    var maxPecentage = 0.70;
                    var maxWidth = (double)(frame.Width * maxPecentage);
                    var maxHeight = (double)(frame.Height * maxPecentage);
                    foreach (var item in lowConfidence) {
                        var elementWidth = item.Rectangle.Width;
                        var elementHeight = item.Rectangle.Height;
                        if (elementWidth > maxWidth || elementHeight > maxHeight)
                        {
                            shouldSave = false;
                        }
                    }
                    if (shouldSave) 
                    {
                        using (StreamWriter sw = File.CreateText($"C:\\{folder}\\{id}.txt"))
                        {
                            foreach (var item in lowConfidence)
                            {

                                string line = string.Format("{0} {1} {2} {3} {4}", "0", (double)(item.Rectangle.X + (double)item.Rectangle.Width / 2) / frame.Width, (double)(item.Rectangle.Y + (double)item.Rectangle.Height / 2) / frame.Height, (double)item.Rectangle.Width / frame.Width, (double)item.Rectangle.Height / frame.Height);
                                sw.WriteLine(line);

                            }
                        }
                        frame.Save($"C:\\{folder}\\{id}.jpg");
                    }
                    
                    saveEnabled = false;
                }

                foreach (var item in results)
                {
                    string text = item.Label + " " + (item.Confidence * 100) + "%";
                    CvInvoke.Rectangle(frame, new Rectangle(item.Rectangle.X, item.Rectangle.Y - 20, text.Length * 12, 20), new MCvScalar(255, 0, 0), -1);
                    CvInvoke.PutText(frame, text, new Point(item.Rectangle.X, item.Rectangle.Y - 5), Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(255, 255, 255), 2);
                    CvInvoke.Rectangle(frame, item.Rectangle, new MCvScalar(255, 0, 0), 2);
                }
                CvInvoke.Imshow("test", frame);
                CvInvoke.WaitKey(1);


            }
        }
    }
}
