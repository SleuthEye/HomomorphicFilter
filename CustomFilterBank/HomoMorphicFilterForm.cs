using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace CustomFilterBank_Test
{
    public partial class HomoMorphicFilterForm : Form
    {
        string path = @"E:\___MSc in CSN\EMSC1,2,3\hough.png";
        private Bitmap _inputImage;
        const int KERNEL_SIZE = 40;

        public HomoMorphicFilterForm()
        {
            InitializeComponent();

            _inputImage = /*Grayscale.ToGrayscale(*/ Bitmap.FromFile(path) as Bitmap /*)*/;

            selectedImagePictureBox.Image = _inputImage;
            loadedImagePictureBox.Image = _inputImage;
        }
       
        private void homoFilterButton_Click(object sender, EventArgs e)
        {
            int newWidth = (int)Tools.ToNextPow2((uint)_inputImage.Width + KERNEL_SIZE -1);
            int newHeight = (int)Tools.ToNextPow2((uint)_inputImage.Height + KERNEL_SIZE - 1);

            HomomorphicFilter hmf = new HomomorphicFilter();

            hmf.KernelWidth = newWidth;
            hmf.KernelHeight = newHeight;
            hmf.PaddedKernelWidth = newWidth;
            hmf.PaddedKernelHeight = newHeight;
            hmf.RL = 0.62;
            hmf.RH = 1.11;
            hmf.Sigma = 64;
            hmf.Slope = 1;

            hmf.PrepareKernel();

            gaussianKernelPictureBox.Image = hmf.GetKernelBitmap();

            //Bitmap paddedImage = ImagePadder.Pad(_inputImage, newWidth, newHeight);

            filteredImagePictureBox.Image = hmf.Apply(_inputImage);
        }

        private void filteredImagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pBox = sender as PictureBox;

            PictureBoxForm f = new PictureBoxForm(pBox.Image);
            f.ShowDialog();
        }
    }
}
