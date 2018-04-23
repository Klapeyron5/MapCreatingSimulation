using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    class Scan
    {
        /// <summary>
        /// Радиус скана.
        /// </summary>
        public const ushort r_scan = Form1.r_scan;
        public const ushort d_scan = 2* r_scan;
        public const ushort d_scan1 = d_scan + 1;

        /// <summary>
        /// Количество точек в круговом скане (дискретность)
        /// </summary>
        public const ushort n_phi = Form1.n_phi;

        /// <summary>
        /// Дискретная ф-я r(phi)
        /// </summary>
        public ushort[] rByPhi = new ushort[n_phi]; //TODO get set

        /// <summary>
        /// Точки, принадлежащие скану в текущей пиксельной дискретности. Если две точки ф-и r(phi)
        /// попадают в один пиксель, то это считается за одну точку. Массив int[] - только размера 2, т.к. (x,y).
        /// Все координаты точек относительно центра (0,0). Для получения абсолютных, прибавьте к ним координаты настоящего центра.
        /// </summary>
        public List<int[]> xyScan = new List<int[]>();

        /// <summary>
        /// Центра скана в абсолютных координатах.
        /// </summary>
        private int X, Y;

        /// <summary>
        /// Рисунок скана.
        /// </summary>
        public PixelMap scanBmp;

        public Scan()
        {
            scanBmp = new PixelMap(d_scan1, d_scan1, 0, 0, 0);
            setCenter(-1,-1);
        }

        public Scan(int X, int Y):this()
        {
            setCenter(X,Y);
        }

        public Bitmap getBitmap()
        {
            return scanBmp.GetBitmap();
        }

        public void setCenter(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
