using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks
{
    class SolidworksBubbleBody : IBody
    {
        private Body body;
        private double distance;
        private Point originalPoint;

        IBendingLine[] IBody.GetBendingLines()
        {
            return new IBendingLine[0];
        }

        double IBody.GetHeightFirstLayer()
        {
            //return this.body.originHeight / 1000 + this.distance;
            return (this.body.totalHeight - this.body.originHeight) / 2;
        }

        IContour IBody.GetIContour()
        {
            return new SolidworksBubbleContour(this.originalPoint, this.distance);
        }

        bool IBody.GetIsFlex()
        {
            return false;
        }

        double IBody.GetTotalHeight()
        {
            return this.distance;
        }

        private Point CalcOriginalRectanglePoint(Line line)
        {
            Point normalDeltaPoint = new NormalDeltaPoint(line, this.distance*2, false).MiddlePointWithDelta();
            if (!this.body.contour.PointInContour(normalDeltaPoint))
                normalDeltaPoint = new NormalDeltaPoint(line, this.distance*2, true).MiddlePointWithDelta();
            return normalDeltaPoint;
        }

        private void Initialization()
        {
            //Не знаю, как по-другому, поэтому сделано так 
            Line line = this.body.contour.Line.First();
            this.distance = 0.001;
            this.originalPoint = this.CalcOriginalRectanglePoint(line);
        }

        public SolidworksBubbleBody(Body body)
        {
            this.body = body;
            this.Initialization();
        }
    }
}
