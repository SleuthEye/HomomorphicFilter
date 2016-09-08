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

            _inputImage = Bitmap.FromFile(path) as Bitmap;

            selectedImagePictureBox.Image = _inputImage;
            loadedImagePictureBox.Image = _inputImage;
        }
       
        private void homoFilterButton_Click(object sender, EventArgs e)
        {
            HomomorphicFilter hmf = new HomomorphicFilter();
            hmf.KernelWidth = _inputImage.Width;
            hmf.KernelHeight = _inputImage.Height;
            hmf.PaddedKernelWidth = _inputImage.Width;
            hmf.PaddedKernelHeight = _inputImage.Height;
            hmf.RL = 0.62;
            hmf.RH = 1.11;
            hmf.Sigma = 64;
            hmf.Slope = 1;

            hmf.PrepareKernel();

            filteredImagePictureBox.Image = hmf.Apply(_inputImage);
        }
    }
}
