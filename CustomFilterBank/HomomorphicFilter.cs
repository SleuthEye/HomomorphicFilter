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
        public HomoMorphicKernel Kernel = null;
        public bool IsPadded { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double RH { get; set; }
        public double RL { get; set; }
        public double Sigma { get; set; }
        public double Slope { get; set; }
        public int PaddedWidth { get; set; }
        public int PaddedHeight { get; set; }
        public Bitmap KernelBitmap
        {
            get
            {
                if (IsPadded)
                {
                    return Kernel.PaddedKernelBitmap;
                }
                else
                {
                    return Kernel.KernelBitmap;
                }
            }
        }

        #region private methods
        private int[,] Apply8bit(int[,] imageData2d)
        {
            Complex[,] imageData2dShiftFftCplx = FourierShifter.ShiftFft(FourierTransform.ForwardFFT(ImageDataConverter.ToComplex(imageData2d)));

            Complex[,] fftShiftedFiltered = null;

            if (IsPadded)
            {
                fftShiftedFiltered = Tools.Multiply(Kernel.PaddedKernel, imageData2dShiftFftCplx);
            }
            else
            {
                fftShiftedFiltered = Tools.Multiply(Kernel.Kernel, imageData2dShiftFftCplx);
            }

            return ImageDataConverter.ToInteger(FourierTransform.InverseFFT(FourierShifter.RemoveFFTShift(fftShiftedFiltered)));
        }

        private int[, ,] Apply3d(int[, ,] image3d)
        {
            int[, ,] filteredImage3d = new int[image3d.GetLength(0), image3d.GetLength(1), image3d.GetLength(2)];

            int widtH = image3d.GetLength(1);
            int heighT = image3d.GetLength(2);

            int[,] imageData2d = new int[widtH, heighT];
            for (int dimension = 0; dimension < 3; dimension++)
            {
                for (int i = 0; i <= widtH - 1; i++)
                {
                    for (int j = 0; j <= heighT - 1; j++)
                    {
                        imageData2d[i, j] = image3d[dimension, i, j];
                    }
                }

                int[,] filteredImage2d = Apply8bit(imageData2d);

                for (int i = 0; i <= widtH - 1; i++)
                {
                    for (int j = 0; j <= heighT - 1; j++)
                    {
                        filteredImage3d[dimension, i, j] = filteredImage2d[i, j];
                    }
                }
            }

            return filteredImage3d;
        }
        #endregion

        public void Compute()
        {
            if (IsPadded)
            {
                if (Width >= PaddedWidth || Height >= PaddedHeight)
                {
                    throw new Exception("PaddedWidth or PaddedHeight must be greater than Width or Height.");
                }
            }

            Kernel = new HomoMorphicKernel();
            Kernel.Width = Width;
            Kernel.Height = Height;
            Kernel.RH = RH;
            Kernel.RL = RL;
            Kernel.Sigma = Sigma;
            Kernel.Slope = Slope;
            Kernel.PaddedWidth = PaddedWidth;
            Kernel.PaddedHeight = PaddedHeight;
            Kernel.Compute();
        }

        public Bitmap Apply8bit(Bitmap image)
        {
            int[,] image2d = ImageDataConverter.ToInteger(image);

            int[,] filtered = Apply8bit(image2d);

            return ImageDataConverter.ToBitmap(filtered);
        }

        public Bitmap Apply32bitColor(Bitmap image)
        {
            int[, ,] image3d = ImageDataConverter.ToInteger3d_32bit(image);

            int[, ,] filtered = Apply3d(image3d);

            return ImageDataConverter.ToBitmap3d_32bit(filtered);
        }
    }
}
