namespace SMAFRO
{
    partial class Panel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.miniViewContainer = new System.Windows.Forms.GroupBox();
            this.flpMiniVista = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flpCamaraPrincipal = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGuardarCamarasActivas = new System.Windows.Forms.Button();
            this.flpGestionCamaras = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.miniViewContainer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // miniViewContainer
            // 
            this.miniViewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.miniViewContainer.Controls.Add(this.flpMiniVista);
            this.miniViewContainer.Location = new System.Drawing.Point(1112, 6);
            this.miniViewContainer.Name = "miniViewContainer";
            this.miniViewContainer.Size = new System.Drawing.Size(217, 971);
            this.miniViewContainer.TabIndex = 0;
            this.miniViewContainer.TabStop = false;
            this.miniViewContainer.Text = "Mini View";
            // 
            // flpMiniVista
            // 
            this.flpMiniVista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMiniVista.AutoScroll = true;
            this.flpMiniVista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.flpMiniVista.Location = new System.Drawing.Point(6, 22);
            this.flpMiniVista.Name = "flpMiniVista";
            this.flpMiniVista.Size = new System.Drawing.Size(205, 943);
            this.flpMiniVista.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flpCamaraPrincipal);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1100, 665);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Panel principal camaras";
            // 
            // flpCamaraPrincipal
            // 
            this.flpCamaraPrincipal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flpCamaraPrincipal.AutoScroll = true;
            this.flpCamaraPrincipal.BackColor = System.Drawing.Color.Gray;
            this.flpCamaraPrincipal.Location = new System.Drawing.Point(12, 22);
            this.flpCamaraPrincipal.Name = "flpCamaraPrincipal";
            this.flpCamaraPrincipal.Size = new System.Drawing.Size(1082, 637);
            this.flpCamaraPrincipal.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1326, 705);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.miniViewContainer);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1318, 677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Panel principal camaras";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1318, 677);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gestion camaras activas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGuardarCamarasActivas);
            this.groupBox1.Controls.Add(this.flpGestionCamaras);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1306, 665);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gestion de camaras activas";
            // 
            // btnGuardarCamarasActivas
            // 
            this.btnGuardarCamarasActivas.Location = new System.Drawing.Point(518, 599);
            this.btnGuardarCamarasActivas.Name = "btnGuardarCamarasActivas";
            this.btnGuardarCamarasActivas.Size = new System.Drawing.Size(177, 23);
            this.btnGuardarCamarasActivas.TabIndex = 1;
            this.btnGuardarCamarasActivas.Text = "Guardar Configuracion";
            this.btnGuardarCamarasActivas.UseVisualStyleBackColor = true;
            this.btnGuardarCamarasActivas.Click += new System.EventHandler(this.btnGuardarCamarasActivas_Click);
            // 
            // flpGestionCamaras
            // 
            this.flpGestionCamaras.BackColor = System.Drawing.Color.Gray;
            this.flpGestionCamaras.Location = new System.Drawing.Point(6, 22);
            this.flpGestionCamaras.Name = "flpGestionCamaras";
            this.flpGestionCamaras.Size = new System.Drawing.Size(1294, 549);
            this.flpGestionCamaras.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(1234, 12);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(94, 23);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "Cerrar Sesion";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1350, 759);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.tabControl1);
            this.Name = "Panel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMAFRO";
            this.miniViewContainer.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox miniViewContainer;
        private FlowLayoutPanel flpMiniVista;
        private GroupBox groupBox2;
        private FlowLayoutPanel flpCamaraPrincipal;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private FlowLayoutPanel flpGestionCamaras;
        private Button btnGuardarCamarasActivas;
        private Button btnCerrarSesion;
    }
}