using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class HoleRound : IArc
    {
        private double x;
        private double y;
        private double height;

        Point IArc.GetOriginalPoint()
        {
            return new Point
            {
                X = this.x,
                Y = this.y
            };
        }

        double IArc.GetEndAngle()
        {
            return 360;
        }

        Point IArc.GetEndPoint()
        {
            return new Point
            {
                X = Math.Round(this.x + this.height / 2, 3),
                Y = Math.Round(this.y, 3)
            };
        }

        double IArc.GetRadius()
        {
            return this.height / 2;
        }

        double IArc.GetStartAngle()
        {
            return 0;
        }

        Point IArc.GetStartPoint()
        {
            return new Point
            {
                X = Math.Round(this.x + this.height / 2, 3),
                Y = Math.Round(this.y, 3)
            };
        }

        double IArc.GetSweepAngle()
        {
            return 360;
        }

        public HoleRound(double x, double y, double height)
        {
            this.x = x;
            this.y = y;
            this.height = height;
        }
    }
}
