﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace MapCreation
{
    /// <summary>
    /// Карта помещения - общая для всех режимов приложения
    /// </summary>
    public class Environment
    {
        public Environment(String s)
        {
            if (s != null)
            {
                try
                {
                    preciseMap = new PixelMap(s);
                    preciseIndoorMap = calculateIndoorMap(preciseMap);

                    loaded = 1;
                }
                catch (System.IO.FileNotFoundException e)
                {
                    loaded = 0;
                }
                catch (FormatException e)
                {
                    loaded = 2;
                }
            }
            else
                loaded = 0;
        }

        /// <summary>
        /// Точная карта
        /// </summary>
        public PixelMap preciseMap;

        /// <summary>
        /// Точная карта indoor-среды: с отступами от препятствий точной карты
        /// </summary>
        private PixelMap preciseIndoorMap;

        /// <summary>
        /// Составленная роботом карта
        /// </summary>
        public PixelMap predictedMap;

        /// <summary>
        /// Если файл оригинальной карты существует по указанному в конструкторе пути, то true.
        /// Иначе карта не загружена и false.
        /// </summary>
        private byte loaded;

        /// <summary>
        /// Номер варианта зашумления радиуса со сканера. Больше номер - больше зашумление.
        /// </summary>
        private static byte r_scanNoiseMode = 1;

        /// <summary>
        /// Возвращает скан с точной карты в заданных координатах
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="scanColor">цвет скана для отрисовки</param>
        /// <returns></returns>
        public Scan getScan(int X,int Y,Color scanColor)
        {
            Scan scan = new Scan();
            int y = new int();
            int x = new int();
            bool flagR; //Будет true, если на текущем угле сканирования видно препятствие, иначе false и радиус от текущего угла будет равен нулю
            bool flagRepeated; //Будет true, если точка уже сохранена в списке скана

            Random rand = new Random(System.DateTime.Now.Millisecond);
        //    Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < Parameters.getN_phi(); i++)
            {
                flagR = false;
                for (ushort r = 1; r < Parameters.getR_scan1(); r++)
                {
                    x = (int)Math.Round(r * Math.Cos(i * Parameters.getScan_step()));
                    y = (int)Math.Round(r * Math.Sin(i * Parameters.getScan_step()));
                    if (preciseMap[x + X, y + Y].Color == Parameters.wallColor)
                    {
                        scan.rByPhi[i] = r;
                        flagRepeated = false;
                        for (int j = 0; j < scan.xyScan.Count; j++)
                            if ((scan.xyScan[j][0] == x) && (scan.xyScan[j][1] == y))
                                flagRepeated = true;
                        if (!flagRepeated)
                        {
                            switch(r_scanNoiseMode)
                            {
                                case 1:
                                    r = rNoising1(ref r, ref rand);
                                    break;
                                case 2:
                                    r = rNoising2(ref r, ref rand);
                                    break;
                                case 3:
                                    r = rNoising3(ref r, ref rand);
                                    break;
                            }

                            x = (int)Math.Round(r * Math.Cos(i * Parameters.getScan_step()));
                            y = (int)Math.Round(r * Math.Sin(i * Parameters.getScan_step()));
                            scan.xyScan.Add(new int[2] { x, y });
                        }
                        scan.scanBmp[x + Parameters.getR_scan(), y + Parameters.getR_scan()] = new Pixel(scanColor);
                        flagR = true;
                        break;
                    }
                }
                if (!flagR)
                    scan.rByPhi[i] = 0;
            }

         //   stopwatch.Stop();
        //    Console.WriteLine("Time wasted: "+stopwatch.ElapsedMilliseconds);
            return scan;
        }

        public bool canRobotStayOnThisPoint(int X, int Y)
        {
            if (preciseIndoorMap[X, Y].Color == Parameters.indoorColor)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Если файл оригинальной карты существует по указанному в конструкторе пути, то true.
        /// Иначе карта не загружена и false.
        /// </summary>
        public byte isMapLoaded()
        {
            return loaded;
        }

        public Bitmap getPreciseBmp()
        {
            return preciseMap.GetBitmap();
        }

        /// <summary>
        /// Рассчитывает карту indoor-среды для заданной карты.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private static PixelMap calculateIndoorMap(PixelMap map)
        {
            Bitmap preciseIndoorMapBmp = map.GetBitmap();
            Pen pen = new Pen(Parameters.wallColor);
            SolidBrush brush = new SolidBrush(Parameters.wallColor);
            Graphics graphics = Graphics.FromImage(preciseIndoorMapBmp);
            int r = Parameters.getR_robot() + Parameters.getR_robot() / 2; //увеличенные размеры робота, для того, чтобы на угловых участках траектории сглаживание происходило без проблем
            int d = Parameters.getD_robot() + Parameters.getR_robot();
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    if (map[i, j].Color == Parameters.wallColor)
                    {
                        try
                        {
                            FillCircle(ref graphics, ref pen, ref brush, r, d, ref i, ref j);
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            //    preciseIndoorMapBmp.Save("C:\\Adocuments\\Library\\Clapeyron_ind\\task6 map creation\\PreciseIndoorMap13.png");
            PixelMap preciseIndoorMap = new PixelMap(preciseIndoorMapBmp);
            return preciseIndoorMap;
        }
        
        /// <summary>
        /// Заполняет круг радиуса r, работает правильно. d должно быть равно 2r (для скорости).
        /// pen и brush должны быть одного цвета.
        /// Метод скоростной.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="r">радиус круга</param>
        /// <param name="d">2r</param>
        /// <param name="X">центр круга</param>
        /// <param name="Y">центр круга</param>
        /// <param name="color">заливка и граница круга</param>
        private static void FillCircle(ref Graphics graphics, ref Pen pen, ref SolidBrush brush, int r, int d, ref int X, ref int Y)
        {
            graphics.DrawEllipse(pen, X - r, Y - r, d, d);
            graphics.FillEllipse(brush, X - r, Y - r, d, d);
        }

        /// <summary>
        /// Зашумляет радиус
        /// </summary>
        /// <returns></returns>
        private ushort rNoising1(ref ushort r, ref Random rand)
        {
            //пока что просто добавляю к текущему значению радиуса +1 или -1
            //равновероятно, с вероятностью 0.1
            int er = rand.Next(-1,9); //9 не входит сюда
            if (Math.Abs(er) == 1)
                return (ushort)(r + er);
            else
                return r;
        }

        private ushort rNoising2(ref ushort r, ref Random rand)
        {
            //пока что просто добавляю к текущему значению радиуса +1 или -1
            //равновероятно, с вероятностью 0.1
            int er = rand.Next(-1, 3);
            if (Math.Abs(er) == 1)
                return (ushort)(r + er);
            else
                return r;
        }

        private ushort rNoising3(ref ushort r, ref Random rand)
        {
            //пока что просто добавляю к текущему значению радиуса +1 или -1
            //равновероятно, с вероятностью 0.1
            int er = rand.Next(0, 16);
            int err = 0;
            if (er < 2)
                err = -1;
            else
            {
                if (er < 4)
                    err = 1;
                else
                {
                    if (er < 5)
                        err = -2;
                    else
                    {
                        if (er < 6)
                            err = 2;
                    }
                }
            }
            return (ushort)(r + err);
        }

        public void recalculateIndoorMap()
        {
            preciseIndoorMap = calculateIndoorMap(preciseMap);
        }
        
        public static void setR_scanNoiseMode(byte r_scanNoiseMode)
        {
            Environment.r_scanNoiseMode = r_scanNoiseMode;
        }

        public static int getR_scanNoiseMode()
        {
            return r_scanNoiseMode;
        }
    }
}
