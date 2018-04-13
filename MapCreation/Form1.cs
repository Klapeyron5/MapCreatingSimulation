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
            preciseIndoorMap = getIndoorMap(preciseMap);
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
        private const ushort sgm_lmax = 1; //3px = 12cm
        private const int sgm_psi_deg = 2;//in degrees: 2*3.14/180*1.5m=0.05m  //0.046; //3*0.046=0.14rad (~20cm)
        private const double sgm_psi_rad = sgm_psi_deg*Math.PI/180;
    //    private const ushort sgm_r = 0; //D = f*h/px

        private double step = 2 * Math.PI / n_phi;
        private ushort[] rByPhi0 = new ushort[n_phi];
        private int X0 = -1, Y0 = -1; //0 scan center
        private ushort[] rByPhi1 = new ushort[n_phi];
        private int X1 = -1, Y1 = -1; //real 1 scan center
        private int X2 = -1, Y2 = -1; //supposed 1 scan center

        private const ushort r_robot1 = r_robot + 1;
        private const ushort d_robot = 2 * r_robot1;
        private const ushort r_scan1 = r_scan + 1;
        private const ushort d_scan = 2 * r_scan1;
        private const ushort l_max2 = l_max  * l_max;

        private Color wallColor = Color.FromArgb(255, 255, 255);
        private Color indoorColor = Color.FromArgb(0, 0, 0);
        private Color startColor = Color.FromArgb(94, 255, 0);
        private Color routeColor = Color.FromArgb(255, 51, 0);
        private Color finishColor = Color.FromArgb(0, 222, 255);

        private byte positionCounter = 0;
        private double l_rl = 0; //[0,l_max]
        private int l_rl_rounded = 0;
        private int l_rl2 = 0; //[0,l_max^2]
        private double psi_rl_rad = 0; //in radians
        private double psi_rl_deg = 0; //in degrees
        private double l_rlPlus3sgm;
        private double l_rlMinus3sgm;


        private double l_sp = 0; //[0,l_max]
    //    private int l_rl_rounded = 0;
        private int l_sp2 = 0; //[0,l_max^2]
        private double psi_sp_rad = 0; //in radians
    //    private double psi_rl_deg = 0; //in degrees

        /// <summary>
        /// Обработчик кликов на pictureBox1 (где отрисована preciseMap).
        /// Первый клик устанавливает центр нулевого скана
        /// Второй клик устанавливает центр первого скана: расстояние от центра scan1 до центра scan0 должно быть меньше l_max
        /// Третий клик устанавливает supposed центр скана 1: может быть установлен только в зоне погрешности скана 1
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
                Bitmap preciseMapBmp = preciseMap.GetBitmap();
                Pen pen;
                SolidBrush brush;
                Graphics graphics = Graphics.FromImage(preciseMapBmp);
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
                        l_rl2 = getSquaredDistance(X0, Y0, X, Y);
                        l_rl = Math.Pow(l_rl2,0.5);
                        l_rl_rounded = (int)Math.Round(l_rl);
                        psi_rl_rad = Math.Atan2(Y-Y0,X-X0);
                        psi_rl_deg = psi_rl_rad*180/Math.PI;
                        Console.WriteLine(psi_rl_deg);
                        if (l_rl2<=l_max2) {
                            X1 = X;
                            Y1 = Y;
                            scan1 = new PixelMap(d_scan, d_scan, 0, 0, 0);
                            rByPhi1 = getScanFromPreciseMap(X, Y, scan1);
                            pictureBox3.Image = scan1.GetBitmap();
                            positionCounter++;
                            drawPieZone(graphics);
                        }
                        break;
                    case 2:
                        //пока простая проверка: ровный разброс по углу и по длине
                        l_sp2 = getSquaredDistance(X0, Y0, X, Y);
                        l_sp = Math.Pow(l_sp2, 0.5);
                        psi_sp_rad = Math.Atan2(Y - Y0, X - X0);
                        bool angleFlag = false; //входит ли по угловой зоне
                        if ((psi_rl_rad > Math.PI / 2) && (psi_sp_rad < -Math.PI / 2))
                        {
                            if ((psi_sp_rad + 2 * Math.PI >= psi_rl_rad - 3 * sgm_psi_rad) && (psi_sp_rad + 2 * Math.PI <= psi_rl_rad + 3 * sgm_psi_rad)) angleFlag = true;
                            else angleFlag = false;
                        }
                        else {
                            if ((psi_rl_rad < -Math.PI / 2) && (psi_sp_rad > Math.PI / 2))
                            {
                                if ((psi_sp_rad - 2 * Math.PI >= psi_rl_rad - 3 * sgm_psi_rad) && (psi_sp_rad - 2 * Math.PI <= psi_rl_rad + 3 * sgm_psi_rad)) angleFlag = true;
                                else angleFlag = false;
                            }
                            else
                            {
                                if ((psi_sp_rad >= psi_rl_rad - 3 * sgm_psi_rad) && (psi_sp_rad <= psi_rl_rad + 3 * sgm_psi_rad)) angleFlag = true;
                            }
                        }
                        if (((l_sp>=l_rlMinus3sgm)&&(l_sp<=l_rlPlus3sgm))&& angleFlag)
                        {
                            X2 = X;
                            Y2 = Y;
                            positionCounter = 0;
                        }
                        else
                            drawPieZone(graphics);
                        break;
                }
                //отрисовать все три центра
                //отрисовать радиусы для scan0 и scan1
                try
                {
                    if ((X0 >= 0) && (Y0 >= 0))
                    {
                        preciseMapBmp.SetPixel(X0, Y0, startColor);
                        pen = new Pen(startColor);
                        graphics.DrawEllipse(pen, X0 - r_scan1, Y0 - r_scan1, d_scan, d_scan);
                        graphics.DrawEllipse(pen, X0 - r_robot1, Y0 - r_robot1, d_robot, d_robot);
                    }
                    if ((X1 >= 0) && (Y1 >= 0))
                    {
                        preciseMapBmp.SetPixel(X1, Y1, finishColor);
                        pen = new Pen(finishColor);
                        graphics.DrawEllipse(pen, X1 - r_scan1, Y1 - r_scan1, d_scan, d_scan);
                        graphics.DrawEllipse(pen, X1 - r_robot1, Y1 - r_robot1, d_robot, d_robot);
                    }
                    if ((X2 >= 0) && (Y2 >= 0))
                    {
                        preciseMapBmp.SetPixel(X2, Y2, routeColor);
                    }
                }
                catch (Exception ex) { }
                pictureBox1.Image = preciseMapBmp;
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

        /// <summary>
        /// Возвращает карту indoor-среды для заданной карты.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private PixelMap getIndoorMap(PixelMap map)
        {
            Bitmap preciseIndoorMapBmp = map.GetBitmap();
            SolidBrush brush = new SolidBrush(wallColor);
            Graphics graphics = Graphics.FromImage(preciseIndoorMapBmp);
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    if (map[i,j].Color == wallColor)
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

        /// <summary>
        /// Возвращает квадрат расстояния между точками.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private int getSquaredDistance(int x1,int y1,int x2,int y2)
        {
            return ((x1-x2)*(x1-x2)+ (y1 - y2) * (y1 - y2));
        }
        
        /// <summary>
        /// Рисует зону, в которой может быть supposed положение робота относительно real положения в центре scan1.
        /// </summary>
        /// <param name="graphics"></param>
        private void drawPieZone (Graphics graphics)
        {
            //отрисовать зону погрешности, в которую попадает supposed центр
            //  graphics.DrawEllipse(pen, X0 - l_rl_rounded, Y0 - l_rl_rounded, 2 * l_rl_rounded, 2 * l_rl_rounded);
            double sgm_lrl = l_rl * sgm_lmax / l_max; //вычисляем погрешность передвижения, считая зависимость погрешности от пройденного расстояния линейной
            l_rlPlus3sgm = l_rl + 3 * sgm_lrl;
            l_rlMinus3sgm = l_rl - 3 * sgm_lrl;
            int lplus3sgmInt = (int)Math.Round(l_rlPlus3sgm);
            int lminus3sgmInt = (int)Math.Round(l_rlMinus3sgm);
            SolidBrush brush = new SolidBrush(routeColor);
            graphics.FillPie(brush, X0 - lplus3sgmInt - 1, Y0 - lplus3sgmInt - 1, 2 * lplus3sgmInt + 2, 2 * lplus3sgmInt + 2, (float)psi_rl_deg - 3 * sgm_psi_deg, 6 * sgm_psi_deg);
            brush = new SolidBrush(indoorColor);
            graphics.FillPie(brush, X0 - lminus3sgmInt, Y0 - lminus3sgmInt, 2 * lminus3sgmInt, 2 * lminus3sgmInt, (float)psi_rl_deg - 3 * sgm_psi_deg, 6 * sgm_psi_deg);
        }
    }
}
