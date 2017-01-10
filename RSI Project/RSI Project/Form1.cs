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
using System.Diagnostics;

namespace RSI_Project
{
    public partial class Form1 : Form
    {
        List<Bitmap> orig_img = new List<Bitmap>();
        List<Bitmap> frontal_img = new List<Bitmap>();
        int height;
        int width;
        Bitmap seedFrontal;
        Bitmap seedLateral;


        List<Bitmap> lateral_img = new List<Bitmap>();
        int currBMP, currF,currL;
        Point coordinates;

        int w_iF, h_iF, w_cF, h_cF;
        int w_iL, h_iL, w_cL, h_cL;


        int px, py;

        Boolean paintHandler = false;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            BackColor = Color.Black;
            checkBox1.ForeColor = Color.White;
        }
        private void InitializeTrackBar()
        {
            //Track Bar 1
            trackBar1.Minimum = 0;
            trackBar1.Maximum = orig_img.Count()-1;
            trackBar1.TickFrequency = 10;
            trackBar1.LargeChange = 10;
            trackBar1.SmallChange = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

            //Track Bar 2
            trackBar2.Minimum = 0;
            trackBar2.Maximum = orig_img[0].Width-1;
            //trackBar2.Maximum = orig_img.Count();
            trackBar2.TickFrequency = 10;
            trackBar2.LargeChange = 10;
            trackBar2.SmallChange = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);

            //Track Bar 3
            trackBar3.Minimum = 0;
            trackBar3.Maximum = orig_img[0].Height-1;
            //trackBar3.Maximum = orig_img.Count();
            trackBar3.TickFrequency = 10;
            trackBar3.LargeChange = 10;
            trackBar3.SmallChange = 1;
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

                    height = orig_img[0].Height;
                    width = orig_img[0].Width;

