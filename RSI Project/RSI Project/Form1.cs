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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BMP Files|*.bmp";
            openFileDialog1.Title = "Select a BMP file";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //string startupPath = Environment.CurrentDirectory;
                //string directoryPath = System.IO.Path.GetDirectoryName(startupPath);
                //int numbFiles = openFileDialog1.FileName.Length;
                Bitmap[] images;
                for(int i=1; i<openFileDialog1.FileName.Length; i++)
                {
                    images[i] = openFileDialog1.FileNames.Select(fn => new Bitmap(fn)).ToArray();
                    //Estou com dificuldades em armazenar um array de imagens. Ele tem problemas na classe Bitmap.
                    //Não sei se devíamos usar o foreach e uma lista com as imagens carregadas invés.
                }
            }

        }
        public static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
