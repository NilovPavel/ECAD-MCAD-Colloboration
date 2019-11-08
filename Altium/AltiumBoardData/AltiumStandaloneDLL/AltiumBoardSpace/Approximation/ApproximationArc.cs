using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardSpace
{
    class AltiumApproximationArc : IArc
    {
        private List<ILine> approximationLines;
        private Point startPoint, endPoint, originalPoint;
        private double startAngle, endAngle;

        double IArc.GetEndAngle()
        {
            return this.endAngle;
        }

        Point IArc.GetEndPoint()
        {
            return this.endPoint;
        }

        Point IArc.GetOriginalPoint()
        {
            return this.originalPoint;
        }

        double IArc.GetRadius()
        {
            return Math.Round(Math.Sqrt(Math.Pow((this.originalPoint.X - this.startPoint.X), 2) + Math.Pow((this.originalPoint.Y - this.startPoint.Y), 2)), 3);
        }

        double IArc.GetStartAngle()
        {
            return this.startAngle;
        }

        Point IArc.GetStartPoint()
        {
            return this.startPoint;
        }

        double IArc.GetSweepAngle()
        {
            return this.endAngle - this.startAngle;
        }

        public AltiumApproximationArc(List<ILine> approximationLines)
        {
            this.approximationLines = approximationLines;
            this.Initialization();
            this.CalcOriginPoint();
            this.CalcAngles();
        }

        private void CalcAngles()
        {
            this.startAngle = Math.Atan2(this.originalPoint.Y - this.startPoint.Y, this.originalPoint.X - this.startPoint.X) * 180 / Math.PI + 180;
            this.endAngle = Math.Atan2(this.originalPoint.Y - this.endPoint.Y, this.originalPoint.X - this.endPoint.X) * 180 / Math.PI + 180;
        }

        private void Initialization()
        {
            this.startPoint =
                new Point
                {
                    X = this.approximationLines.First().GetStartPoint().X,
                    Y = this.approximationLines.First().GetStartPoint().Y
                };

            this.endPoint =
                new Point
                {
                    X = this.approximationLines.Last().GetEndPoint().X,
                    Y = this.approximationLines.Last().GetEndPoint().Y
                };
        }

        private void CalcOriginPoint()
        {
            double xa = this.approximationLines[0].GetStartPoint().X;
            double ya = this.approximationLines[0].GetStartPoint().Y;

            double xb = this.approximationLines[1].GetStartPoint().X;
            double yb = this.approximationLines[1].GetStartPoint().Y;

            double xc = this.approximationLines[1].GetEndPoint().X;
            double yc = this.approximationLines[1].GetEndPoint().Y;

            this.originalPoint =
                new Point
                {
                    X = Math.Round(0.5 * ((yc - yb) * (xb * xb - xa * xa + yb * yb - ya * ya) - (yb - ya) * (xc * xc - xb * xb + yc * yc - yb * yb)) / ((xb - xa) * (yc - yb) - (xc - xb) * (yb - ya)),3),
                    Y = Math.Round(0.5 * ((xb - xa) * (xc * xc - xb * xb + yc * yc - yb * yb) - (xc - xb) * (xb * xb - xa * xa + yb * yb - ya * ya)) / ((xb - xa) * (yc - yb) - (xc - xb) * (yb - ya)),3)
                };
        }
    }
}
