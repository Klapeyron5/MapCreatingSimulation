using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    class Scan
    {
        /// <summary>
        /// Количество точек в круговом скане (дискретность)
        /// </summary>
        public const ushort n_phi = Form1.n_phi;

        /// <summary>
        /// Дискретная ф-я r(phi)
        /// </summary>
        private ushort[] rByPhi = new ushort[n_phi];

        /// <summary>
        /// Точки, принадлежащие скану в текущей пиксельной дискретности. Если две точки ф-и r(phi)
        /// попадают в один пиксель, то это считается за одну точку. Массив int[] - только размера 2, т.к. (x,y).
        /// Все координаты точек относительно центра (0,0). Для получения абсолютных, прибавьте к ним координаты настоящего центра.
        /// </summary>
        private List<int[]> xyScan = new List<int[]>();

        /// <summary>
        /// Центра скана в абсолютных координатах.
        /// </summary>
        private int X, Y;

        /// <summary>
        /// Рисунок скана.
        /// </summary>
        private PixelMap scanBmp;

        public Scan()
        {

        }
    }
}
