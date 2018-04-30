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
        /// </summary>
        /// <returns></returns>
        public List<int[]> getPieSupposedZone()
        {
            return pieErrorZoneSearch(X0,Y0,X1,Y1);
        }

        /// <summary>
        /// Возвращает зону, в которой статистически должен находиться real центр скана scan1.
        /// Эта зона - кусок сектора с центром в supposed центре скана scan1. 
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
            int searchingSquareHalfSide = (int)Math.Ceiling(Math.Pow(Math.Pow(lplus3sgm * 3 * Parameters.sgm_psi_rad, 2) + Math.Pow(3 * Parameters.getSgm_l(l), 2), 0.5));

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
                if ((psi_sp_rad + 2 * Math.PI >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad + 2 * Math.PI <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                else angleFlag = false;
            }
            else
            {
                if ((psi_rl_rad < -Math.PI / 2) && (psi_sp_rad > Math.PI / 2))
                {
                    if ((psi_sp_rad - 2 * Math.PI >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad - 2 * Math.PI <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                    else angleFlag = false;
                }
                else
                {
                    if ((psi_sp_rad >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
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
                if ((psi_sp_rad + 2 * Math.PI >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad + 2 * Math.PI <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                else angleFlag = false;
            }
            else
            {
                if ((psi_rl_rad < -Math.PI / 2) && (psi_sp_rad > Math.PI / 2))
                {
                    if ((psi_sp_rad - 2 * Math.PI >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad - 2 * Math.PI <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                    else angleFlag = false;
                }
                else
                {
                    if ((psi_sp_rad >= psi_rl_rad - 3 * Parameters.sgm_psi_rad) && (psi_sp_rad <= psi_rl_rad + 3 * Parameters.sgm_psi_rad)) angleFlag = true;
                }
            }
            if (((l_sp >= l_rlMinus3sgm) && (l_sp <= l_rlPlus3sgm)) && angleFlag)
                return true;
            else
                return false;
        }
    }
}
