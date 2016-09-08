using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace CustomFilterBank_Test
{
    public partial class FourierForm : Form
    {
        public FourierForm()
        {
            InitializeComponent();
        }

        string path = @"E:\___MSc in Computer Systems & Network\EMSC1,2,3\scratch.png";

        private void loadButton_Click(object sender, EventArgs e)
        {
            Bitmap bInputImage = Grayscale.ToGrayscale( Bitmap.FromFile(path) as Bitmap);

            int newWidth = (int)Tools.ToNextPow2((uint)bInputImage.Width);
            int newHeight = (int)Tools.ToNextPow2((uint)bInputImage.Height);

            Bitmap lenaResized = ImageResizer.Resize(bInputImage, newWidth, newHeight);

            inputImagePictureBox.Image = lenaResized as Image;
        }

        Complex[,] ___shifted;

        private void fftButton_Click(object sender, EventArgs e)
        {
            Bitmap lena = inputImagePictureBox.Image as Bitmap;

            ___shifted = FourierTransform.FFTShift(FourierTransform.ForwardFFT(lena));

            Bitmap magnitudePlot = FourierTransform.FftMagnitudePlot(___shifted);
            Bitmap phasePlot = FourierTransform.FftPhasePlot(___shifted);

            fourierMagnitudePictureBox.Image = (Image)magnitudePlot;
            fourierPhasePictureBox.Image = (Image)phasePlot;
        }

        private void iFftButton_Click(object sender, EventArgs e)
        {
            inverseFftPictureBox.Image = FourierTransform.InverseFFTBitmap(___shifted);
        }

        private void convolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ConvolutionForm f = new ConvolutionForm();
            //f.ShowDialog();
        }
    }
}