using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    public class Scan
    {
        /// <summary>
        /// Дискретная ф-я r(phi)
        /// </summary>
        public ushort[] rByPhi = new ushort[Parameters.getN_phi()]; //TODO get set

        /// <summary>
        /// Точки, принадлежащие скану в текущей пиксельной дискретности. Если две точки ф-и r(phi)
        /// попадают в один пиксель, то это считается за одну точку. Массив int[] - только размера 2, т.к. (x,y).
        /// Все координаты точек относительно центра (0,0). Для получения абсолютных, прибавьте к ним координаты настоящего центра.
        /// </summary>
        public List<int[]> xyScan = new List<int[]>();

        /// <summary>
        /// Рисунок скана.
        /// </summary>
        public PixelMap scanBmp;

        public Scan()
        {
            scanBmp = new PixelMap(Parameters.getD_scan1(), Parameters.getD_scan1(), 0, 0, 0);
        }

        public Scan(Scan scan)
        {
            this.xyScan = new List<int[]>(scan.getXYScan());
            this.rByPhi = scan.getRbyPhi();
        }

        public Bitmap getBitmap()
        {
            return scanBmp.GetBitmap();
        }

        public ushort[] getRbyPhi()
        {
            return rByPhi;
        }

        public List<int[]> getXYScan()
        {
            return xyScan;
        }
    }
}
