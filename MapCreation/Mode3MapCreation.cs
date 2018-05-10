using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    class Mode3MapCreation:Mode
    {
        public Mode3MapCreation(MainForm mainForm) : base(mainForm)
        {
            if (this.mainForm != null)
            {
                initialize();
            }
        }
    }
}
