using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomFilterBank_Test
{

    public partial class PictureBoxForm : Form
    {
        public PictureBoxForm()
        {
            InitializeComponent();
        }

        public PictureBoxForm(Image image)
        {
            InitializeComponent();

            this.pictureBox1.Image = image;
        }

        public PictureBoxForm(Bitmap image)
        {
            InitializeComponent();

            this.pictureBox1.Image = image as System.Drawing.Image;
        }

        public PictureBoxForm(Bitmap input, Bitmap output)
        {
            InitializeComponent();

            this.pictureBox1.Image = output as System.Drawing.Image;
            this.pictureBox2.Image = input as System.Drawing.Image;
        }

        public PictureBoxForm(Image input, Image output)
        {
            InitializeComponent();

            this.pictureBox1.Image = output;
            this.pictureBox2.Image = input;
        }

        public PictureBoxForm(Complex[,] image1, Complex[,] image2)
        {
            InitializeComponent();

            Bitmap bitmap1 = ImageDataConverter.ToBitmap(image1);
            Bitmap bitmap2 = ImageDataConverter.ToBitmap(image2);

            this.pictureBox1.Image = bitmap1 as System.Drawing.Image;
            this.pictureBox2.Image = bitmap2 as System.Drawing.Image;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Save(@"C:\Users\edurazee\Desktop\lenagr2.png");
                MessageBox.Show("Image saved !", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string str = string.Empty;
        }
    }
}
