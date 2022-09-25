using AForge.Video;
using AForge.Video.DirectShow;
using DarknetYolo;
using DarknetYolo.Models;
using Emgu.CV;
using Emgu.CV.Structure;
using SMAFRO.Detector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Control;

namespace SMAFRO.Models
{
    public class CameraAddedEventArgs : EventArgs
    {
        public string DeviceName { get; private set; }

        public CameraAddedEventArgs( string deviceName)
        {
            DeviceName = deviceName;
        }
    }
    public class NextFrameEventArgs : EventArgs
    {
        public string DeviceName { get; private set; }
        public Bitmap Frame { get; private set; }

        public NextFrameEventArgs(string deviceName, Bitmap frame)
        {
            DeviceName = deviceName;
            Frame = frame;
        }
    }
    public class SmafroCam
    {
        public delegate void NextFrameUpdateHandler(object sender, NextFrameEventArgs e);

        public event NextFrameUpdateHandler OnNextFrame;
        public VideoCaptureDevice VideoCaptureDevice { get; set; }
        public string DeviceName { get; set; }

        private HandgunDetector HandgunDetector = new HandgunDetector(75, new MCvScalar(255, 0, 0), new MCvScalar(0, 0, 255));

        public void setupCamera() {

            VideoCaptureDevice.NewFrame += new NewFrameEventHandler((sender, e) => NewFrameEvent(sender, e, DeviceName));
            VideoCaptureDevice.Start();
        }

        private void NewFrameEvent(object sender, NewFrameEventArgs eventArgs, string deviceName)
        {
            var nextFrame = HandgunDetector.Predict((Bitmap)eventArgs.Frame.Clone());
            NextFrameEventArgs args = new NextFrameEventArgs(deviceName, nextFrame);
            OnNextFrame(this, args);
        }

        public void stopCamera() {
            this.VideoCaptureDevice.Stop();
        }
    }
}
