using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class AltiumPadArc : IArc
{
    private double X0;
    private double Y0;
    private double startAngle;
    private double endAngle;
    private double radius;

    public AltiumPadArc(double X0, double Y0, double startAngle, double endAngle, double radius)
    {
        this.X0 = X0;
        this.Y0 = Y0;
        this.startAngle = startAngle;//( + 360) % 360;
        this.endAngle = endAngle; //(+ 360) % 360;
        this.radius = radius;
    }

    double IArc.GetEndAngle()
    {
        return this.endAngle;
    }

    Point IArc.GetEndPoint()
    {
        return new Point
        {
            X = this.X0 + Math.Cos(Math.PI * this.endAngle / 180.0) * this.radius,
            Y = this.Y0 + Math.Sin(Math.PI * this.endAngle / 180.0) * this.radius
        };
    }

    Point IArc.GetOriginalPoint()
    {
        return new Point
        {
            X = this.X0,
            Y = this.Y0
        };
    }

    double IArc.GetRadius()
    {
        return this.radius;
    }

    double IArc.GetStartAngle()
    {
        return this.startAngle;
    }

    Point IArc.GetStartPoint()
    {
        return new Point
        {
            X = this.X0 + Math.Cos(Math.PI * this.startAngle / 180.0) * this.radius,
            Y = this.Y0 + Math.Sin(Math.PI * this.startAngle / 180.0) * this.radius
        };
    }

    double IArc.GetSweepAngle()
    {
        return this.endAngle - this.startAngle;
    }
}
