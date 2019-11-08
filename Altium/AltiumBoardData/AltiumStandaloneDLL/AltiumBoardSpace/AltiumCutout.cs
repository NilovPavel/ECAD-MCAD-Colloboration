using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumCutout : ICutOut
    {
        private IPCB_Region iregion;
        public AltiumCutout(IPCB_Region iregion)
        {
            this.iregion = iregion;
        }
        IContour ICutOut.GetIContour()
        {
            return new AltiumContour(this.iregion);
        }
    }
}
