using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RSI_Project
{
    public partial class Form1 : Form
    {
        List<Bitmap> orig_img = new List<Bitmap>();
        List<Bitmap> frontal_img = new List<Bitmap>();
        int height;
        int width;
        Bitmap seed;
        List<Bitmap> lateral_img = new List<Bitmap>();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void InitializeTrackBar()
        {
            //Track Bar 1
            trackBar1.Minimum = 0;
            trackBar1.Maximum = orig_img.Count();
            trackBar1.TickFrequency = orig_img.Count();
            trackBar1.LargeChange = 10;
            trackBar1.SmallChange = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

            //Track Bar 2
            trackBar2.Minimum = 0;
            trackBar2.Maximum = orig_img.Count();
            trackBar2.TickFrequency = orig_img.Count();
            trackBar2.LargeChange = 10;
            trackBar2.SmallChange = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            
            //Track Bar 3
            trackBar3.Minimum = 0;
            trackBar3.Maximum = orig_img.Count();
            trackBar3.TickFrequency = orig_img.Count();
            trackBar3.LargeChange = 10;
            trackBar3.SmallChange = 5;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "BMP Files|*.bmp";
            openFileDialog1.Title = "Select a BMP file";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .BMP will be loaded
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in openFileDialog1.FileNames)
                {
                    //Original Images
                    orig_img.Add(new Bitmap(file));

                    int x_max = orig_img.Count();
                    int y_max = x_max;
                    //Loop through the images pixels create collumn
                    int x;
                    Rectangle rect = new Rectangle(50, 50, 50, 50);

                    /* Nova implementação */
                    height = orig_img[0].Height;
                    width = orig_img[0].Width;

                    seed = new Bitmap(width, height);
                    //Empty bmp
                    /*var b = new Bitmap(1, 1);
                    b.SetPixel(0, 0, Color.White);
                    var result = new Bitmap(b, x_max, y_max);
                    for(x=0; x<x_max; x++)
                    {
                        /*
                        //Create new bitmap and associated graphics object
                        Bitmap frontal = new Bitmap(rect.Width, rect.Height);
                        Graphics g = Graphics.FromImage(frontal);

                        //Draw the specific section of the source bitmap to the new one
                        g.DrawImage(result, 0, 0, rect, GraphicsUnit.Pixel);
                        g.Dispose();
                         //Esta parte não funciona como tenciono, deve faltar algo mais
                        frontal_img.Add(orig_img.ElementAt(x));
                        //orig_img.ElementAt(x);
                        //Tentativa de buscar as linhas da imagem
                    }*/
                    //Lateral Images
                }
                //Insert Track Bar properties
                InitializeTrackBar();

                //Verificação de contagem de imagens. Está a funcionar bem!
                textBox1.Text = "" + orig_img.Count();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB1;
            myTB1 = (System.Windows.Forms.TrackBar)sender;
            pictureBox1.Image = orig_img.ElementAtOrDefault(myTB1.Value);

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB2;
            myTB2 = (System.Windows.Forms.TrackBar)sender;
            process_frontal(myTB2.Value);
            pictureBox2.Image = seed;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB3;
            myTB3 = (System.Windows.Forms.TrackBar)sender;
        }

        private void process_frontal(int pos)
        {
            for (int i = 0; i < orig_img.Count(); i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color newPixel = orig_img[i].GetPixel(j, pos);
                    seed.SetPixel(j, i, newPixel);
                }
            }
        }
    }
}
