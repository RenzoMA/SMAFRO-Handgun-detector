namespace SMAFRO
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pbSingIn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSingIn)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSingIn
            // 
            this.pbSingIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbSingIn.BackgroundImage")));
            this.pbSingIn.Location = new System.Drawing.Point(172, 157);
            this.pbSingIn.Name = "pbSingIn";
            this.pbSingIn.Size = new System.Drawing.Size(414, 94);
            this.pbSingIn.TabIndex = 4;
            this.pbSingIn.TabStop = false;
            this.pbSingIn.Click += new System.EventHandler(this.pbSingIn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbSingIn);
            this.Name = "Login";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pbSingIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox pbSingIn;
    }
}