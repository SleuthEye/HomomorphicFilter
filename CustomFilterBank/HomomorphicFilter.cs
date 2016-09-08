using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Numerics;

namespace CustomFilterBank_Test
{
    public class HomomorphicFilter
    {
        private HomoMorphicKernel _kernel = null;
        private Bitmap _inputImage = null;
        public Complex[,] ImageComplex = null;
        public Complex[,] ImageFftComplex = null;
        public Complex[,] ImageShiftedFftComplex = null;

        public int KernelWidth { get; set; }
        public int KernelHeight { get; set; }

        public int PaddedKernelWidth { get; set; }
        public int PaddedKernelHeight { get; set; }

        public double RH { get; set; }
        public double RL { get; set; }
        public double Sigma { get; set; }
        public double Slope { get; set; }

        public void PrepareKernel()
        {
            _kernel = new HomoMorphicKernel();
            _kernel.RH = RH;
            _kernel.RL = RL;
            _kernel.Sigma = Sigma;
            _kernel.Slope = Slope;
            _kernel.Width = KernelWidth;
            _kernel.Height = KernelHeight;
            _kernel.PaddedWidth = PaddedKernelWidth;
            _kernel.PaddedHeight = PaddedKernelHeight;
            _kernel.Compute();
        }

        public Bitmap Apply(Bitmap image)
        {
            _inputImage = image;

            ImageComplex = ImageDataConverter.ToComplex(image);
            ImageFftComplex = FourierTransform.ForwardFFT(image);
            ImageShiftedFftComplex = FourierShifter.ShiftFft(ImageFftComplex);

            int[, ,] inputImage3d = ImageDataConverter.ToInteger3d_32bit(_inputImage);

            int [,,] filteredImage3d = ApplyFilterTo3d(inputImage3d, RH, RL);

            return ImageDataConverter.ToBitmap3d_32bit(filteredImage3d);
        }

        private int[, ,] ApplyFilterTo3d(int[, ,] imageData3d, double rh, double rl)
        {
            int[, ,] filteredImage3d = new int[imageData3d.GetLength(0), imageData3d.GetLength(1), imageData3d.GetLength(2)];

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

                Complex[,] imageComplex = ImageDataConverter.ToComplex(imageInteger2d);
                Complex[,] imageFftComplex = FourierTransform.ForwardFFT(imageComplex);
                Complex[,] imageFftShiftedComplex = FourierShifter.ShiftFft(imageFftComplex);
                //////////////////////////////////////////////////////////////////////////////
                
                Complex[,] fftShiftedFilteredComplex = Tools.Multiply(imageFftShiftedComplex, _kernel.KernelShiftedFftComplex);
                
                /////////////////////////////////////////////////////////////////////////////////////////
                Complex[,] fftFilteredComplex = FourierShifter.RemoveFFTShift(fftShiftedFilteredComplex);
                Complex[,]filteredImageComplex = FourierTransform.InverseFFT(fftFilteredComplex);
                int[,] filteredImage2d = ImageDataConverter.ToInteger(filteredImageComplex);

                //new PictureBoxForm(ImageDataConverter.ToBitmap(filteredImage2d)).ShowDialog();

                for (int i = 0; i <= width - 1; i++)
                {
                    for (int j = 0; j <= height - 1; j++)
                    {
                        filteredImage3d[dimension, i, j] = filteredImage2d[i, j];
                    }
                }
            }

            return filteredImage3d;
        }

        public Bitmap GetKernel()
        {
            return _kernel.KernelBitmap;
        }

        public Bitmap GetPaddedKernel()
        {
            return _kernel.PaddedKernelBitmap;
        }
    }
}
