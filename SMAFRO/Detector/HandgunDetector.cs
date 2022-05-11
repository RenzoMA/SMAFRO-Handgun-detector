using DarknetYolo;
using DarknetYolo.Models;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SMAFRO.Detector
{
    public class HandgunDetector
    {
        public int Threshold { get; private set; }
        public MCvScalar MediumConfidenceColor { get; private set; }
        public MCvScalar HighConfidenceColor { get; private set; }

        DarknetYOLO model;
        public HandgunDetector(int threshold, MCvScalar mediumConfidenceColor, MCvScalar highConfidenceColor) {
            string labels = @"..\..\..\NetworkModels\coco.names";
            string weights = @"..\..\..\NetworkModels\yolov4-custom_best.weights";
            string cfg = @"..\..\..\NetworkModels\yolov4-custom.cfg";


            model = new DarknetYOLO(labels, weights, cfg, PreferredBackend.Cuda, PreferredTarget.Cuda);
            model.NMSThreshold = 0.4f;
            model.ConfidenceThreshold = 0.5f;
            this.Threshold = threshold;
            this.MediumConfidenceColor = mediumConfidenceColor;
            this.HighConfidenceColor = highConfidenceColor;
        }

        public Bitmap Predict(Bitmap bitmap, bool scoped = true)
        {
            var image = bitmap.ToImage<Bgr, Byte>();
            Mat frame = image.Mat;
            List<YoloPrediction> results = model.Predict(frame.ToBitmap(), 416, 416);
            foreach (var item in results)
            {
                var confidence = item.Confidence * 100;
                var color = confidence < this.Threshold ? this.MediumConfidenceColor : this.HighConfidenceColor;
                if (scoped)
                {
                    string text = item.Label + " " + confidence.ToString("N0") + "%";
                    CvInvoke.Rectangle(frame, new Rectangle(item.Rectangle.X, item.Rectangle.Y - 20, text.Length * 12, 20), color, -1);
                    CvInvoke.PutText(frame, text, new Point(item.Rectangle.X, item.Rectangle.Y - 5), Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(255, 255, 255), 2);
                    CvInvoke.Rectangle(frame, item.Rectangle, color, 2);
                }
                else
                {
                    CvInvoke.Rectangle(frame, new Rectangle(0, 0, frame.Width, frame.Height), color, 200);
                }
                
            }
            return frame.ToBitmap();
        }

    }
}
