using Help;
using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumHolePad : IHolePad
    {
        private IPCB_Pad2 ipad;
        IContour IHolePad.GetIContour()
        {
            AltiumHoleContour altiumHoleContour = new AltiumHoleContour(this.ipad);
            if (altiumHoleContour.NotDrill) return null;
            return new AltiumHoleContour(this.ipad);
        }

        public AltiumHolePad(IPCB_Pad2 ipad)
        {
            this.ipad = ipad;
        }
    }
}
