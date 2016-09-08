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

            //PrepareKernel();

            ImageComplex = ImageDataConverter.ToComplex(image);
            ImageFftComplex = FourierTransform.ForwardFFT(image);
            ImageShiftedFftComplex = FourierShifter.ShiftFft(ImageFftComplex);

            int[, ,] inputImage3d = ImageDataConverter.ToInteger3d_32bit(_inputImage);

            int [,,] filteredImage3d = _Apply2(inputImage3d);

            return ImageDataConverter.ToBitmap3d_32bit(filteredImage3d);
        }

        private int[, ,] _Apply2(int[, ,] imageData3d)
        {
            int[, ,] filteredImage3d = new int[imageData3d.GetLength(0), imageData3d.GetLength(1), imageData3d.GetLength(2)];

            int Width = imageData3d.GetLength(1);
            int Height = imageData3d.GetLength(2);

            ///////////////////////////////////////////////////
            int[,] imageInteger2d = new int[Width, Height];

            for (int dimension = 0; dimension < 3; dimension++)
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                        imageInteger2d[i, j] = imageData3d[dimension, i, j];
                    }
                }

                Complex[,] imageComplex = ImageDataConverter.ToComplex(imageInteger2d);
                Complex[,] imageFftComplex = FourierTransform.ForwardFFT(imageComplex);
                Complex[,] imageFftShiftedComplex = FourierShifter.ShiftFft(imageFftComplex);
                Complex[,] fftShiftedFilteredComplex = Filter(imageFftShiftedComplex);
                Complex[,] fftFilteredComplex = FourierShifter.RemoveFFTShift(fftShiftedFilteredComplex);

                imageComplex = FourierTransform.InverseFFT(fftFilteredComplex);

                int[,] filteredImage2d = ImageDataConverter.ToInteger(imageComplex);

                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                        filteredImage3d[dimension, i, j] = filteredImage2d[i, j];
                    }
                }
            }

            return filteredImage3d;
        }

        private Complex[,] Filter(Complex[,] imageFftComplex)
        {
            int Width = imageFftComplex.GetLength(0);
            int Height = imageFftComplex.GetLength(1);

            Complex[,] Output = new Complex[Width, Height];

            Complex[,] kernel = _kernel.PaddedKernelShiftedFftComplex;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Complex temp = new Complex((RH - RL) * kernel[i, j].Real + RL, 
                            (RH - RL) * kernel[i, j].Imaginary + RL);
                    
                    kernel[i, j] = temp;
                }
            }

            Output = Tools.MultiplyComplex(kernel, imageFftComplex);

            return Output;
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
