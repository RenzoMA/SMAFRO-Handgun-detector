using SMAFRO.Controllers;

namespace SMAFRO
{
    public partial class Panel : Form
    {
        Dictionary<string, PictureBox> availableCameras = new Dictionary<string, PictureBox>();
        List<string> disbaledCamaras = new List<string>();
        public string currentCamera = "";
        CameraController cameraController;

        public Panel()
        {
            InitializeComponent();
            LoadMainPanel();
            LoadManagementPanel();
        }

        private void LoadMainPanel()
        {
            cameraController = new CameraController();
            cameraController.OnNextFrame += new CameraController.NextFrameUpdateHandler((sender, e) => updateFrame(e.DeviceName, e.Frame));
            cameraController.OnCameraAdded += new CameraController.CameraAddedHandler((sender, e) => bindingControl(e.DeviceName));
            cameraController.loadSystemCameras(disbaledCamaras);
            bindingSwitchZoomEventToCamara();
            setCameraDimensions(4);
        }

        public void bindingSwitchZoomEventToCamara() 
        {
            foreach (var camara in availableCameras) 
            {
                string cameraName = camara.Key;
                PictureBox pictureBox = camara.Value;
                pictureBox.DoubleClick += new EventHandler((s, e) => this.switchZoom(cameraName));
            }
        }

        private void LoadManagementPanel()
        {
            var checkboxesForPanel = CamaraNamesInChecBoxes();

            foreach (var checkbox in checkboxesForPanel)
            {
                this.flpGestionCamaras.Controls.Add(checkbox);
            }
        }

        public void switchZoom(string deviceName) 
        {
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

        public void bindingControl(string cameraId) 
        {
            var control = AddCameraControl(cameraId);
            availableCameras.Add(cameraId, control);
        }

        private void updateFrame(string cameraId, Bitmap frame)
        {
            var pictureBox = availableCameras.GetValueOrDefault(cameraId);
            if (pictureBox != null) pictureBox.Image = frame;
        }

        public void zoomOut() 
        {
            this.clearPanels();
            var cams = this.availableCameras.ToList().Select(cam => cam.Value).ToArray();
            cameraController.cameras.ToList().ForEach(camera =>camera.switchMode(true));
            this.flpCamaraPrincipal.Controls.AddRange(cams);
            this.setCameraDimensions(2);
        }

        public void zoomIn(string deviceName) 
        {
            var pictureBox = availableCameras.GetValueOrDefault(deviceName);
            var rest = availableCameras.ToList().Where(cam => cam.Value != pictureBox).Select(cam => cam.Value).ToArray();
            cameraController.cameras.Where(cam => cam.DeviceName != deviceName).ToList().ForEach(camera => camera.switchMode(false));
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
                cam.Width = this.flpMiniVista.Width - 10;
                cam.Height = cam.Width / 2;
            });
            this.flpMiniVista.Controls.AddRange(cams);
        }

        public void clearPanels() {
            this.flpMiniVista.Controls.Clear();
            this.flpCamaraPrincipal.Controls.Clear();
        }

        PictureBox AddCameraControl(string deviceName) {
            var picture = new PictureBox
            {
                Name = deviceName,
                BackColor = Color.Gray,
                SizeMode = PictureBoxSizeMode.StretchImage

            };
            var title = new Label
            {
                Text = deviceName,
                AutoSize = true
            };

            picture.Controls.Add(title);

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

        private List<CheckBox> CamaraNamesInChecBoxes()
        {
            var generatedCheckboxes = new List<CheckBox>();

            var valuesForCheckboxes = new Dictionary<int, string>();

            int count = 1;
            foreach (var camara in availableCameras)
            {
                valuesForCheckboxes.Add(count, camara.Value.Name);
                count++;
            }

            if (valuesForCheckboxes != null && valuesForCheckboxes.Count != 0)
            {
                int index = 0;
                foreach (var chbx in valuesForCheckboxes)
                {
                    var checkboxToAdd = new System.Windows.Forms.CheckBox();

                    checkboxToAdd.AutoSize = true;
                    checkboxToAdd.Enabled = true;
                    checkboxToAdd.Checked = true;
                    checkboxToAdd.CheckState = System.Windows.Forms.CheckState.Checked;
                    checkboxToAdd.Size = new System.Drawing.Size(84, 21);
                    checkboxToAdd.UseVisualStyleBackColor = true;

                    checkboxToAdd.Name = "chboxCountry" + chbx.Key;
                    checkboxToAdd.Text = chbx.Value;
                    checkboxToAdd.TabIndex = index + 1;

                    index++;

                    generatedCheckboxes.Add(checkboxToAdd);
                }
            }

            return generatedCheckboxes;
        }

        private void btnGuardarCamarasActivas_Click(object sender, EventArgs e)
        {
            disbaledCamaras = new List<string>();

            foreach (var checkBox in flpGestionCamaras.Controls)
            {
                var converted = (CheckBox)checkBox;
                if (!converted.Checked) disbaledCamaras.Add(converted.Text);
            }

            availableCameras = new Dictionary<string, PictureBox>();
            clearPanels();
            cameraController.StopAllCamaras();
            LoadMainPanel();
            Task.Delay(2);
            MessageBox.Show("Cambios efectuados correctamente", "SMAFRO");
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            cameraController.StopAllCamaras();
            availableCameras = new Dictionary<string, PictureBox>();
            disbaledCamaras = new List<string>();
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraController.StopAllCamaras();
        }
    }
}