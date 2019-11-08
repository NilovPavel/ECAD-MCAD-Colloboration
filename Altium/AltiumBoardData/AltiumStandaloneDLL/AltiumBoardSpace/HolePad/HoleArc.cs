using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class HoleArc : IArc
    {
        private double x;
        private double y;
        private double angle;
        private double height;
        private double width;

        Point IArc.GetOriginalPoint()
        {
            return new Point
            {
                X = Math.Round(this.x, 3),
                Y = Math.Round(this.y, 3)
            };
        }

        double IArc.GetEndAngle()
        {
            return this.angle + 90;
        }

        Point IArc.GetEndPoint()
        {
            return new Point
            {
                X = Math.Round(this.x + this.width * Math.Cos(Math.PI * (this.angle + 90) / 180) / 2, 3),
                Y = Math.Round(this.y + this.width * Math.Sin(Math.PI * (this.angle + 90) / 180) / 2, 3)
            };
        }

        double IArc.GetRadius()
        {
            return this.width / 2;
        }

        double IArc.GetStartAngle()
        {
            return this.angle - 90;
        }

        Point IArc.GetStartPoint()
        {
            return new Point
            {
                X = Math.Round(this.x + this.width * Math.Cos(Math.PI * (this.angle - 90) / 180) / 2, 3),
                Y = Math.Round(this.y + this.width * Math.Sin(Math.PI * (this.angle - 90) / 180) / 2, 3)
            };
        }

        double IArc.GetSweepAngle()
        {
            return 180;
        }

        private void Initialization()
        {
            this.x = this.x + (this.height - this.width) * Math.Cos(Math.PI * this.angle / 180) / 2;
            this.y = this.y + (this.height - this.width) * Math.Sin(Math.PI * this.angle / 180) / 2;
        }

        public HoleArc(double x, double y, double angle, double height, double width)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.height = height;
            this.width = width;
            this.Initialization();
        }
    }
}
