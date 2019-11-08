using Help;
using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumViaContour : IContour
    {
        private IPCB_Via iVia;
        private List<ILine> iLines;
        private List<IArc> iArcs;

        public AltiumViaContour(IPCB_Via iVia)
        {
            this.iVia = iVia;
            this.Initialization();
            this.CalcViaRound();
        }

        private void CalcViaRound()
        {
            this.iArcs.Add(new HoleRound(new AltiumHelper().CoordToMMsX(this.iVia.GetState_XLocation()), new AltiumHelper().CoordToMMsY(this.iVia.GetState_YLocation()), new AltiumHelper().CoordToMMs(iVia.GetState_HoleSize())));
        }

        private void Initialization()
        {
            this.iLines = new List<ILine>();
            this.iArcs = new List<IArc>();
        }

        IArc[] IContour.GetIArcs()
        {
            return this.iArcs.ToArray();
        }

        ILine[] IContour.GetILines()
        {
            return this.iLines.ToArray();
        }
    }
}
