using AltiumDebugDLL.AltiumLayerSpace.AltiumLayerObjects.AltiumPad.AltiumPadShapes;
using System;
using System.Collections.Generic;

internal class AltiumPadOctagonShape : IContour
{
    private IContour iContour;
    private List<ILine> iLines;

    public AltiumPadOctagonShape(IContour iContour)
    {
        this.iContour = iContour;
        this.Initialization();
        this.Calc();
    }

    private void Calc()
    {
        this.iLines.AddRange(this.iContour.GetILines());

        Array.ForEach(this.iContour.GetIArcs(), item => this.iLines.Add(
            new Line2Point(item.GetStartPoint(), item.GetEndPoint())));
    }

    private void Initialization()
    {
        this.iLines = new List<ILine>();
    }

    public IArc[] GetIArcs()
    {
        return new IArc[0];
    }

    public ILine[] GetILines()
    {
        return this.iLines.ToArray();
    }
}