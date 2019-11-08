using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardSpace
{
    class Region : AbstractAltiumContour
    {
        private PCB.IPCB_Region iregion;

        protected override void GetPrimitives()
        {
            PCB.IPCB_RegionShape ipcb_RegionShape = (PCB.IPCB_RegionShape)this.iregion;
            for (int i = 0; i < ipcb_RegionShape.GetState_ShapeSegmentCount(); i++)
            {
                int j = i == ipcb_RegionShape.GetState_ShapeSegmentCount() - 1 ? 0 : i + 1;
                this.Segments.Add(new AltiumSegment(ipcb_RegionShape.GetState_ShapeSegment(i), ipcb_RegionShape.GetState_ShapeSegment(j)));
            }
            //Частный случай для кутаутов - последний сегмент преобразуется в точку
            this.Segments = this.Segments.Where(item => !(item.PointStart.X == item.PointEnd.X && item.PointStart.Y == item.PointEnd.Y)).ToList();
        }

        public void UnApproximation(AbstractAltiumContour boardOutline)
        {

        }

        public Region(PCB.IPCB_Region iregion)
        {
            this.iregion = iregion;
            this.Initialization();
        }
    }
}
