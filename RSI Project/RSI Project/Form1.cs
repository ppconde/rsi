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
        List<Bitmap> images = new List<Bitmap>();

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
            trackBar1.Maximum = images.Count();
            trackBar1.TickFrequency = images.Count();
            trackBar1.LargeChange = 10;
            trackBar1.SmallChange = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

            //Track Bar 2
            trackBar2.Minimum = 0;
            trackBar2.Maximum = images.Count();
            trackBar2.TickFrequency = images.Count();
            trackBar2.LargeChange = 10;
            trackBar2.SmallChange = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            
            //Track Bar 3
            trackBar3.Minimum = 0;
            trackBar3.Maximum = images.Count();
            trackBar3.TickFrequency = images.Count();
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
                    images.Add(new Bitmap(file));
                }
                //Insert Track Bar properties
                InitializeTrackBar();

                //Verificação de contagem de imagens. Está a funcionar bem!
                textBox1.Text = "" + images.Count();
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
            pictureBox1.Image = images.ElementAtOrDefault(myTB1.Value);

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB2;
            myTB2 = (System.Windows.Forms.TrackBar)sender;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar myTB3;
            myTB3 = (System.Windows.Forms.TrackBar)sender;
        }
    }
}
