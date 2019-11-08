using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumLine : ILine
    {
        private AltiumSegment altiumSegment;

        public AltiumLine(AltiumSegment altiumSegment)
        {
            this.altiumSegment = altiumSegment;
        }

        Point ILine.GetEndPoint()
        {
            return this.altiumSegment.PointEnd;
        }

        Point ILine.GetStartPoint()
        {
            return this.altiumSegment.PointStart;
        }
    }
}
