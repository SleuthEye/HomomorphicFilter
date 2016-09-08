using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace CustomFilterBank_Test
{
    public class FourierTransform
    {   
        public static Complex[,] ForwardFFT(Complex[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            return FourierFunction.FFT2D(image, Width, Height, 1);
        }

        public static Complex[,] ForwardFFT(Bitmap image)
        {
            Complex[,] cImage = ImageDataConverter.ToComplex(image);

            return ForwardFFT(cImage);
        }

        public static Complex[,] InverseFFT(Complex[,] fftImage)
        {
            int Width = fftImage.GetLength(0);
            int Height = fftImage.GetLength(1);

            return FourierFunction.FFT2D(fftImage, Width, Height, -1);
        }

        public static Bitmap InverseFFTBitmap(Complex[,] fftImage)
        {
            return ImageDataConverter.ToBitmap(InverseFFT(fftImage));
        }
    }
}