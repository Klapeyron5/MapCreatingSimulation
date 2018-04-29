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
        /// 
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <returns></returns>
        public List<int[]> pieErrorZoneSearch(int X1, int Y1, int X2, int Y2)
        {
            List<int[]> pieErrorZone = new List<int[]>();
            double l2 = Parameters.getSquaredDistance(X1, Y1, X2, Y2);
            double l = Math.Pow(l2, 0.5);
            double lminus3sgm = l - 3 * Parameters.getSgm_l(l);
            double lplus3sgm = l + 3 * Parameters.getSgm_l(l);

            int searchingSquareHalfSide = (int)Math.Ceiling(Math.Pow(Math.Pow(lplus3sgm * 3 * Parameters.sgm_psi_rad, 2) + Math.Pow(3 * Parameters.getSgm_l(l), 2), 0.5));
            Console.WriteLine("searchingSquareHalfSide " + searchingSquareHalfSide);

            for (int x = -searchingSquareHalfSide; x <= searchingSquareHalfSide; x++)
                for (int y = -searchingSquareHalfSide; y <= searchingSquareHalfSide; y++)
                    if (isPointInPieZone(X0, Y0, X2 + x, Y2 + y))
                        pieErrorZone.Add(new int[2] { X2 + x, Y2 + y });

            return pieErrorZone;
        }

        private bool isPointInPieZone(int X1, int Y1, int X2, int Y2)
        {
            double l_sp2 = Parameters.getSquaredDistance(X1, Y1, X2, Y2);
            double l_sp = Math.Pow(l_sp2, 0.5);
            double psi_sp_rad = Parameters.getAngleRadian(X1, Y1, X2, Y2);
            double psi_rl_rad = Parameters.getAngleRadian(getXY0(), getXY1());
            bool angleFlag = false; //входит ли по угловой зоне
            
            double l_rl2 = Parameters.getSquaredDistance(getXY0(), getXY1());
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
