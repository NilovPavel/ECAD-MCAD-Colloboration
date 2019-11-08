using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks
{
    class SolidworksBubbleContour : IContour
    {
        private Point originalPoint;
        private double distance;

        IArc[] IContour.GetIArcs()
        {
            return (new List<IArc>()).ToArray();
        }

        ILine[] IContour.GetILines()
        {
            List<ILine> iLines = new List<ILine>();

            Point pointByAxisX = new Point { X = this.originalPoint.X + this.distance, Y = this.originalPoint.Y };
            Point pointByAxisY = new Point { X = this.originalPoint.X, Y = this.originalPoint.Y + this.distance };

            iLines.Add(new SolidworksBubbleLine(this.originalPoint, pointByAxisX));
            iLines.Add(new SolidworksBubbleLine(pointByAxisY, this.originalPoint));
            iLines.Add(new SolidworksBubbleLine(pointByAxisX, pointByAxisY));

            return iLines.ToArray();
        }

        public SolidworksBubbleContour(Point originalPoint, double distance)
        {
            this.originalPoint = originalPoint;
            this.distance = distance;
        }
    }
}
