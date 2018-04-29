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

        public Scan scan0 = new Scan();
        private Scan scan1 = new Scan();

        private int X0 = -1, Y0 = -1; //0 scan center
        private int X1 = -1, Y1 = -1; //real 1 scan center
        private int X2 = -1, Y2 = -1; //supposed 1 scan center

        public void setCenter0(int X, int Y)
        {
            X0 = X;
            Y0 = Y;

        }
    }
}
