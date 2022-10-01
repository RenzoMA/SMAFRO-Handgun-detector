using AForge.Video.DirectShow;
using SMAFRO.Models;

namespace SMAFRO.Controllers
{
    public class CameraController
    {
        public HashSet<SmafroCam> cameras = new HashSet<SmafroCam>();
        public delegate void NextFrameUpdateHandler(object sender, NextFrameEventArgs e);

        public event NextFrameUpdateHandler OnNextFrame;
        public delegate void CameraAddedHandler(object sender, CameraAddedEventArgs e);

        public event CameraAddedHandler OnCameraAdded;
        
        public CameraController() { }

        public void loadSystemCameras(List<string> disbaledCamaras) {
            var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (var i = 0; i < filterInfoCollection.Count; i++)
            {
                if (!disbaledCamaras.Contains(filterInfoCollection[i].Name))
                {
                    FilterInfo Device = filterInfoCollection[i];
                    string cameraId = filterInfoCollection[i].MonikerString;
                    var args = new CameraAddedEventArgs(Device.Name);
                    OnCameraAdded(this, args);
                    loadCamera(cameraId, Device.Name);
                }                
            }
        }

        private void loadCamera(string cameraId, string deviceName)
        {
            startCamera(cameraId, deviceName);
        }

        private void startCamera(string cameraId, string deviceName)
        {
            var smafroCam = new SmafroCam
            {
                VideoCaptureDevice = new VideoCaptureDevice(cameraId),
                DeviceName = deviceName
            };
            smafroCam.setupCamera();
            smafroCam.OnNextFrame += new SmafroCam.NextFrameUpdateHandler((sender, e) => updateFrame(e.DeviceName, e.Frame));
            this.cameras.Add(smafroCam);
        }

        public void switchCameraMode(string cameraName, bool scoped) {
            var cam = this.cameras.First((cam) => cam.DeviceName == cameraName);
            cam?.switchMode(scoped);
        }

        private void updateFrame(string deviceId, Bitmap frame)
        {
            NextFrameEventArgs args = new NextFrameEventArgs(deviceId, frame);
            OnNextFrame(this, args);
        }
    }
}
