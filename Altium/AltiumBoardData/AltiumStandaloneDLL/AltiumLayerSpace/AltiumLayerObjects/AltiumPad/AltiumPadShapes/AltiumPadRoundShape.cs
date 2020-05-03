using System;
using System.Collections.Generic;
using System.Linq;

internal class AltiumPadRoundShape : IContour
{
    private IContour iContour;
    private List<ILine> iLines;
    private List<IArc> iArcs;

    public AltiumPadRoundShape(IContour iContour)
    {
        this.iContour = iContour;
        this.Initialization();
        this.Calc();
    }

    private void Calc()
    {
        IArc firstPadArc = new AltiumPadArc(this.iArcs[0].GetOriginalPoint().X, this.iArcs[0].GetOriginalPoint().Y, 
            this.iArcs[0].GetStartAngle(), this.iArcs[1].GetEndAngle(), this.iArcs[0].GetRadius());
        IArc secondPadArc = new AltiumPadArc(this.iArcs[2].GetOriginalPoint().X, this.iArcs[2].GetOriginalPoint().Y,
            this.iArcs[2].GetStartAngle(), this.iArcs[3].GetEndAngle(), this.iArcs[2].GetRadius());

        this.iArcs.Clear();

        if (firstPadArc.GetOriginalPoint().X == secondPadArc.GetOriginalPoint().X
            && firstPadArc.GetOriginalPoint().Y == secondPadArc.GetOriginalPoint().Y)
            this.iArcs.Add(new AltiumPadArc(firstPadArc.GetOriginalPoint().X, firstPadArc.GetOriginalPoint().Y,
                0, 360, firstPadArc.GetRadius()));
        else
        {
            this.iArcs.Add(firstPadArc);
            this.iArcs.Add(secondPadArc);
        }
    }

    private void Initialization()
    {
        this.iLines = this.iContour.GetILines().ToList();
        this.iArcs = this.iContour.GetIArcs().ToList();
    }

    public IArc[] GetIArcs()
    {
        return this.iArcs.ToArray();
    }

    public ILine[] GetILines()
    {
        return this.iLines.ToArray();
    }
}