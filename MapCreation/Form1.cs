using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;

namespace MapCreation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox_MouseDown;

            preciseMap = new PixelMap("C:\\Adocuments\\Library\\Clapeyron_ind\\task6 map creation\\PreciseMap1.png");
            pictureBox1.Image = preciseMap.GetBitmap();
        }

        private PixelMap preciseMap;
        private PixelMap scan0;
        private PixelMap scan1;

        private const ushort n_phi = 360;
        private double step = 2 * Math.PI / n_phi;
        private ushort[] rByPhi0 = new ushort[n_phi];
        private int X0, Y0; //0 scan centre
        private ushort[] rByPhi1 = new ushort[n_phi];
        private int X1, Y1; //real centre
        private int X2, Y2; //supposed centre

        private const ushort r_scan = 70;
        private const ushort d_scan = 2 * r_scan + 1;
        private const ushort r_scan1 = r_scan + 1;

        private Color wallColor = Color.FromArgb(255, 255, 255);
        private Color indoorColor = Color.FromArgb(0, 0, 0);
        private Color startColor = Color.FromArgb(94, 255, 0);
        private Color routeColor = Color.FromArgb(255, 51, 0);
        private Color finishColor = Color.FromArgb(0, 222, 255);

        private byte positionCounter = 0;

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X * preciseMap.Width / pictureBox1.Width;
            int Y = e.Y * preciseMap.Height / pictureBox1.Height;

            switch (positionCounter)
            {
                case 0:
                    preciseMap[X0, Y0] = new Pixel(indoorColor);
                    X0 = X;
                    Y0 = Y;
                    preciseMap[X0, Y0] = new Pixel(startColor);
                    
                    scan0 = new PixelMap(d_scan, d_scan, 0, 0, 0);
                    rByPhi0 = getScanFromPreciseMap(X, Y, scan0);

                    Bitmap map = preciseMap.GetBitmap();
                    Pen pen = new Pen(wallColor);
                    Graphics graphics = Graphics.FromImage(map);
                    try
                    {
                        graphics.DrawEllipse(pen, 0,0,100,100);// X0 - r_scan-1, Y0 - r_scan-1, d_scan, d_scan);
                        Console.WriteLine("HI");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception");
                    }
                    map.SetPixel(0,0,startColor);

                    pictureBox1.Image = map;
                    pictureBox2.Image = scan0.GetBitmap();
                    positionCounter++;
                    break;
                case 1:
                    preciseMap[X1, Y1] = new Pixel(indoorColor);
                    X1 = X;
                    Y1 = Y;
                    preciseMap[X1, Y1] = new Pixel(finishColor);
                    scan1 = new PixelMap(d_scan, d_scan, 0, 0, 0);
                    rByPhi1 = getScanFromPreciseMap(X, Y, scan1);
                    pictureBox3.Image = scan1.GetBitmap();
                    positionCounter++;
                    break;
                case 2:
                    preciseMap[X2, Y2] = new Pixel(indoorColor);
                    X2 = X;
                    Y2 = Y;
                    preciseMap[X2, Y2] = new Pixel(routeColor);
                    positionCounter = 0;
                    break;
            }
            pictureBox1.Image = preciseMap.GetBitmap();
        }

        private ushort[] getScanFromPreciseMap(int X, int Y, PixelMap scanBmp)
        {
            ushort[] rByPhi = new ushort[n_phi];
            int y = new int();
            int x = new int();
            bool flag;

            for (int i = 0; i < n_phi; i++)
            {
                flag = false;
                for (ushort r = 1; r < r_scan1; r++)
                {
                    x = (int)Math.Round(r * Math.Cos(i * step)) + X;
                    y = (int)Math.Round(r * Math.Sin(i * step)) + Y;
                    if (preciseMap[x, y].Color == wallColor)
                    {
                        rByPhi[i] = r;
                        scanBmp[x - X + r_scan, y - Y + r_scan] = new Pixel(wallColor);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    rByPhi[i] = 0;
            }
            return rByPhi;
        }
    }
}
