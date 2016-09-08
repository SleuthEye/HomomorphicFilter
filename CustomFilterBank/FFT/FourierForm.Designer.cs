namespace CustomFilterBank_Test
{
    partial class FourierForm
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
            this.inputImagePictureBox = new System.Windows.Forms.PictureBox();
            this.fourierMagnitudePictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fourierPhasePictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inverseFftPictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.convolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.inputImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierMagnitudePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierPhasePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inverseFftPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputImagePictureBox
            // 
            this.inputImagePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.inputImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inputImagePictureBox.Location = new System.Drawing.Point(12, 69);
            this.inputImagePictureBox.Name = "inputImagePictureBox";
            this.inputImagePictureBox.Size = new System.Drawing.Size(256, 256);
            this.inputImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.inputImagePictureBox.TabIndex = 17;
            this.inputImagePictureBox.TabStop = false;
            // 
            // fourierMagnitudePictureBox
            // 
            this.fourierMagnitudePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.fourierMagnitudePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fourierMagnitudePictureBox.Location = new System.Drawing.Point(274, 69);
            this.fourierMagnitudePictureBox.Name = "fourierMagnitudePictureBox";
            this.fourierMagnitudePictureBox.Size = new System.Drawing.Size(256, 256);
            this.fourierMagnitudePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fourierMagnitudePictureBox.TabIndex = 18;
            this.fourierMagnitudePictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Selected edge";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "GrayscaleImageComplex Magnitude Plot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "GrayscaleImageComplex Phase Plot";
            // 
            // fourierPhasePictureBox
            // 
            this.fourierPhasePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.fourierPhasePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fourierPhasePictureBox.Location = new System.Drawing.Point(536, 69);
            this.fourierPhasePictureBox.Name = "fourierPhasePictureBox";
            this.fourierPhasePictureBox.Size = new System.Drawing.Size(256, 256);
            this.fourierPhasePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fourierPhasePictureBox.TabIndex = 21;
            this.fourierPhasePictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(795, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Inverse GrayscaleImageComplex  Plot";
            // 
            // inverseFftPictureBox
            // 
            this.inverseFftPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.inverseFftPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inverseFftPictureBox.Location = new System.Drawing.Point(798, 69);
            this.inverseFftPictureBox.Name = "inverseFftPictureBox";
            this.inverseFftPictureBox.Size = new System.Drawing.Size(256, 256);
            this.inverseFftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.inverseFftPictureBox.TabIndex = 23;
            this.inverseFftPictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 34);
            this.button1.TabIndex = 25;
            this.button1.Text = "Forward FFT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.fftButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(798, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(256, 34);
            this.button2.TabIndex = 26;
            this.button2.Text = "Inverse FFT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.iFftButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(12, 331);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(256, 34);
            this.loadButton.TabIndex = 27;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convolutionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1068, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // convolutionToolStripMenuItem
            // 
            this.convolutionToolStripMenuItem.Name = "convolutionToolStripMenuItem";
            this.convolutionToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.convolutionToolStripMenuItem.Text = "Convolution";
            this.convolutionToolStripMenuItem.Click += new System.EventHandler(this.convolutionToolStripMenuItem_Click);
            // 
            // FourierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 378);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.inverseFftPictureBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fourierPhasePictureBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fourierMagnitudePictureBox);
            this.Controls.Add(this.inputImagePictureBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FourierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFT";
            ((System.ComponentModel.ISupportInitialize)(this.inputImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierMagnitudePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierPhasePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inverseFftPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox inputImagePictureBox;
        private System.Windows.Forms.PictureBox fourierMagnitudePictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox fourierPhasePictureBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox inverseFftPictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem convolutionToolStripMenuItem;
    }
}

