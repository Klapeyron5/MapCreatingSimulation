using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    class Crosslinker
    {
        public Crosslinker()
        {
        }

        public Scan scan0;
        public Scan scan1 = new Scan();

        private int X0 = -1, Y0 = -1; //0 scan center
        private int X1 = -1, Y1 = -1; //real 1 scan center
        private int X2 = -1, Y2 = -1; //supposed 1 scan center

        public void setCenter0(int X, int Y)
        {
            X0 = X;
            Y0 = Y;
            scan0 = new Scan();
        }
        
        public void setCenter1(int X, int Y)
        {
            X1 = X;
            Y1 = Y;
            scan1 = new Scan();
        }
        
        public void setCenter2(int X, int Y)
        {
            X2 = X;
            Y2 = Y;
        }

        public int[] getXY0()
        {
            return new int[2] { X0, Y0 };
        }

        public int[] getXY1()
        {
            return new int[2] { X1, Y1 };
        }

        public int[] getXY2()
        {
            return new int[2] { X2, Y2 };
        }

        /// <summary>
        /// Возвращает зону, в которой статистически может находиться supposed центр.
        /// Эта зона - кусок сектора с центром в real центре скана scan1. 
        /// Зона в абсолютных координатах preciseMap
        /// </summary>
        /// <returns></returns>
        public List<int[]> getPieSupposedZone()
        {
            return pieErrorZoneSearch(X0,Y0,X1,Y1);
        }

        /// <summary>
        /// Возвращает зону, в которой статистически должен находиться real центр скана scan1.
        /// Эта зона - кусок сектора с центром в supposed центре скана scan1. 
        /// Зона в абсолютных координатах preciseMap
        /// </summary>
        /// <returns></returns>
        public List<int[]> getPieRealZone()
        {
            return pieErrorZoneSearch(X0, Y0, X2, Y2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private List<int[]> pieErrorZoneSearch(int x1, int y1, int x2, int y2)
        {
            List<int[]> pieErrorZone = new List<int[]>();
            double l2 = Parameters.getSquaredDistance(x1, y1, x2, y2);
            double l = Math.Pow(l2, 0.5);
            double lminus3sgm = l - 3 * Parameters.getSgm_l(l);
            double lplus3sgm = l + 3 * Parameters.getSgm_l(l);

            double psi_rad = Parameters.getAngleRadian(x1,y1,x2,y2);

            //половина стороны квадрата, в который точно вписан искомый сектор
            int searchingSquareHalfSide = (int)Math.Ceiling(Math.Pow(Math.Pow(lplus3sgm * 3 * Parameters.getSgm_psi_rad(), 2) + Math.Pow(3 * Parameters.getSgm_l(l), 2), 0.5));

            double l_rl2 = Parameters.getSquaredDistance(X0, Y0, X1, Y1);
            double l_rl = Math.Pow(l_rl2, 0.5);
            double sgm_lrl = Parameters.getSgm_l(l_rl);
            double l_rlPlus3sgm = l_rl + 3 * sgm_lrl;
            double l_rlMinus3sgm = l_rl - 3 * sgm_lrl;
            for (int x = -searchingSquareHalfSide; x <= searchingSquareHalfSide; x++)
                for (int y = -searchingSquareHalfSide; y <= searchingSquareHalfSide; y++)
                {
                    double l_sp2 = Parameters.getSquaredDistance(x1, y1, x2 + x, y2 + y);
                    double l_sp = Math.Pow(l_sp2, 0.5);
                    double psi_sp_rad = Parameters.getAngleRadian(x1, y1, x2 + x, y2 + y);

                    if (isPointInPieZoneExpress(ref l_sp, ref psi_sp_rad, ref psi_rad, ref l_rlMinus3sgm, ref l_rlPlus3sgm))
                        pieErrorZone.Add(new int[2] { x2 + x, y2 + y });
                }

            return pieErrorZone;
        }

        /// <summary>
        /// Находится ли точка в зоне ошибки?
        /// Метод для быстрого расчета в цикле.
        /// </summary>
        /// <param name="l_sp">Расстояние от центра scan0 до проверяемой точки</param>
        /// <param name="psi_sp_rad">Угол между центром scan0 и проверяемой точкой</param>
        /// <param name="psi_rl_rad">Угол между центром scan0 и центральной точкой зоны ошибки (центр второго скана)</param>
        /// <param name="l_rlMinus3sgm">Допустимая верхняя граница радиуса сектора зоны ошибки</param>
        /// <param name="l_rlPlus3sgm">Допустимая нижняя граница радиуса сектора зоны ошибки</param>
        /// <returns></returns>
        private bool isPointInPieZoneExpress(ref double l_sp, ref double psi_sp_rad, ref double psi_rl_rad, ref double l_rlMinus3sgm, ref double l_rlPlus3sgm)
        {
            bool angleFlag = false; //входит ли по угловой зоне
            
            if ((psi_rl_rad > Math.PI / 2) && (psi_sp_rad < -Math.PI / 2))
            {
                if ((psi_sp_rad + 2 * Math.PI >= psi_rl_rad - 3 * Parameters.getSgm_psi_rad()) && (psi_sp_rad + 2 * Math.PI <= psi_rl_rad + 3 * Parameters.getSgm_psi_rad())) angleFlag = true;
                else angleFlag = false;
            }
            else
            {
                if ((psi_rl_rad < -Math.PI / 2) && (psi_sp_rad > Math.PI / 2))
                {
                    if ((psi_sp_rad - 2 * Math.PI >= psi_rl_rad - 3 * Parameters.getSgm_psi_rad()) && (psi_sp_rad - 2 * Math.PI <= psi_rl_rad + 3 * Parameters.getSgm_psi_rad())) angleFlag = true;
                    else angleFlag = false;
                }
                else
                {
                    if ((psi_sp_rad >= psi_rl_rad - 3 * Parameters.getSgm_psi_rad()) && (psi_sp_rad <= psi_rl_rad + 3 * Parameters.getSgm_psi_rad())) angleFlag = true;
                }
            }
            if (((l_sp >= l_rlMinus3sgm) && (l_sp <= l_rlPlus3sgm)) && angleFlag)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Находится ли точка в supposed зоне ошибки относительно scan0, scan1 real ?
        /// </summary>
        /// <returns></returns>
        public bool isPointInSupposedZone(int x2, int y2)
        {
            return isPointInPieZone(X0,Y0,X1,Y1,x2,y2);
        }

        /// <summary>
        /// Находится ли точка в зоне ошибки?
        /// </summary>
        /// <param name="x0">Центр первого скана</param>
        /// <param name="y0">Центр первого скана</param>
        /// <param name="x1">Центр зоны ошибки (центр второго скана)</param>
        /// <param name="y1">Центр зоны ошибки (центр второго скана)</param>
        /// <param name="x2">Проверяемая точка</param>
        /// <param name="y2">Проверяемая точка</param>
        /// <returns></returns>
        private bool isPointInPieZone(int x0, int y0, int x1, int y1, int x2, int y2)
        {
            //пока простая проверка: ровный разброс по углу и по длине
            double l_sp2 = Parameters.getSquaredDistance(x0,y0,x2,y2);
            double l_sp = Math.Pow(l_sp2, 0.5);
            double psi_rl_rad = Parameters.getAngleRadian(x0,y0,x1,y1);
            double psi_sp_rad = Parameters.getAngleRadian(x0,y0,x2,y2);
            bool angleFlag = false; //входит ли по угловой зоне

            double l_rl2 = Parameters.getSquaredDistance(x0,y0,x1,y1);
            double l_rl = Math.Pow(l_rl2, 0.5);
            double sgm_lrl = Parameters.getSgm_l(l_rl);
            double l_rlPlus3sgm = l_rl + 3 * sgm_lrl;
            double l_rlMinus3sgm = l_rl - 3 * sgm_lrl;
            if ((psi_rl_rad > Math.PI / 2) && (psi_sp_rad < -Math.PI / 2))
            {
                if ((psi_sp_rad + 2 * Math.PI >= psi_rl_rad - 3 * Parameters.getSgm_psi_rad()) && (psi_sp_rad + 2 * Math.PI <= psi_rl_rad + 3 * Parameters.getSgm_psi_rad())) angleFlag = true;
                else angleFlag = false;
            }
            else
            {
                if ((psi_rl_rad < -Math.PI / 2) && (psi_sp_rad > Math.PI / 2))
                {
                    if ((psi_sp_rad - 2 * Math.PI >= psi_rl_rad - 3 * Parameters.getSgm_psi_rad()) && (psi_sp_rad - 2 * Math.PI <= psi_rl_rad + 3 * Parameters.getSgm_psi_rad())) angleFlag = true;
                    else angleFlag = false;
                }
                else
                {
                    if ((psi_sp_rad >= psi_rl_rad - 3 * Parameters.getSgm_psi_rad()) && (psi_sp_rad <= psi_rl_rad + 3 * Parameters.getSgm_psi_rad())) angleFlag = true;
                }
            }
            if (((l_sp >= l_rlMinus3sgm) && (l_sp <= l_rlPlus3sgm)) && angleFlag)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Думаем, что центр scan1 - это X2,Y2. Нужно вернуть X1,Y1.
        /// </summary>
        /// <returns>Возвращает массив int[3]: X,Y,error: координаты настоящего центра scan1 и квадрат расстояния от него до реального симуляционного центра scan1</returns>
        public int[] getRealCoords4()
        {
            Console.WriteLine("crosslinkig " + X0 + "," + Y0 + ";"
                + X1 + "," + Y1 + ";" + X2 + "," + Y2);
            double minsum = 100000000;
            int limXY = 20;
            double summ;
            double min;
            int optX = 0, optY = 0;
            int C = (Parameters.getD_scan() + Parameters.getR_scan()) / 2;
            List<int[]> errorZone = pieErrorZoneSearch(0,0,X2-X0,Y2-Y0);
            //    Console.WriteLine("errorZone.Count "+ errorZone.Count);
            int numberOfMinPoints = 0; //количество точек с одинаковыми минимальными значениями функционала ошибки //TODO
            for(int k = 0; k < errorZone.Count; k++)
            {
                PixelMap map01 = new PixelMap(Parameters.getD_scan1() + Parameters.getR_scan(), Parameters.getD_scan1() + Parameters.getR_scan(), 0, 0, 0);
                List<int[]> irrelevantPoints0 = new List<int[]>();
                List<int[]> irrelevantPoints1 = new List<int[]>();
                int X = errorZone[k][0];
                int Y = errorZone[k][1];
                for (int i = 0; i < scan0.xyScan.Count; i++)
                {
                    map01[scan0.xyScan[i][0] + C, scan0.xyScan[i][1] + C] = new Pixel(Parameters.wallColor);
                }
                for (int i = 0; i < scan1.xyScan.Count; i++)
                {
                    map01[scan1.xyScan[i][0] + X + C, scan1.xyScan[i][1] + Y + C] = new Pixel(Parameters.wallColor);
                }
                int y1 = new int();
                int x1 = new int();
                bool flagR; //Будет true, если на текущем угле сканирования видно препятствие, иначе false и радиус от текущего угла будет равен нулю
                bool flagRepeated; //Будет true, если точка уже сохранена в списке скана
                int rPhi = -1;

                for (int i = 0; i < Parameters.getN_phi(); i++)
                {
                    //---------------------------------------scan1
                    flagR = false;
                    for (ushort r = 1; r < Parameters.getR_scan() + 1; r++)
                    {
                        x1 = (int)Math.Round(r * Math.Cos(i * Parameters.getScan_step()));
                        y1 = (int)Math.Round(r * Math.Sin(i * Parameters.getScan_step()));
                        if (map01[x1 + X + C, y1 + Y + C].Color == Parameters.wallColor)
                        {
                            rPhi = r;
                            flagR = true;
                            break;
                        }
                    }
                    if (!flagR)
                        rPhi = 0;
                    if (rPhi == -1) Console.WriteLine("Scanning problems r == -1 on scan1, angle: " + i * Parameters.getScan_step() / Math.PI * 180);
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
                    for (ushort r = 1; r < Parameters.getR_scan() + 1; r++)
                    {
                        x1 = (int)Math.Round(r * Math.Cos(i * Parameters.getScan_step()));
                        y1 = (int)Math.Round(r * Math.Sin(i * Parameters.getScan_step()));
                        if (map01[x1 + C, y1 + C].Color == Parameters.wallColor)
                        {
                            rPhi = r;
                            flagR = true;
                            break;
                        }
                    }
                    if (!flagR)
                        rPhi = 0;
                    if (rPhi == -1) Console.WriteLine("Scanning problems r == -1 on scan0, angle: " + i * Parameters.getScan_step() / Math.PI * 180);
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
                        if (min > Parameters.getSquaredDistance(irrelevantPoints1[i][0], irrelevantPoints1[i][1], scan1.xyScan[j][0], scan1.xyScan[j][1]))
                            min = Parameters.getSquaredDistance(irrelevantPoints1[i][0], irrelevantPoints1[i][1], scan1.xyScan[j][0], scan1.xyScan[j][1]);
                    }
                    summ += min;
                }
                for (int i = 0; i < irrelevantPoints0.Count; i++)
                {
                    min = 1000000;
                    for (int j = 0; j < scan0.xyScan.Count; j++)
                    {
                        if (min > Parameters.getSquaredDistance(irrelevantPoints0[i][0], irrelevantPoints0[i][1], scan0.xyScan[j][0], scan0.xyScan[j][1]))
                            min = Parameters.getSquaredDistance(irrelevantPoints0[i][0], irrelevantPoints0[i][1], scan0.xyScan[j][0], scan0.xyScan[j][1]);
                    }
                    summ += min;
                }
                if (minsum > summ)
                {
                    minsum = summ;
                    optX = X;
                    optY = Y;
                    numberOfMinPoints = 0; //TODO
                }
                if (summ == minsum) //TODO
                {
                    numberOfMinPoints++;
                }
            }
            int errorSquredDistance = Parameters.getSquaredDistance(X1,Y1,X0+optX,Y0+optY); //квадрат раастояния от реального положения до предсказанного
            //
        //    Console.WriteLine("opts "+optX+","+optY);
            if (errorSquredDistance != 0) //TODO
            {
                Console.WriteLine("Don't match--------------------------------------");
            }
            return new int[5] { X0 + optX, Y0 + optY, errorSquredDistance, errorZone.Count(), numberOfMinPoints }; //TODO
        }
    }
}
