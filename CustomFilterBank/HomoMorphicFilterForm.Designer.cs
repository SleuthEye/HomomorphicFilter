namespace CustomFilterBank_Test
{
    partial class HomoMorphicFilterForm
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
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.filteredImagePictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gaussianKernelPictureBox = new System.Windows.Forms.PictureBox();
            this.selectedImagePictureBox = new System.Windows.Forms.PictureBox();
            this.loadedImagePictureBox = new System.Windows.Forms.PictureBox();
            this.CNMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectFullImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.homoFilterButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.filteredImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaussianKernelPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadedImagePictureBox)).BeginInit();
            this.CNMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(787, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Homomorphic Filtered Image";
            // 
            // filteredImagePictureBox
            // 
            this.filteredImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.filteredImagePictureBox.Location = new System.Drawing.Point(777, 28);
            this.filteredImagePictureBox.Name = "filteredImagePictureBox";
            this.filteredImagePictureBox.Size = new System.Drawing.Size(250, 250);
            this.filteredImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.filteredImagePictureBox.TabIndex = 29;
            this.filteredImagePictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "High Pass Gaussian Kernel Plot";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Selected Image";
            // 
            // gaussianKernelPictureBox
            // 
            this.gaussianKernelPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gaussianKernelPictureBox.Location = new System.Drawing.Point(522, 28);
            this.gaussianKernelPictureBox.Name = "gaussianKernelPictureBox";
            this.gaussianKernelPictureBox.Size = new System.Drawing.Size(250, 250);
            this.gaussianKernelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gaussianKernelPictureBox.TabIndex = 26;
            this.gaussianKernelPictureBox.TabStop = false;
            // 
            // selectedImagePictureBox
            // 
            this.selectedImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.selectedImagePictureBox.Location = new System.Drawing.Point(267, 28);
            this.selectedImagePictureBox.Name = "selectedImagePictureBox";
            this.selectedImagePictureBox.Size = new System.Drawing.Size(250, 250);
            this.selectedImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.selectedImagePictureBox.TabIndex = 25;
            this.selectedImagePictureBox.TabStop = false;
            // 
            // loadedImagePictureBox
            // 
            this.loadedImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.loadedImagePictureBox.ContextMenuStrip = this.CNMenuStrip;
            this.loadedImagePictureBox.Location = new System.Drawing.Point(12, 28);
            this.loadedImagePictureBox.Name = "loadedImagePictureBox";
            this.loadedImagePictureBox.Size = new System.Drawing.Size(250, 250);
            this.loadedImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadedImagePictureBox.TabIndex = 33;
            this.loadedImagePictureBox.TabStop = false;
            // 
            // CNMenuStrip
            // 
            this.CNMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFullImageToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.CNMenuStrip.Name = "CNMenuStrip";
            this.CNMenuStrip.Size = new System.Drawing.Size(137, 70);
            // 
            // selectFullImageToolStripMenuItem
            // 
            this.selectFullImageToolStripMenuItem.Name = "selectFullImageToolStripMenuItem";
            this.selectFullImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.selectFullImageToolStripMenuItem.Text = "Select Image";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openFileToolStripMenuItem.Text = "Preview";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // homoFilterButton
            // 
            this.homoFilterButton.Location = new System.Drawing.Point(522, 284);
            this.homoFilterButton.Name = "homoFilterButton";
            this.homoFilterButton.Size = new System.Drawing.Size(132, 31);
            this.homoFilterButton.TabIndex = 51;
            this.homoFilterButton.Text = "Homomorphic Filtering";
            this.homoFilterButton.UseVisualStyleBackColor = true;
            this.homoFilterButton.Click += new System.EventHandler(this.homoFilterButton_Click);
            // 
            // HomoMorphicFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 325);
            this.Controls.Add(this.homoFilterButton);
            this.Controls.Add(this.loadedImagePictureBox);
            this.Controls.Add(this.filteredImagePictureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.selectedImagePictureBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gaussianKernelPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HomoMorphicFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Homomorphic Filtering";
            ((System.ComponentModel.ISupportInitialize)(this.filteredImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaussianKernelPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadedImagePictureBox)).EndInit();
            this.CNMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox filteredImagePictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox gaussianKernelPictureBox;
        private System.Windows.Forms.PictureBox selectedImagePictureBox;
        private System.Windows.Forms.PictureBox loadedImagePictureBox;
        private System.Windows.Forms.ContextMenuStrip CNMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectFullImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button homoFilterButton;
    }
}

