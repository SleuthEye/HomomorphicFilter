using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public class HomoMorpficKernel
    {
        public double[,] KernelDouble { get; private set; }
        public double[,] PaddedKernelDouble { get; private set; }

        public Bitmap KernelBitmap { get; private set; }
        public Bitmap PaddedKernelBitmap { get; private set; }

        public Complex[,] KernelComplex { get; private set; }
        public Complex[,] PaddedKernelComplex { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int PaddedWidth { get; set; }
        public int PaddedHeight { get; set; }

        public double Sigma { get; set; }
        public double Slope { get; set; }
        public double Weight { get; private set; }

        public HomoMorpficKernel()
        {            
        }

        public void Compute()
        {
            double weight;
            KernelDouble = Gaussian.GaussianKernelHPF(Width, Height, Sigma, Slope, out weight); Weight = weight;
            
            PaddedKernelDouble = ImagePadder.Pad(KernelDouble, PaddedWidth, PaddedHeight);

            KernelBitmap = ImageDataConverter.ToBitmap(KernelDouble);

        }
    }
}
