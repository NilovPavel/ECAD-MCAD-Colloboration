using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks
{
    class SolidworksBubbleLine : ILine
    {
        private Point pointStart;
        private Point pointEnd;

        public SolidworksBubbleLine(Point pointStart, Point pointEnd)
        {
            this.pointStart = pointStart;
            this.pointEnd = pointEnd;
        }

        Point ILine.GetEndPoint()
        {
            return this.pointEnd;
        }

        Point ILine.GetStartPoint()
        {
            return this.pointStart;
        }
    }
}
