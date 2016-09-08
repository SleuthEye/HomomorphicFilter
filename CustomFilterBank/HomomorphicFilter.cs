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
        private Bitmap _inputImage = null;
        public Complex[,] FftImageComplex { get; set; }
        public double RH { get; set; }
        public double RL { get; set; }
        public double Sigma { get; set; }
        public double Slope { get; set; }

        public Complex[,] Apply()
        {
            int Width = FftImageComplex.GetLength(0);
            int Height = FftImageComplex.GetLength(1);

            Complex[,] Output = new Complex[Width, Height];

            double Weight;
            //Taking FFT of Gaussian HPF
            double[,] gausianHpfKernel = Gaussian.GaussianKernelHPF(Width, Height, Sigma, Slope, out Weight);
            
            Complex[,] comp = ImageDataConverter.ToComplex(gausianHpfKernel);
            Complex[,] shiftedFft = FourierTransform.FFTShift(FourierTransform.ForwardFFT(comp));
            //Complex[,] GaussianHPFFFT = shiftedFft;

            for (int i = 0; i < Width ; i++)
            {
                for (int j = 0; j < Height ; j++)
                {
                    Complex temp = new Complex((RH - RL) * shiftedFft[i, j].Real + RL, (RH - RL) * shiftedFft[i, j].Imaginary + RL);
                    shiftedFft[i, j] = temp;
                }
            }
            
            Output = Tools.MultiplyComplex(shiftedFft, FftImageComplex);
           
            return Output;
        }

        public Bitmap Apply(Bitmap image)
        {
            _inputImage = image;

            FftImageComplex = ImageDataConverter.ToComplex(image);

            int[, ,] filteredImage3d = ImageDataConverter.ToInteger3d_32bit(_inputImage);

            filteredImage3d = Apply(filteredImage3d);

            return ImageDataConverter.ToBitmap3d_32bit(filteredImage3d);
        }

        private int[, ,] Apply(int[, ,] imageData3d)
        {
            int[, ,] filteredImage3d = new int[imageData3d.GetLength(0), imageData3d.GetLength(1), imageData3d.GetLength(2)];

            int Width = imageData3d.GetLength(1);
            int Height = imageData3d.GetLength(2);

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

                Complex[,] cImage = ImageDataConverter.ToComplex(imageInteger2d);
                Complex[,] fft = FourierTransform.ForwardFFT(cImage);
                Complex[,] fftShifted = FourierTransform.FFTShift(fft);
                Complex[,] fftShiftedFiltered = Apply();
                Complex[,] fftFiltered = FourierTransform.RemoveFFTShift(fftShiftedFiltered);

                cImage = FourierTransform.InverseFFT(fftFiltered);

                int[,] filteredImage2d = ImageDataConverter.ToInteger(cImage);


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

        public Bitmap ShowKernel()
        {
            ////Displaying Gaussian Kernel Used for Filtering
            double WeightHPF;
            double[,] GaussianKernelHPF = Gaussian.GaussianKernelHPF(_inputImage.Width, _inputImage.Height, Sigma, Slope, out WeightHPF);
            int Width = GaussianKernelHPF.GetLength(0);
            int Height = GaussianKernelHPF.GetLength(1);
            int[,] GaussianImage = new int[Width, Height];

            for (int i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    GaussianImage[i, j] = (int)(255 * GaussianKernelHPF[i, j]);
                }
            }

            return ImageDataConverter.ToBitmap32bit(GaussianImage);
        }
    }
}
