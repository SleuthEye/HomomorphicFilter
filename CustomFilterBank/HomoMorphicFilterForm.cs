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
        Bitmap inputImage;
        int[, ,] _image3d;
        int[, ,] _filteredImage3d;
        double rL=0.62, rH=1.11; 
        double Sigma=64, Slope=1;

        string path = @"E:\___MSc in CSN\EMSC1,2,3\lenagr.png";

        public HomoMorphicFilterForm()
        {
            InitializeComponent();

            inputImage = Bitmap.FromFile(path) as Bitmap;

            selectedImagePictureBox.Image = inputImage;
            loadedImagePictureBox.Image = inputImage;

            _image3d = ImageDataConverter.ToInteger3d_32bit(inputImage); // Reading Colour Image Object
            _filteredImage3d = new int[_image3d.GetLength(0), _image3d.GetLength(1), _image3d.GetLength(2)]; // Output Image
        }

        private void homoFilterButton_Click(object sender, EventArgs e)
        {
            HomomorphicFilter filter = new HomomorphicFilter();
            filter.IsPadded = false;
            filter.Width = _image3d.GetLength(1);
            filter.Height =_image3d.GetLength(2);
            filter.RH = rH;
            filter.RL = rL;
            filter.Sigma = Sigma;
            filter.Slope = Slope;
            filter.PaddedWidth = _image3d.GetLength(1);
            filter.PaddedHeight = _image3d.GetLength(2);
            filter.Compute();

            Bitmap filtered = filter.Apply32bitColor(inputImage);

            gaussianKernelPictureBox.Image = filter.KernelBitmap;
            filteredImagePictureBox.Image = filtered;
        }

        private void filteredImagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pBox = sender as PictureBox;

            PictureBoxForm f = new PictureBoxForm(pBox.Image);
            f.ShowDialog();
        }
    }
}





