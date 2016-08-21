using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Color = System.Drawing.Color;

namespace ChromaImg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void turnKeyRed()
        {
            Keyboard.Instance.SetKeys(Corale.Colore.Core.Color.Blue, Key.A, Key.D4);
        }

        private Bitmap bitmap;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // 
            {
                using (Stream bmpStream = File.Open(openFileDialog1.FileName, FileMode.Open))
                {
                    Image image = Image.FromStream(bmpStream);
                    bitmap = new Bitmap(image);
                }
                popArrayColors(bitmap);
            }

        }

        private int[,] colorArray= new int[6,15];
        
       
        public void popArrayColors(Bitmap map)
        {

            double h = map.Height/6;
            double w = map.Width/15;
            for (int i = 0; i < 15; i++)
            {
                getColorAvg(h+h, w+w, map);
            }


        }

        private void getColorAvg(double h, double w, Bitmap map)
        {
            int[] pixelArray = new int[Convert.ToInt32(h)];
            int[] keyArray = new int[Convert.ToInt32(w)];
            int c = 0;
            int rez = 0;
            for (int i = 0; i < w; i++)
            {
              
                var a = 0;
                for (int j = 0; j < h; j++)
                {
                    
                    Color clr = map.GetPixel(i, j);
                    int red = clr.R;
                    int green = clr.G;
                    int blue = clr.B;
                    int avg = (red + green + blue) / 3;
                    pixelArray[a] = avg;
                    a++;
                }
                for (int j = 0; j < pixelArray.Length; j++)
                {
                    rez += pixelArray[j];
                }

                float avgP = rez / pixelArray.Length;
                rez = 0;
                keyArray[c] = Convert.ToInt32(avgP);
                c++;
            }
            int avgBlock = 0;
            for (int i = 0; i < keyArray.Length; i++)
            {
                avgBlock += keyArray[i];
            }
            //Keyboard.Instance.SetKeys();
        }
        
        
    }
}
