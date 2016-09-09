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

            //HomomorphicFilter hmf = new HomomorphicFilter();
            //hmf.KernelWidth = newWidth;
            //hmf.KernelHeight = newHeight;
            //hmf.PaddedKernelWidth = newWidth;
            //hmf.PaddedKernelHeight = newHeight;
            //hmf.RL = 0.62;
            //hmf.RH = 1.11;
            //hmf.Sigma = 64;
            //hmf.Slope = 1;

            //hmf.PrepareKernel();

            //gaussianKernelPictureBox.Image = hmf.GetKernelBitmap();

            ////Bitmap paddedImage = ImagePadder.Pad(_inputImage, newWidth, newHeight);
            ////new PictureBoxForm(_inputImage).ShowDialog();
            //filteredImagePictureBox.Image = hmf.Apply(_inputImage);

            HomoMorphicKernel _kernel = new HomoMorphicKernel();
            _kernel.RH = 1.11;
            _kernel.RL = 0.62;
            _kernel.Sigma = 64;
            _kernel.Slope = 1;
            _kernel.Width = 40;
            _kernel.Height = 40;
            _kernel.PaddedWidth = newWidth;
            _kernel.PaddedHeight = newHeight;
            _kernel.Compute();

            /////////////////////////////////////////////////////////////////////////
            int[, ,] imageData3d = ImageDataConverter.ToInteger3d_32bit(_inputImage);
            int[, ,] filteredImage3d = new int[imageData3d.GetLength(0),
                                                imageData3d.GetLength(1),
                                                imageData3d.GetLength(2)];
            int width = imageData3d.GetLength(1);
            int height = imageData3d.GetLength(2);

            ///////////////////////////////////////////////////
            for (int dimension = 0; dimension < 3; dimension++)
            {
                int[,] imageInteger2d = new int[width, height];

                for (int i = 0; i <= width - 1; i++)
                {
                    for (int j = 0; j <= height - 1; j++)
                    {
                        imageInteger2d[i, j] = imageData3d[dimension, i, j];
                    }
                }

                //new PictureBoxForm(ImageDataConverter.ToBitmap(imageInteger2d)).ShowDialog();

                Complex[,] imageComplex = ImageDataConverter.ToComplex(imageInteger2d);
                Complex[,] imageFftComplex = FourierTransform.ForwardFFT(imageComplex);
                Complex[,] imageFftShiftedComplex = FourierShifter.ShiftFft(imageFftComplex);
                //////////////////////////////////////////////////////////////////////////////

                Complex[,] fftShiftedFilteredComplex = new Complex[width, height];

                Complex[,] paddedKernelFFtShift = _kernel.PaddedKernelShiftedFftComplex;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        fftShiftedFilteredComplex[i, j] = imageFftShiftedComplex[i, j] * paddedKernelFFtShift[i, j];
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////
                Complex[,] fftFilteredComplex = FourierShifter.RemoveFFTShift(fftShiftedFilteredComplex);
                Complex[,] filteredImageComplex = FourierTransform.InverseFFT(fftFilteredComplex);
                int[,] filteredImage2d = ImageDataConverter.ToInteger(filteredImageComplex);

                //new PictureBoxForm(ImageDataConverter.ToBitmap(filteredImage2d)).ShowDialog();

                for (int i = 0; i <= width - 1; i++)
                {
                    for (int j = 0; j <= height - 1; j++)
                    {
                        filteredImage3d[dimension, i, j] = filteredImage2d[i, j];
                    }
                }
            }///////////////////////////////////////////////////////////////////////////////////////

            filteredImagePictureBox.Image = ImageDataConverter.ToBitmap3d_32bit(filteredImage3d);
        }

        private void filteredImagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pBox = sender as PictureBox;

            PictureBoxForm f = new PictureBoxForm(pBox.Image);
            f.ShowDialog();
        }
    }
}
