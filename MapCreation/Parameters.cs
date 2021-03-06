﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    /// <summary>
    /// Здесь храняться параметры симуляции, некоторые параметры приложения, а также некоторые статические методы.
    /// </summary>
    static class Parameters
    {
        //Simulation parameters
        //Все расстояния - это от центра пикселя до центра пикселя
        //Т.е. между ближайшими краями двух пикселей лежит (расстояние между этими пикселями-1) пикселей
        private static int n_phi = 250;
        private static int r_robot = 6;//6px = 25cm
        private static int r_scan = 70;//70; //25cm*12=3m; 6px*12=72px ~ 70+1
        private static int l_max = r_scan/2; //1.5m
        private static int sgm_lmax = 3;//1; //3px = 12cm
        private static int sgm_psi_deg = 4;//2;//in degrees: 2*3.14/180*1.5m=0.05m  //0.046; //3*0.046=0.14rad (~20cm)
        private static double sgm_psi_rad = sgm_psi_deg * Math.PI / 180;

        private static double step = 2 * Math.PI / n_phi; //для скана

        private static int d_robot = 2 * r_robot;
        private static int r_scan1 = r_scan + 1;
        private static int r_scan2 = r_scan * r_scan;
        private static int d_scan = 2 * r_scan;
        private static int d_scan1 = d_scan + 1;
        private static int l_max2 = l_max * l_max;

        public static Color wallColor = Color.FromArgb(255, 255, 255);
        public static Color indoorColor = Color.FromArgb(0, 0, 0);
        public static Color startColor = Color.FromArgb(94, 255, 0);
        public static Color routeColor = Color.FromArgb(255, 51, 0);
        public static Color finishColor = Color.FromArgb(0, 222, 255);
        public static Color predictionColor = Color.FromArgb(0, 26, 255);
        public static Color mouseMoveColor = Color.FromArgb(255, 255, 0);

        /// <summary>
        /// Возвращает квадрат расстояния между точками.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static int getSquaredDistance(int x1, int y1, int x2, int y2)
        {
            return ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        
        public static int getSquaredDistance(int[] xy1, int x2, int y2)
        {
            return ((xy1[0] - x2) * (xy1[0] - x2) + (xy1[1] - y2) * (xy1[1] - y2));
        }

        public static int getSquaredDistance(int[] xy1, int[] xy2)
        {
            return ((xy1[0] - xy2[0]) * (xy1[0] - xy2[0]) + (xy1[1] - xy2[1]) * (xy1[1] - xy2[1]));
        }

        /// <summary>
        /// 00 ====>x
        /// ||
        /// ||
        /// \/
        /// y
        /// Угол в диапазоне [-pi,pi]. Направление 0 по оси x.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double getAngleRadian(int x1, int y1, int x2, int y2)
        {
            return Math.Atan2(y2 - y1, x2 - x1);
        }

        public static double getAngleRadian(int[] xy1, int x2, int y2)
        {
            return Math.Atan2(y2 - xy1[1], x2 - xy1[0]);
        }

        public static double getAngleRadian(int[] xy1, int[] xy2)
        {
            return Math.Atan2(xy2[1] - xy1[1], xy2[0] - xy1[0]);
        }

        /// <summary>
        /// Вычисляем погрешность передвижения, считая зависимость погрешности от пройденного расстояния линейной.
        /// </summary>
        /// <param name="l">Пройденное расстояние.</param>
        /// <returns></returns>
        public static double getSgm_l(double l)
        {
            return l * sgm_lmax / l_max;
        }

        public static void changeParameter()
        {
            //mainForm recreate current Mode
        }

        public static void setN_phi(int n_phi)
        {
            Parameters.n_phi = n_phi;
            Parameters.step = 2 * Math.PI / n_phi;
    }

        public static void setR_scan(int r_scan)
        {
            Parameters.r_scan = r_scan;
            Parameters.r_scan1 = r_scan + 1;
            Parameters.r_scan2 = r_scan * r_scan;
            Parameters.d_scan = 2 * r_scan;
            Parameters.d_scan1 = d_scan + 1;
            Parameters.l_max = r_scan / 2;
            Parameters.l_max2 = l_max* l_max;
        }

        public static void setR_robot(int r_robot)
        {
            Parameters.r_robot = r_robot;
            Parameters.d_robot = 2 * r_robot;
        }

        public static void setSgm_lmax(int sgm_lmax)
        {
            Parameters.sgm_lmax = sgm_lmax;
        }

        public static void setSgm_psi_deg(int sgm_psi_deg)
        {
            Parameters.sgm_psi_deg = sgm_psi_deg;
            Parameters.sgm_psi_rad = sgm_psi_deg * Math.PI / 180;
        }

        public static int getR_scan()
        {
            return r_scan;
        }

        public static int getR_scan1()
        {
            return r_scan1;
        }

        public static int getD_scan()
        {
            return d_scan;
        }

        public static int getD_scan1()
        {
            return d_scan1;
        }

        public static int getL_max()
        {
            return l_max;
        }

        public static int getL_max2()
        {
            return l_max2;
        }
        
        public static int getR_robot()
        {
            return r_robot;
        }

        public static int getD_robot()
        {
            return d_robot;
        }

        public static int getSgm_lmax()
        {
            return sgm_lmax;
        }

        public static int getN_phi()
        {
            return n_phi;
        }
        
        public static double getScan_step()
        {
            return step;
        }
        
        public static double getSgm_psi_rad()
        {
            return sgm_psi_rad;
        }

        public static int getSgm_psi_deg()
        {
            return sgm_psi_deg;
        }
    }
}
