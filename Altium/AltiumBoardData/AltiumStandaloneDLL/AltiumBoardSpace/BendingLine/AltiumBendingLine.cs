using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumBendingLine : IBendingLine
    {
        private IPCB_BendingLine iPCB_BendingLine;

        public AltiumBendingLine(IPCB_BendingLine iPCB_BendingLine)
        {
            this.iPCB_BendingLine = iPCB_BendingLine;
        }

        double IBendingLine.GetAngle()
        {
            return this.iPCB_BendingLine.GetState_Angle();
        }

        double IBendingLine.GetBendRadius()
        {
            return this.iPCB_BendingLine.GetState_Radius();
        }

        short IBendingLine.GetFoldIndex()
        {
            throw new NotImplementedException();
        }

        Line IBendingLine.GetLine()
        {
            return null;
        }
    }
}
