using BoardSpace;
using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumVia : IVia
    {
        private IPCB_Via iVia;
        public AltiumVia(IPCB_Via iVia)
        {
            this.iVia = iVia;
        }

        IContour IVia.GetIContour()
        {
            return new AltiumViaContour(this.iVia);
        }
    }
}
