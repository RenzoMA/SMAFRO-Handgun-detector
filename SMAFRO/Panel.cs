using Emgu.CV;
using Emgu.CV.Structure;
using DarknetYolo;
using DarknetYolo.Models;
using System.Diagnostics;
using AForge.Video.DirectShow;
using AForge.Video;
using SMAFRO.Models;

namespace SMAFRO
{
    public partial class Panel : Form
    {
        Dictionary<string, PictureBox> camDictionary = new Dictionary<string, PictureBox>();
        public string currentCamera = "";
        public Panel()
        {
            InitializeComponent();
            setupCameras();
            setupCamClick();

            this.setCameraDimensions(2);
        }
        public void setupCamClick() {
            foreach (var entry in camDictionary) {
                string cameraName = entry.Key;
                PictureBox pictureBox = entry.Value;
                pictureBox.DoubleClick += new EventHandler((s, e) => this.switchZoom(cameraName));
            }
        }

        public void switchZoom(string deviceName) {
            if (currentCamera == deviceName)
            {
                zoomOut();
                this.currentCamera = "";
            }
            else {
                zoomIn(deviceName);
                this.currentCamera = deviceName;
            }
            
        }
        public void zoomOut() {
            this.clearPanels();
            var cams = this.camDictionary.ToList().Select(cam => cam.Value).ToArray();
            this.flowLayoutPanel1.Controls.AddRange(cams);
            this.setCameraDimensions(2);
        }

        public void zoomIn(string deviceName) {
            var pictureBox = camDictionary.GetValueOrDefault(deviceName);
            var rest = camDictionary.ToList().Where(cam => cam.Value != pictureBox).Select(cam => cam.Value).ToArray();

            this.clearPanels();
            this.flowLayoutPanel1.Controls.Add(pictureBox);
            addToMainView(new PictureBox[] { pictureBox});
            setCameraDimensions(1);
            addToMiniView(rest);
        }

        private void addToMainView(PictureBox[] cams) {
            this.flowLayoutPanel1.Controls.AddRange(cams);
        }

        private void addToMiniView(PictureBox[] cams) {
            cams.ToList().ForEach(cam => {
                cam.Width = this.flowLayoutPanel2.Width - 10;
                cam.Height = cam.Width / 2;
            });
            this.flowLayoutPanel2.Controls.AddRange(cams);
        }

        public void clearPanels() {
            this.flowLayoutPanel2.Controls.Clear();
            this.flowLayoutPanel1.Controls.Clear();
        }

        public void setupCameras() {
            var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (var i = 0; i < filterInfoCollection.Count; i++)
            {
                FilterInfo Device = filterInfoCollection[i];
                var cameraId = filterInfoCollection[i].MonikerString;
                loadCamera(cameraId, Device.Name);
            }
        }

        private void loadCamera(string cameraId, string deviceName)
        {
            var picture = AddCameraControl(deviceName);
            startCamera(cameraId, deviceName);
            camDictionary.Add(deviceName, picture);
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

        private void updateFrame(string cameraName, Bitmap frame) {
            var pictureBox = camDictionary.GetValueOrDefault(cameraName);
            pictureBox.Image = frame;
        }

        PictureBox AddCameraControl(string deviceName) {
            var picture = new PictureBox
            {
                Name = "pictureBox",
                BackColor = Color.Gray,
                SizeMode = PictureBoxSizeMode.StretchImage

            };
            var title = new Label
            {
                Text = deviceName,
                AutoSize = true
            };
            picture.Controls.Add(title);
            this.flowLayoutPanel1.Controls.Add(picture);
            return picture;
        }

        private void setCameraDimensions(int camPerRow) {
            foreach (var control in this.flowLayoutPanel1.Controls) {
                if (control is PictureBox) {
                    var pictureBox = control as PictureBox;
                    pictureBox.Width = (this.flowLayoutPanel1.Width / camPerRow) - 10;
                    pictureBox.Height = pictureBox.Width / 2;
                }
            }
        }
    }
}