using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    class Mode2ManualMapCreation:Mode
    {
        public Mode2ManualMapCreation(MainForm mainForm) : base(mainForm)
        {
            if (this.mainForm != null)
            {
                initialize();
            }
        }
    }
}
