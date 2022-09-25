using AForge.Video.DirectShow;
using SMAFRO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMAFRO.Controllers
{
    public class CameraController
    {
        public HashSet<string> cameras = new HashSet<string>();
        public delegate void NextFrameUpdateHandler(object sender, NextFrameEventArgs e);

        public event NextFrameUpdateHandler OnNextFrame;
        public delegate void CameraAddedHandler(object sender, CameraAddedEventArgs e);

        public event CameraAddedHandler OnCameraAdded;
        public CameraController() { 
        
        }

        public void loadSystemCameras() {
            var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (var i = 0; i < filterInfoCollection.Count; i++)
            {
                FilterInfo Device = filterInfoCollection[i];
                string cameraId = filterInfoCollection[i].MonikerString;
                var args = new CameraAddedEventArgs(Device.Name);
                OnCameraAdded(this, args);
                loadCamera(cameraId, Device.Name);

            }
        }

        private void loadCamera(string cameraId, string deviceName)
        {
            this.cameras.Add(cameraId);
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
        }

        private void updateFrame(string deviceId, Bitmap frame)
        {
            NextFrameEventArgs args = new NextFrameEventArgs(deviceId, frame);
            OnNextFrame(this, args);
        }
    }
}
