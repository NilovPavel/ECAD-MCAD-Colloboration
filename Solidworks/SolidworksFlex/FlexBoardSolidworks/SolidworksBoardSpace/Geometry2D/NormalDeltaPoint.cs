using System;

namespace FlexBoardSolidworks
{
    class NormalDeltaPoint
    {
        private Line line;
        private Point middlePoint;
        private bool leftOrRight;
        private double distance;

        private void Initialization()
        {
            this.middlePoint = this.line.GetMiddlePoint();
        }

        public Point MiddlePointWithDelta()
        {
            double Angle = this.line.GetTiltAngle() + (this.leftOrRight ? 90 : -90);
            this.middlePoint.X += Math.Cos(Angle * Math.PI / 180) * this.distance;
            this.middlePoint.Y += Math.Sin(Angle * Math.PI / 180) * this.distance;
            return this.middlePoint;
        }

        public NormalDeltaPoint(Line line, double distance, bool leftOrRight)
        {
            this.line = line;
            this.distance = distance;
            this.leftOrRight = leftOrRight;
            this.Initialization();
        }
    }
}
