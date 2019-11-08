using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumBoardOutline : AbstractAltiumContour
    {
        private IPCB_BoardOutline iBoardOutline;

        protected override void GetPrimitives()
        {
            for (int i = 0; i < this.iBoardOutline.GetState_PointCount(); i++)
            {
                int j = i == this.iBoardOutline.GetState_PointCount() - 1 ? 0 : i + 1;
                this.Segments.Add(new AltiumSegment(this.iBoardOutline.GetState_Segments(i), this.iBoardOutline.GetState_Segments(j)));
            }
        }

        public AltiumBoardOutline(IPCB_BoardOutline iBoardOutline)
        {
            this.iBoardOutline = iBoardOutline;
            this.Initialization();
        }
    }
}
