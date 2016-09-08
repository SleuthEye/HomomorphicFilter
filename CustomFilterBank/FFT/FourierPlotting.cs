using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public static class FourierPlotter
    {
        public static Bitmap FftMagnitudePlot(Complex[,] fftImage)
        {
            int[,] FourierMagnitudeNormalizedInteger = FourierNormalizer.Normalize(fftImage, NormalizeType.Magnitude);

            return ImageDataConverter.ToBitmap(FourierMagnitudeNormalizedInteger);
        }

        public static Bitmap FftPhasePlot(Complex[,] fftImage)
        {
            int[,] FourierPhaseNormalizedInteger = FourierNormalizer.Normalize(fftImage, NormalizeType.Phase);

            return ImageDataConverter.ToBitmap(FourierPhaseNormalizedInteger);
        }
    }
}
