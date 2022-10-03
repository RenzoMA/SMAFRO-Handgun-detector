using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV.Structure;
using SMAFRO.Detector;

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
        public bool Scoped { get; set; }

        public NewFrameEventHandler handler;

        private HandgunDetector HandgunDetector = new HandgunDetector(75, new MCvScalar(255, 0, 0), new MCvScalar(0, 0, 255));
        public SmafroCam() { 
            handler = new NewFrameEventHandler((sender, e) => NewScopedFrameEvent(sender, e, this.DeviceName));
        }
        public void setupCamera() {

            VideoCaptureDevice.NewFrame += handler;
            VideoCaptureDevice.Start();
        }

        public void switchMode(bool scoped) {
            this.Scoped = scoped;
            if (this.Scoped)
            {
                VideoCaptureDevice.NewFrame -= handler;
                handler = new NewFrameEventHandler((sender, e) => NewScopedFrameEvent(sender, e, DeviceName));
                VideoCaptureDevice.NewFrame += handler;
            }
            else {
                VideoCaptureDevice.NewFrame -= handler;
                handler = new NewFrameEventHandler((sender, e) => NewFrameEvent(sender, e, DeviceName));
                VideoCaptureDevice.NewFrame += handler;
            }
        }

        private void NewScopedFrameEvent(object sender, NewFrameEventArgs eventArgs, string deviceName)
        {
            var nextFrame = HandgunDetector.Predict((Bitmap)eventArgs.Frame.Clone());
            NextFrameEventArgs args = new NextFrameEventArgs(deviceName, nextFrame);
            OnNextFrame(this, args);
        }

        private void NewFrameEvent(object sender, NewFrameEventArgs eventArgs, string deviceName)
        {
            var nextFrame = HandgunDetector.Predict((Bitmap)eventArgs.Frame.Clone(), false);
            NextFrameEventArgs args = new NextFrameEventArgs(deviceName, nextFrame);
            OnNextFrame(this, args);
        }

        public void stopCamera() {
            this.VideoCaptureDevice.SignalToStop();
        }
    }
}
