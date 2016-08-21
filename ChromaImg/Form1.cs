using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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

        private double w;
        private double h;
        public void popArrayColors(Bitmap map)
        {

            h = map.Height/6;
            w = map.Width/15;
            redArrayA = new int[Convert.ToInt32(h)];
            greenArrayA = new int[Convert.ToInt32(h)];
            blueArrayA = new int[Convert.ToInt32(h)];
            avgRGBOveral = new int[6,15,3];

            for (int i = 0; i < 15; i++)
            {
                getColorAvg(h,w,map);
            }


        }

        private int[] redArrayA;
        private int[] greenArrayA;
        private int[] blueArrayA;
        private int st = 0;
        private void getRGBAvg(int[] r, int[] g, int[] b)
        {
            //redArrayA = new int[Convert.ToInt32(h)];
            //greenArrayA = new int[Convert.ToInt32(h)];
            //blueArrayA = new int[Convert.ToInt32(h)];
            int rezR=0, rezG=0, rezB=0;
            for (int i = 0; i < w; i++)
            {
                rezR += r[i];
                rezG += g[i];
                rezB += b[i];
            }
            redArrayA[st] = rezR/Convert.ToInt32(w);
            greenArrayA[st] = rezG/Convert.ToInt32(w);
            blueArrayA[st] = rezB/Convert.ToInt32(w);
            st++;
        }
        private void getColorAvg(double h, double w, Bitmap map)
        {
            int[] redArray = new int[Convert.ToInt32(w)];
            int[] greenArray = new int[Convert.ToInt32(w)];
            int[] blueArray = new int[Convert.ToInt32(w)];
            
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Color clr = map.GetPixel(i, j);
                    redArray[j] = clr.R;
                    greenArray[j] = clr.G;
                    blueArray[j] = clr.B;

                }
                getRGBAvg(redArray,greenArray,blueArray);

            }
            st = 0;
            calcAvgBlock(redArrayA,greenArrayA,blueArrayA);
        }

        private int[,,] avgRGBOveral;
        private int visina = 0, sirina = 0, z = 0;
        private void calcAvgBlock(int[] r, int[] g, int[] b)
        {
            //r=new int[Convert.ToInt32(w)];
            //g=new int[Convert.ToInt32(w)];
            //b=new int[Convert.ToInt32(w)];
            int rezR = 0, rezG = 0, rezB = 0;
            for (int i = 0; i < w; i++)
            {
                rezR += r[i];
                rezG += g[i];
                rezB += b[i];
            }
            // 3D array's https://msdn.microsoft.com/sl-si/library/2yd9wwz4.aspx
            avgRGBOveral[visina, sirina, 0] = rezR/Convert.ToInt32(h);
            avgRGBOveral[visina, sirina, 1] = rezG/Convert.ToInt32(h);
            avgRGBOveral[visina, sirina, 2] = rezB/Convert.ToInt32(h);
            int rdeca = avgRGBOveral[visina, sirina, 0];
            //Color barva = new Color() { A = 255, R = rdeca, G = avgRGBOveral[visina, sirina, 1], B = avgRGBOveral[visina, sirina, 2] };

            Keyboard.Instance.SetKeys(Corale.Colore.Core.Color.FromRgb(Convert.ToUInt32(avgRGBOveral[visina, sirina, 0]+ avgRGBOveral[visina, sirina, 1]+ avgRGBOveral[visina, sirina, 2])), Key.Macro5);

        }
        
        
    }
}
