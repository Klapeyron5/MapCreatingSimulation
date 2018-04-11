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
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            
            preciseMap = new PixelMap("C:\\Adocuments\\Library\\Clapeyron_ind\\task6 map creation\\PreciseMap1.png");
            preciseIndoorMap = getPreciseIndoorMap(preciseMap);
            pictureBox1.Image = preciseMap.GetBitmap();
        }

        private PixelMap preciseMap; //точная карта
        private PixelMap scan0; //холст для scan0
        private PixelMap scan1; //холст для scan1

        private PixelMap preciseIndoorMap; //точная карта indoor-среды: с отступами от препятствий точной карты

        //Simulation parameters
        private const ushort n_phi = 250;
        private const ushort r_robot = 5; //5+1 (потому что центральный px еще) = 6px = 25cm
        private const ushort r_scan = 70; //25cm*12=3m; 6px*12=72px ~ 70+1
        private const ushort l_max = 35; //1.5m
        private const ushort sgm_lmax = 4; //4px = 16.5cm
        private const double sgm_psi = 0.14; //rad (~20cm)
    //    private const ushort sgm_r = 0; //D = f*h/px

        private double step = 2 * Math.PI / n_phi;
        private ushort[] rByPhi0 = new ushort[n_phi];
        private int X0 = -1, Y0 = -1; //0 scan centre
        private ushort[] rByPhi1 = new ushort[n_phi];
        private int X1 = -1, Y1 = -1; //real centre
        private int X2 = -1, Y2 = -1; //supposed centre

        private const ushort r_robot1 = r_robot + 1;
        private const ushort d_robot = 2 * r_robot1;
        private const ushort r_scan1 = r_scan + 1;
        private const ushort d_scan = 2 * r_scan1;

        private Color wallColor = Color.FromArgb(255, 255, 255);
        private Color indoorColor = Color.FromArgb(0, 0, 0);
        private Color startColor = Color.FromArgb(94, 255, 0);
        private Color routeColor = Color.FromArgb(255, 51, 0);
        private Color finishColor = Color.FromArgb(0, 222, 255);

        private byte positionCounter = 0;
        
        /// <summary>
        /// Обработчик кликов на pictureBox1 (где отрисована preciseMap).
        /// Первый клик устанавливает центра scan0, второй клик устанавливает центр scan1, третий клик устанавливает supposed центр scan1.
        /// Все эти центры могут находиться только в indoor-среде.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X * preciseMap.Width / pictureBox1.Width;
            int Y = e.Y * preciseMap.Height / pictureBox1.Height;
            if (preciseIndoorMap[X, Y].Color == indoorColor)
            {

                switch (positionCounter)
                {
                    case 0:
                        X0 = X;
                        Y0 = Y;
                        scan0 = new PixelMap(d_scan, d_scan, 0, 0, 0);
                        rByPhi0 = getScanFromPreciseMap(X, Y, scan0);
                        pictureBox2.Image = scan0.GetBitmap();
                        positionCounter++;
                        break;
                    case 1:
                        X1 = X;
                        Y1 = Y;
                        scan1 = new PixelMap(d_scan, d_scan, 0, 0, 0);
                        rByPhi1 = getScanFromPreciseMap(X, Y, scan1);
                        pictureBox3.Image = scan1.GetBitmap();
                        positionCounter++;
                        break;
                    case 2:
                        X2 = X;
                        Y2 = Y;
                        positionCounter = 0;
                        break;
                }
                //отрисовать все три центра
                //отрисовать радиусы для scan0 и scan1
                Bitmap map = preciseMap.GetBitmap();
                Pen pen;
                Graphics graphics = Graphics.FromImage(map);
                try
                {
                    if ((X0 >= 0) && (Y0 >= 0))
                    {
                        map.SetPixel(X0, Y0, startColor);
                        pen = new Pen(startColor);
                        graphics.DrawEllipse(pen, X0 - r_scan1, Y0 - r_scan1, d_scan, d_scan);
                    }
                    if ((X1 >= 0) && (Y1 >= 0))
                    {
                        map.SetPixel(X1, Y1, finishColor);
                        pen = new Pen(finishColor);
                        graphics.DrawEllipse(pen, X1 - r_scan1, Y1 - r_scan1, d_scan, d_scan);
                    }
                    if ((X2 >= 0) && (Y2 >= 0)) map.SetPixel(X2, Y2, routeColor);
                }
                catch (Exception ex) { }
                pictureBox1.Image = map;
            }
        }

        /// <summary>
        /// Возвращает скан с точной карты в заданных координатах. Также рисует этот скан в заданном PixelMap.
        /// </summary>
        /// <param name="X">X координата центра скана на точной карте</param>
        /// <param name="Y">Y координата центра скана на точной карте</param>
        /// <param name="scanBmp">Холст для отрисовки сделанного скана</param>
        /// <returns></returns>
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

        private PixelMap getPreciseIndoorMap(PixelMap preciseMap)
        {
            Bitmap preciseIndoorMapBmp = preciseMap.GetBitmap();
            SolidBrush brush = new SolidBrush(wallColor);
            Graphics graphics = Graphics.FromImage(preciseIndoorMapBmp);
            for (int i = 0; i < preciseMap.Width; i++)
            {
                for (int j = 0; j < preciseMap.Height; j++)
                {
                    if (preciseMap[i,j].Color == wallColor)
                    {
                        try
                        {
                            graphics.FillEllipse(brush, i-r_robot1, j-r_robot1, d_robot, d_robot);
                        }
                        catch (Exception ex) { }
                    }
                }
            }
        //    preciseIndoorMapBmp.Save("C:\\Adocuments\\Library\\Clapeyron_ind\\task6 map creation\\PreciseIndoorMap1.png");
            PixelMap preciseIndoorMap = new PixelMap(preciseIndoorMapBmp);
            return preciseIndoorMap;
        }
    }
}
