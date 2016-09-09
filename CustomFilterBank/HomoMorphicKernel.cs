using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public class HomoMorphicKernel 
    {
        public Bitmap KernelBitmap { get; private set; }
        public Complex[,] Kernel { get; private set; }

        public Bitmap PaddedKernelBitmap { get; private set; }
        public Complex[,] PaddedKernel { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int PaddedWidth { get; set; }
        public int PaddedHeight { get; set; }

        public double RH { get; set; }
        public double RL { get; set; }
        public double Sigma { get; set; }
        public double Slope { get; set; }
        public double Weight { get; private set; }

        public HomoMorphicKernel() { }

        public HomoMorphicKernel(int width, int height,
            int paddedWidth, int paddedHeight,
            double sigma, double slope)
        {
            Width = width;
            Height = height;
            PaddedWidth = paddedWidth;
            PaddedHeight = paddedHeight;
            Sigma = sigma;
            Slope = slope;
        }

        public void Compute()
        {
            double weight;
            double[,] KernelDouble = Gaussian.GaussianKernelHPF(Width, Height, Sigma, Slope, out weight);
            Weight = weight;
            KernelBitmap = GetKernelBitmap(KernelDouble);
            Complex[,] KernelComplex = ImageDataConverter.ToComplex(KernelDouble);
            Complex[,] KernelFftComplex = FourierTransform.ForwardFFT(KernelComplex);
            Kernel = GetKernelScaled(FourierShifter.ShiftFft(KernelFftComplex), RH, RL);

            //new PictureBoxForm(KernelBitmap).ShowDialog();

            double[,] PaddedKernelDouble = ImagePadder.Pad(KernelDouble, PaddedWidth, PaddedHeight);
            PaddedKernelBitmap = GetKernelBitmap(PaddedKernelDouble);
            Complex[,] PaddedKernelComplex = ImageDataConverter.ToComplex(PaddedKernelDouble);
            Complex[,] PaddedKernelFftComplex = FourierTransform.ForwardFFT(PaddedKernelComplex);
            PaddedKernel = GetKernelScaled(FourierShifter.ShiftFft(PaddedKernelFftComplex), RH, RL);

            //new PictureBoxForm(PaddedKernelBitmap).ShowDialog();
        }

        private Complex[,] GetKernelScaled(Complex[,] kernelShiftedFftComplex, double rH, double rL)
        {
            int width = kernelShiftedFftComplex.GetLength(0);
            int height = kernelShiftedFftComplex.GetLength(1);

            Complex[,] output = new Complex[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    output[i, j] = new Complex((rH - rL) * kernelShiftedFftComplex[i, j].Real + rL,
                        (rH - rL) * kernelShiftedFftComplex[i, j].Imaginary + rL);
                }
            }

            return output;
        }

        private Bitmap GetKernelBitmap(double[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            int[,] GaussianImage = new int[Width, Height];

            for (int i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    GaussianImage[i, j] = (int)(255 * image[i, j]);
                }
            }

            return ImageDataConverter.ToBitmap32bitColor(GaussianImage);
        }
    }
}
