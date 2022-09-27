using Emgu.CV;
using Emgu.CV.Structure;
using DarknetYolo;
using DarknetYolo.Models;
using System.Diagnostics;
using AForge.Video.DirectShow;
using AForge.Video;
using SMAFRO.Models;
using SMAFRO.Controllers;

namespace SMAFRO
{
    public partial class Panel : Form
    {
        Dictionary<string, PictureBox> camDictionary = new Dictionary<string, PictureBox>();
        public string currentCamera = "";
        CameraController cameraController;
        public Panel()
        {
            InitializeComponent();
            cameraController = new CameraController();
            cameraController.OnNextFrame += new CameraController.NextFrameUpdateHandler((sender, e) => updateFrame(e.DeviceName, e.Frame));
            cameraController.OnCameraAdded += new CameraController.CameraAddedHandler((sender, e) => test(e.DeviceName));
            cameraController.loadSystemCameras();
            setupCamClick();
            setCameraDimensions(4);
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
                this.cameraController.switchCameraMode(this.currentCamera, true);
            }
            
        }

        public void test(string cameraId) {
            var control = AddCameraControl(cameraId);
            camDictionary.Add(cameraId, control);
        }
        public void zoomOut() {
            this.clearPanels();
            var cams = this.camDictionary.ToList().Select(cam => cam.Value).ToArray();
            cameraController.cameras.ToList().ForEach(camera =>
            camera.switchMode(true));
            this.mainVideoPanel.Controls.AddRange(cams);
            this.setCameraDimensions(2);
        }

        public void zoomIn(string deviceName) {
            var pictureBox = camDictionary.GetValueOrDefault(deviceName);
            var rest = camDictionary.ToList().Where(cam => cam.Value != pictureBox).Select(cam => cam.Value).ToArray();
            cameraController.cameras.Where(cam => cam.DeviceName != deviceName).ToList().ForEach(camera => 
            camera.switchMode(false));
            this.clearPanels();
            this.mainVideoPanel.Controls.Add(pictureBox);
            addToMainView(new PictureBox[] { pictureBox});
            setCameraDimensions(1);
            addToMiniView(rest);
        }

        private void addToMainView(PictureBox[] cams) {
            this.mainVideoPanel.Controls.AddRange(cams);
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
            this.mainVideoPanel.Controls.Clear();
        }

        private void updateFrame(string cameraId, Bitmap frame) {
            var pictureBox = camDictionary.GetValueOrDefault(cameraId);
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
            this.mainVideoPanel.Controls.Add(picture);
            return picture;
        }

        private void setCameraDimensions(int camPerRow) {
            foreach (var control in this.mainVideoPanel.Controls) {
                if (control is PictureBox) {
                    var pictureBox = control as PictureBox;
                    pictureBox.Width = (this.mainVideoPanel.Width / camPerRow) - 10;
                    pictureBox.Height = pictureBox.Width / 2;
                }
            }
        }
    }
}