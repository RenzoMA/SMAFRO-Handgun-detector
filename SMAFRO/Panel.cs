using SMAFRO.Controllers;

namespace SMAFRO
{
    public partial class Panel : Form
    {
        //Dictionary<string, PictureBox> allCameras = new Dictionary<string, PictureBox>();
        Dictionary<string, PictureBox> availableCameras = new Dictionary<string, PictureBox>();
        public string currentCamera = "";
        CameraController cameraController;

        public Panel()
        {
            InitializeComponent();
            cameraController = new CameraController();
            cameraController.OnNextFrame += new CameraController.NextFrameUpdateHandler((sender, e) => updateFrame(e.DeviceName, e.Frame));
            cameraController.OnCameraAdded += new CameraController.CameraAddedHandler((sender, e) => bindingControl(e.DeviceName));
            cameraController.loadSystemCameras();
            bindingSwitchZoomEventToCamara();
            setCameraDimensions(4);
        }

        public void bindingSwitchZoomEventToCamara() {
            foreach (var camara in availableCameras) {
                string cameraName = camara.Key;
                PictureBox pictureBox = camara.Value;
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

        public void bindingControl(string cameraId) {
            var control = AddCameraControl(cameraId);
            //allCameras.Add(cameraId, control);
            availableCameras.Add(cameraId, control);
        }
        public void zoomOut() {
            this.clearPanels();
            var cams = this.availableCameras.ToList().Select(cam => cam.Value).ToArray();
            cameraController.cameras.ToList().ForEach(camera =>
            camera.switchMode(true));
            this.flpCamaraPrincipal.Controls.AddRange(cams);
            this.setCameraDimensions(2);
        }

        public void zoomIn(string deviceName) {
            var pictureBox = availableCameras.GetValueOrDefault(deviceName);
            var rest = availableCameras.ToList().Where(cam => cam.Value != pictureBox).Select(cam => cam.Value).ToArray();
            cameraController.cameras.Where(cam => cam.DeviceName != deviceName).ToList().ForEach(camera => 
            camera.switchMode(false));
            this.clearPanels();
            this.flpCamaraPrincipal.Controls.Add(pictureBox);
            addToMainView(new PictureBox[] { pictureBox});
            setCameraDimensions(1);
            addToMiniView(rest);
        }

        private void addToMainView(PictureBox[] cams) {
            this.flpCamaraPrincipal.Controls.AddRange(cams);
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
            this.flpCamaraPrincipal.Controls.Clear();
        }

        private void updateFrame(string cameraId, Bitmap frame) {
            var pictureBox = availableCameras.GetValueOrDefault(cameraId);
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

            this.flpGestionCamaras.Controls.Add(picture);
            this.flpCamaraPrincipal.Controls.Add(picture);
            
            return picture;
        }

        private void setCameraDimensions(int camPerRow) {
            foreach (var control in this.flpCamaraPrincipal.Controls) {
                if (control is PictureBox) {
                    var pictureBox = control as PictureBox;
                    pictureBox.Width = (this.flpCamaraPrincipal.Width / camPerRow) - 10;
                    pictureBox.Height = pictureBox.Width / 2;
                }
            }
        }
    }
}