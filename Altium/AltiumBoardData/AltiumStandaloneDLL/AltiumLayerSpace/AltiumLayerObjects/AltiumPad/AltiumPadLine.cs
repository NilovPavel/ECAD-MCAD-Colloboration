using System;

internal class AltiumPadLine : ILine
{
    private double X;
    private double Y;
    private double angle;
    private double length;

    public AltiumPadLine(double X, double Y, double angle, double length)
    {
        this.X = X;
        this.Y = Y;
        this.angle = angle;
        this.length = length;
    }

    Point ILine.GetEndPoint()
    {
        return new Point
        {
            X = this.X + this.length * Math.Cos(Math.PI * (this.angle + 180) / 180.0) / 2,
            Y = this.Y + this.length * Math.Sin(Math.PI * (this.angle + 180) / 180.0) / 2
        };
    }

    Point ILine.GetStartPoint()
    {
        return new Point
        {
            X = this.X + this.length * Math.Cos(Math.PI * (this.angle) / 180.0) / 2,
            Y = this.Y + this.length * Math.Sin(Math.PI * (this.angle) / 180.0) / 2
        };
    }
}