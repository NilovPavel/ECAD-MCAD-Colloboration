using PCB;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Linq;

namespace BoardSpace
{
    class AltiumContour : IContour
    {
        private AbstractAltiumContour iAltiumContour;

        public AltiumContour(IPCB_Region iregion)
        {
            this.iAltiumContour = new Region(iregion);
        }

        public AltiumContour(IPCB_BoardOutline iBoardOutline)
        {
            this.iAltiumContour = new AltiumBoardOutline(iBoardOutline);
        }

        IArc[] IContour.GetIArcs()
        {
            return this.iAltiumContour.Arcs.ToArray();
        }

        ILine[] IContour.GetILines()
        {
            return this.iAltiumContour.Lines.ToArray();
        }
    }
}
