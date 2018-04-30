using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;

namespace MapCreation
{
    public partial class MainForm : Form
    {//TODO проверка на отсутствие белых пикселей на indoor-карте в зоне supposed пути (от scan0 до scan1) робота (пока что сектор)
        //Как рисовать мелкую картинку с BPP == 4, а не 3: в PS сохраняем как bmp, а настройки такие: Изображение->Режим->RGB, 8 бит/канал
        public MainForm()
        {
            InitializeComponent();
            button1.MouseClick += button1_MouseClick;
            environment = new Environment(@"./Maps/PreciseMap1.png"); //default map
            updateEnvironmentProjection();
            if (environment.isMapLoaded() == 1)
                mode1ManualCrosslinking = new Mode1ManualCrosslinking(this);

            /*    preciseMap = new PixelMap("C:\\Adocuments\\Library\\Clapeyron_ind\\task6 map creation\\PreciseMap1.png");
                mouseMoveMap = new PixelMap(preciseMap);
                preciseIndoorMap = getIndoorMap(preciseMap);
                drawBitmapOnPictureBox(pictureBox1, preciseMap.GetBitmap());*/
        }

        /// <summary>
        /// Загружаем новую карту для environment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.RestoreDirectory = true;
                dlg.Title = "Open Image";
                dlg.Filter = "images (*.png;*.jpg;*bmp)|*.png;*.jpg;*bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    String s = dlg.FileName;
                    environment = new Environment(s);
                    updateEnvironmentProjection();
                }
            }
        }

        public Environment environment;

        private Mode1ManualCrosslinking mode1ManualCrosslinking;



        
        /*   
             /// <summary>
             /// Думаем, что центр scan1 - это X2,Y2. Нужно вернуть X1,Y1.
             /// </summary>
             /// <returns></returns>
             private int[] getRealCoords3()
             {
                 List<int[]> across0 = new List<int[]>(); //точки из пересечения сканов
                 List<int[]> across1 = new List<int[]>();
                 List<int[]> pointsLess, pointsMore;
                 double minsum = 100000000;
                 int limXY = 10;
                 double summ;
                 double min;
                 int optX = 0, optY = 0;

                 for (int x = -limXY; x < limXY + 1; x++)
                 {
                     for (int y = -limXY; y < limXY + 1; y++)
                     {
                         computeAcrossingPoints(ref across0, ref across1, X2+x, Y2+y);
                         if (across0.Count < across1.Count)
                         {
                             pointsLess = across0;
                             pointsMore = across1;
                         }
                         else
                         {
                             pointsLess = across1;
                             pointsMore = across0;
                         }
                         summ = 0;
                         for (int k = 0; k < across1.Count; k++)
                         {
                             across1[k][0] = across1[k][0] + (X2 - X0) + x;
                             across1[k][1] = across1[k][1] + (Y2 - Y0) + y;
                         }
                         for (int i = 0; i < pointsLess.Count; i++)
                         {
                             min = 1000000;
                             for (int j = 0; j < pointsMore.Count; j++)
                             {
                                 if (min > getSquaredDistance(pointsLess[i][0], pointsLess[i][1], pointsMore[j][0], pointsMore[j][1]))
                                     min = getSquaredDistance(pointsLess[i][0], pointsLess[i][1], pointsMore[j][0], pointsMore[j][1]);
                             }
                             summ += min;
                         }
                         for (int k = 0; k < across1.Count; k++)
                         {
                             across1[k][0] = across1[k][0] - (X2 - X0) - x;
                             across1[k][1] = across1[k][1] - (Y2 - Y0) - y;
                         }
                         Console.WriteLine("(" + x + "," + y + "): " + summ);
                         if (minsum > summ)
                         {
                             minsum = summ;
                             optX = x;
                             optY = y;
                         }
                     }
                 }
                 scan1.setCenter(X2+optX,Y2+optY);
                 Console.WriteLine("minsum: "+minsum);
                 Console.WriteLine("opt coords: "+optX+","+optY);
                 Console.WriteLine("center1-center2 error: " + (X1-X2) + "," + (Y1-Y2));
                 return new int[2]{X2+optX,Y2+optY};
             }

             /// <summary>
             /// Добавляет в списки точки из пересечения сканов scan0 и scan1, при этом за центр последнего берется задаваемый X_sp и Y_sp.
             /// </summary>
             /// <param name="across0"></param>
             /// <param name="across1"></param>
             /// <param name="X_sp">Предполагаемый центр scan1</param>
             /// <param name="Y_sp">Предполагаемый центр scan1</param>
             private void computeAcrossingPoints(ref List<int[]> across0, ref List<int[]> across1, int X_sp, int Y_sp)
             {
                 across0.Clear();
                 across1.Clear();
                 int x0, y0, x1, y1;
                 for (int i = 0; i < scan0.xyScan.Count; i++)
                 {
                     x0 = scan0.xyScan[i][0];
                     y0 = scan0.xyScan[i][1];
                     if ((getSquaredDistance(x0+X0, y0+Y0, X_sp, Y_sp) <= r_scan2))
                         across0.Add(new int[2] { x0, y0 });
                 }
                 for (int i = 0; i < scan1.xyScan.Count; i++)
                 {
                     x1 = scan1.xyScan[i][0];
                     y1 = scan1.xyScan[i][1];
                     if ((getSquaredDistance(x1+X_sp, y1+Y_sp, X0, Y0) <= r_scan2))
                         across1.Add(new int[2] { x1, y1 });
                 }
             }


             private int[] getRealCoords4()
             {
                 double minsum = 100000000;
                 int limXY = 20;
                 double summ;
                 double min;
                 int optX = 0, optY = 0;
                 int C = (d_scan + r_scan) / 2;
                 for (int x = -limXY; x < limXY + 1; x++)
                 {
                     for (int y = -limXY; y < limXY + 1; y++)
                     {
                         PixelMap map01 = new PixelMap(d_scan1 + r_scan, d_scan1 + r_scan, 0, 0, 0);
                         List<int[]> irrelevantPoints0 = new List<int[]>();
                         List<int[]> irrelevantPoints1 = new List<int[]>();
                         int X = X2 - X0 + x;
                         int Y = Y2 - Y0 + y;
                         for (int i = 0; i < scan0.xyScan.Count; i++)
                         {
                             map01[scan0.xyScan[i][0] + C, scan0.xyScan[i][1] + C] = new Pixel(wallColor);
                         }
                         for (int i = 0; i < scan1.xyScan.Count; i++)
                         {
                             map01[scan1.xyScan[i][0] + X + C, scan1.xyScan[i][1] + Y + C] = new Pixel(wallColor);
                         }
                         int y1 = new int();
                         int x1 = new int();
                         bool flagR; //Будет true, если на текущем угле сканирования видно препятствие, иначе false и радиус от текущего угла будет равен нулю
                         bool flagRepeated; //Будет true, если точка уже сохранена в списке скана
                         int rPhi = -1;

                         for (int i = 0; i < n_phi; i++)
                         {
                             //---------------------------------------scan1
                             flagR = false;
                             for (ushort r = 1; r < r_scan + 1; r++)
                             {
                                 x1 = (int)Math.Round(r * Math.Cos(i * step));
                                 y1 = (int)Math.Round(r * Math.Sin(i * step));
                                 if (map01[x1 + X + C, y1 + Y + C].Color == wallColor)
                                 {
                                     rPhi = r;
                                     flagR = true;
                                     break;
                                 }
                             }
                             if (!flagR)
                                 rPhi = 0;
                             if (rPhi == -1) Console.WriteLine("Scanning problems r == -1 on scan1, angle: " + i * step / Math.PI * 180);
                             if (scan1.rByPhi[i] != rPhi)
                             {
                                 flagRepeated = false;
                                 for (int j = 0; j < irrelevantPoints1.Count; j++)
                                     if ((irrelevantPoints1[j][0] == x1) && (irrelevantPoints1[j][1] == y1))
                                         flagRepeated = true;
                                 if (!flagRepeated)
                                     irrelevantPoints1.Add(new int[2] { x1, y1 });
                             }
                             //---------------------------------------scan0
                             flagR = false;
                             for (ushort r = 1; r < r_scan + 1; r++)
                             {
                                 x1 = (int)Math.Round(r * Math.Cos(i * step));
                                 y1 = (int)Math.Round(r * Math.Sin(i * step));
                                 if (map01[x1 + C, y1 + C].Color == wallColor)
                                 {
                                     rPhi = r;
                                     flagR = true;
                                     break;
                                 }
                             }
                             if (!flagR)
                                 rPhi = 0;
                             if (rPhi == -1) Console.WriteLine("Scanning problems r == -1 on scan0, angle: " + i * step / Math.PI * 180);
                             if (scan0.rByPhi[i] != rPhi)
                             {
                                 flagRepeated = false;
                                 for (int j = 0; j < irrelevantPoints0.Count; j++)
                                     if ((irrelevantPoints0[j][0] == x1) && (irrelevantPoints0[j][1] == y1))
                                         flagRepeated = true;
                                 if (!flagRepeated)
                                     irrelevantPoints0.Add(new int[2] { x1, y1 });
                             }
                         }

                         summ = 0;
                         for (int i = 0; i < irrelevantPoints1.Count; i++)
                         {
                             min = 1000000;
                             for (int j = 0; j < scan1.xyScan.Count; j++)
                             {
                                 if (min > getSquaredDistance(irrelevantPoints1[i][0], irrelevantPoints1[i][1], scan1.xyScan[j][0], scan1.xyScan[j][1]))
                                     min = getSquaredDistance(irrelevantPoints1[i][0], irrelevantPoints1[i][1], scan1.xyScan[j][0], scan1.xyScan[j][1]);
                             }
                             summ += min;
                         }
                         for (int i = 0; i < irrelevantPoints0.Count; i++)
                         {
                             min = 1000000;
                             for (int j = 0; j < scan0.xyScan.Count; j++)
                             {
                                 if (min > getSquaredDistance(irrelevantPoints0[i][0], irrelevantPoints0[i][1], scan0.xyScan[j][0], scan0.xyScan[j][1]))
                                     min = getSquaredDistance(irrelevantPoints0[i][0], irrelevantPoints0[i][1], scan0.xyScan[j][0], scan0.xyScan[j][1]);
                             }
                             summ += min;
                         }
                         if (minsum > summ)
                         {
                             minsum = summ;
                             optX = x;
                             optY = y;
                         }
                     }
                 }
                 return new int[2] { X2 + optX, Y2 + optY };
             }

             */
    }
}
