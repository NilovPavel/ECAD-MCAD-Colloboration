using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumArc : IArc
    {
        private AltiumSegment altiumSegment;

        public AltiumArc(AltiumSegment altiumSegment)
        {
            this.altiumSegment = altiumSegment;
        }

        Point IArc.GetOriginalPoint()
        {
            return this.altiumSegment.OriginalPoint;
        }

        double IArc.GetEndAngle()
        {
            return this.altiumSegment.AngleEnd;
        }

        Point IArc.GetEndPoint()
        {
            return this.altiumSegment.PointEnd;
        }

        double IArc.GetRadius()
        {
            return this.altiumSegment.Radius;
        }

        double IArc.GetStartAngle()
        {
            return this.altiumSegment.AngleStart;
        }

        Point IArc.GetStartPoint()
        {
            return this.altiumSegment.PointStart;
        }

        double IArc.GetSweepAngle()
        {
            return 0;
        }
    }
}
