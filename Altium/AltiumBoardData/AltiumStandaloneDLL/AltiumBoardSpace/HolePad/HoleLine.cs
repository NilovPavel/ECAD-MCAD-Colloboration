using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class HoleLine : ILine
    {
        private double x, x1, x2;
        private double y, y1, y2;
        private double angle;
        private double height;
        private double width;

        Point ILine.GetEndPoint()
        {
            return new Point
            {
                X = Math.Round(this.x1 + width * Math.Cos(Math.PI * (this.angle + 90) / 180) / 2, 3),
                Y = Math.Round(this.y1 + width * Math.Sin(Math.PI * (this.angle + 90) / 180) / 2, 3)
            };
        }

        Point ILine.GetStartPoint()
        {
            return new Point
            {
                X = Math.Round(this.x2 + width * Math.Cos(Math.PI * (this.angle + 90) / 180) / 2, 3),
                Y = Math.Round(this.y2 + width * Math.Sin(Math.PI * (this.angle + 90) / 180) / 2, 3)
            };
        }

        public HoleLine(double x, double y, double angle, double height, double width)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.height = height;
            this.width = width;
            this.GetMiddleLine();
        }

        private void GetMiddleLine()
        {
            this.x1 = this.x + this.height * Math.Cos(Math.PI * this.angle / 180) / 2;
            this.y1 = this.y + this.height * Math.Sin(Math.PI * this.angle / 180) / 2;
            this.x2 = this.x + this.height * Math.Cos(Math.PI * (this.angle + 180) / 180) / 2;
            this.y2 = this.y + this.height * Math.Sin(Math.PI * (this.angle + 180) / 180) / 2;
        }
    }
}
