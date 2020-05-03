using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AltiumDebugDLL.AltiumLayerSpace.AltiumLayerObjects.AltiumPad.AltiumPadShapes
{
    public class Line2Point : ILine
    {
        private Point point1;
        private Point point2;

        public Line2Point(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        Point ILine.GetEndPoint()
        {
            return this.point2;
        }

        Point ILine.GetStartPoint()
        {
            return this.point1;
        }
    }
}