                    seedFrontal = new Bitmap(width, height);
                    seedLateral = new Bitmap(width, height);

                }
                //Insert Track Bar properties
                InitializeTrackBar();

                //Verificação de contagem de imagens. Está a funcionar bem!
                textBox1.Text = orig_img.Count().ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            coordinates = me.Location;
            px = coordinates.X * width / pictureBox1.Width;
            py = coordinates.Y * height / pictureBox1.Height;
            process_frontal(py);
            process_lateral(px);
            pictureBox1.Image = orig_img.ElementAtOrDefault(currBMP);
            pictureBox2.Image = seedFrontal;
            pictureBox3.Image = seedLateral;
            trackBar2.Value = py;
            trackBar3.Value = px;
            w_iF = pictureBox2.Image.Width;
            h_iF = pictureBox2.Image.Height;
            w_cF = pictureBox2.ClientSize.Width;
            h_cF = pictureBox2.ClientSize.Height;
            w_iL = pictureBox3.Image.Width;
            h_iL = pictureBox3.Image.Height;
            w_cL = pictureBox3.ClientSize.Width;
            h_cL = pictureBox3.ClientSize.Height;
            paintHandler = true;
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
            currBMP = myTB1.Value;
            pictureBox1.Image = orig_img.ElementAtOrDefault(myTB1.Value);
            pictureBox1.Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB2;
            myTB2 = (System.Windows.Forms.TrackBar)sender;
            currF = myTB2.Value;
            process_frontal(myTB2.Value);
            pictureBox2.Image = seedFrontal;
            pictureBox2.Invalidate();
            paintHandler = false;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB3;
            myTB3 = (System.Windows.Forms.TrackBar)sender;
            currL = myTB3.Value;
            process_lateral(myTB3.Value);
            pictureBox3.Image = seedLateral;
            pictureBox3.Invalidate();
            paintHandler = false;
        }

        private void process_frontal(int pos)
        {
            Color newPixel;

            if (checkBox1.Checked)
            {
                int flip = 0;
                for (int i = orig_img.Count() - 1; i >= 0 && flip < orig_img.Count(); i--)
                {
                    for (int j = 0; j < width; j++)
                    {
                        newPixel = orig_img[i].GetPixel(j, pos);
                        seedFrontal.SetPixel(j, flip, newPixel);
                    }
                    flip++;
                }
            }
            else
            {
                for(int i = 0; i < orig_img.Count() - 1;i++)
                {
                    for(int j = 0; j < width; j++)
                    {
                        newPixel = orig_img[i].GetPixel(j, pos);
                        seedFrontal.SetPixel(j, i, newPixel);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkBox1.Text = "Aquisição em sentido caudo-craneal";
            }
            else
            {
                checkBox1.Text = "Aquisição em sentido cranêo-caudal";
            }
        }

        private void process_lateral(int pos)
        {
            Color newPixel;

            if (checkBox1.Checked)
            {
                int flip = 0;
                for (int i = orig_img.Count() - 1; i >= 0 && flip < orig_img.Count(); i--)
                {
                    for (int j = 0; j < height; j++)
                    {
                        newPixel = orig_img[i].GetPixel(pos, j);
                        seedLateral.SetPixel(j, flip, newPixel);
                    }
                    flip++;
                }
            }
            else
            {
                for (int i = 0; i <orig_img.Count() - 1; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        newPixel = orig_img[i].GetPixel(pos, j);
                        seedLateral.SetPixel(j, i, newPixel);
                    }
                }
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!paintHandler)
            {
                return;
            }
            Pen penG = new Pen(Color.GreenYellow, 1);
            Pen penR = new Pen(Color.OrangeRed, 1);
            e.Graphics.DrawLine(penG, new Point(coordinates.X, 0), new Point(coordinates.X, pictureBox1.Height));
            e.Graphics.DrawLine(penR, new Point(0, coordinates.Y), new Point(pictureBox1.Width, coordinates.Y));

            int y = height - py;
            int idx = currBMP + 1;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("Slice Index: " + idx, Font, Brushes.White, 20, 15);
            e.Graphics.DrawString("x: " + px, Font, Brushes.White, 20, 30);
            e.Graphics.DrawString("y: " + y, Font, Brushes.White, 20, 45);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            int aux;
            if (!paintHandler)
            {
                return;
            }

            if(checkBox1.Checked)
            {
                aux = orig_img.Count() - 1 - currBMP;
            }
            else
            {
                aux = currBMP;
            }
                  
            float imageRatio = w_iF / (float)h_iF; // image W:H ratio
            float containerRatio = w_cF / (float)h_cF; // container W:H ratio
            float scaledHeight;
            float scaledWidth;
            Point line = new Point();
            float filler;
            Point imgStart = new Point();
            int imgEnd;
            if (imageRatio >= containerRatio)
            {
                // horizontal image
                float scaleFactor = w_cF / (float)w_iF;
                scaledHeight = h_iF * scaleFactor;
                // calculate gap between top of container and top of image
                filler = Math.Abs(h_cF - scaledHeight) / 2;
                line.X = (int)(px * scaleFactor);
                line.Y = (int)(aux * scaleFactor + filler);
                imgStart.X = 0;
                imgStart.Y = (int)filler;
                imgEnd = imgStart.Y + (int)scaledHeight;

            }
            else
            {
                // vertical image
                float scaleFactor = h_cF / (float)h_iF;
                scaledWidth = w_iF * scaleFactor;
                filler = Math.Abs(w_cF - scaledWidth) / 2;
                line.X = (int)((px * scaleFactor) + filler);
                line.Y = (int)(aux * scaleFactor);
                imgStart.X = (int)filler;
                imgStart.Y = 0;
                imgEnd = imgStart.X + (int)scaledWidth;
            }

            Pen pen = new Pen(Color.OrangeRed, 1);
            e.Graphics.DrawLine(pen, new Point(imgStart.X, line.Y), new Point(imgEnd, line.Y));
            e.Graphics.DrawLine(pen, new Point(line.X,imgStart.Y), new Point(line.X,imgEnd));
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            int aux;
            if (!paintHandler)
            {
                return;
            }

            if (checkBox1.Checked)
            {
                aux = orig_img.Count() - 1 - currBMP;
            }
            else
            {
                aux = currBMP;
            }

            float imageRatio = w_iL / (float)h_iL; // image W:H ratio
            float containerRatio = w_cL / (float)h_cL; // container W:H ratio
            float scaledHeight;
            float scaledWidth;
            Point line = new Point();
            float filler;
            Point imgStart = new Point();
            int imgEnd;
            if (imageRatio >= containerRatio)
            {
                // horizontal image
                float scaleFactor = w_cL / (float)w_iL;
                scaledHeight = h_iL * scaleFactor;
                // calculate gap between top of container and top of image
                filler = Math.Abs(h_cL - scaledHeight) / 2;
                line.X = (int)(py * scaleFactor);
                line.Y = (int)(aux * scaleFactor + filler);
                imgStart.X = 0;
                imgStart.Y = (int)filler;
                imgEnd = imgStart.Y + (int)scaledHeight;

            }
            else
            {
                // vertical image
                float scaleFactor = h_cL / (float)h_iL;
                scaledWidth = w_iL * scaleFactor;
                filler = Math.Abs(w_cL - scaledWidth) / 2;
                line.X = (int)((py * scaleFactor) + filler);
                line.Y = (int)(aux * scaleFactor);
                imgStart.X = (int)filler;
                imgStart.Y = 0;
                imgEnd = imgStart.X + (int)scaledWidth;
            }

            Pen pen = new Pen(Color.GreenYellow, 1);
            e.Graphics.DrawLine(pen, new Point(imgStart.X, line.Y), new Point(imgEnd, line.Y));
            e.Graphics.DrawLine(pen, new Point(line.X, imgStart.Y), new Point(line.X, imgEnd));
        }
    }    
}