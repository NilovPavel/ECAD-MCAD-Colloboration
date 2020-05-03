using System;
using System.Collections.Generic;
using System.Windows.Forms;

internal class RoundRectangularShape : IContour
{
    private double X;
    private double Y;
    private double rotate;
    private double width;
    private double height;
    private int percentCornerRadius;
    private bool inverse;
    private double bigLineLength;
    private double radius;
    private double smallLength;
    private List<IArc> iArcs;
    private List<ILine> iLines;

    public RoundRectangularShape(double X, double Y, double rotate, double width, double height, int percentCornerRadius)
    {
        this.X = X;
        this.Y = Y;
        this.rotate = rotate;
        this.width = width;
        this.height = height;
        this.percentCornerRadius = percentCornerRadius;
        this.Initialization();
        this.CalcArcs();
        this.CalcLines();
    }

    private void CalcLines()
    {
        double lineX = 0;
        double lineY = 0;

        this.CalcLineOriginalPoint(ref lineX, ref lineY, 90, this.height/2);
        this.iLines.Add(new AltiumPadLine(lineX, lineY, this.rotate, this.bigLineLength));

        this.CalcLineOriginalPoint(ref lineX, ref lineY, 270, this.height/2);
        this.iLines.Add(new AltiumPadLine(lineX, lineY, this.rotate, this.bigLineLength));

        this.CalcLineOriginalPoint(ref lineX, ref lineY, 0, this.width/2);
        this.iLines.Add(new AltiumPadLine(lineX, lineY, this.rotate + 90, this.smallLength));

        this.CalcLineOriginalPoint(ref lineX, ref lineY, 180, this.width / 2);
        this.iLines.Add(new AltiumPadLine(lineX, lineY, this.rotate + 90, this.smallLength));

        this.iLines.RemoveAll(item => item.GetStartPoint().X == item.GetEndPoint().X && item.GetStartPoint().Y == item.GetEndPoint().Y);
    }

    private void Initialization()
    {
        this.iArcs = new List<IArc>();
        this.iLines = new List<ILine>();
        this.inverse = this.height > this.width;
        if (this.inverse)
        {
            double tempHeight = this.height;
            this.height = this.width;
            this.width = tempHeight;
        }
        this.radius = (this.height / 2) * this.percentCornerRadius / 100;
        this.smallLength = this.height - 2 * this.radius;
        this.bigLineLength = this.width - this.radius * 2;
        this.rotate += this.inverse ? 90 : 0;
        //MessageBox.Show(this.radius + "\t" + this.smallLength + "\t" + this.bigLineLength + "\t" + this.rotate);
    }

    private void CalcArcOriginalPoint(ref double arcX, ref double arcY, double startAngle, double endAngle)
    {
        arcX = this.X + (this.bigLineLength / 2) * Math.Cos(Math.PI * startAngle / 180.0) 
            + (this.smallLength/2) * Math.Cos(Math.PI * (endAngle) / 180.0);
        arcY = this.Y + (this.bigLineLength / 2) * Math.Sin(Math.PI * startAngle / 180.0) 
            + (this.smallLength/2) * Math.Sin(Math.PI * (endAngle) / 180.0);
    }

    private void CalcLineOriginalPoint(ref double lineX, ref double lineY, double angle, double normalLength)
    {
        lineX = this.X + (normalLength) * Math.Cos(Math.PI * (this.rotate + angle) / 180.0);
        lineY = this.Y + (normalLength) * Math.Sin(Math.PI * (this.rotate + angle) / 180.0);
    }

    private void CalcArcs()
    {
        double arcX = 0;
        double arcY = 0;

        this.CalcArcOriginalPoint(ref arcX, ref arcY, this.rotate , this.rotate - 90);
        this.iArcs.Add(new AltiumPadArc(arcX, arcY, this.rotate -90, this.rotate, this.radius));

        this.CalcArcOriginalPoint(ref arcX, ref arcY, this.rotate, this.rotate + 90);
        this.iArcs.Add(new AltiumPadArc(arcX, arcY, this.rotate, this.rotate + 90, this.radius));

        this.CalcArcOriginalPoint(ref arcX, ref arcY, this.rotate + 180, this.rotate + 90);
        this.iArcs.Add(new AltiumPadArc(arcX, arcY, this.rotate + 90, this.rotate + 180, this.radius));

        this.CalcArcOriginalPoint(ref arcX, ref arcY, this.rotate + 180, this.rotate + 270);
        this.iArcs.Add(new AltiumPadArc(arcX, arcY, this.rotate + 180, this.rotate + 270, this.radius));

        this.iArcs.RemoveAll(item => item.GetRadius() == 0);
    }

    IArc[] IContour.GetIArcs()
    {
        return this.iArcs.ToArray();
    }

    ILine[] IContour.GetILines()
    {
        return this.iLines.ToArray();
    }
}