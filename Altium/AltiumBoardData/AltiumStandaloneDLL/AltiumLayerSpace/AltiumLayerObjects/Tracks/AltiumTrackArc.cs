using Help;
using PCB;

internal class AltiumTrackArc : IArc
{
    private IPCB_Arc iArc;

    public double Width
    {
        get
        {
            return new AltiumHelper().CoordToMMs(this.iArc.GetState_LineWidth());
        }
    }

    public AltiumTrackArc(IPCB_Arc iArc)
    {
        this.iArc = iArc;
    }

    double IArc.GetEndAngle()
    {
        return this.iArc.GetState_EndAngle();
    }

    Point IArc.GetEndPoint()
    {
        Point endPoint = new Point
        {
            X = new AltiumHelper().CoordToMMsX(this.iArc.GetState_EndX()),
            Y = new AltiumHelper().CoordToMMsY(this.iArc.GetState_EndY())
        };
        return endPoint;
    }

    Point IArc.GetOriginalPoint()
    {
        Point originPoint = new Point
        {
            X = new AltiumHelper().CoordToMMsX(this.iArc.GetState_CenterX()),
            Y = new AltiumHelper().CoordToMMsY(this.iArc.GetState_CenterY())
        };
        return originPoint;
    }

    double IArc.GetRadius()
    {
        return new AltiumHelper().CoordToMMs(this.iArc.GetState_Radius());
    }

    double IArc.GetStartAngle()
    {
        return this.iArc.GetState_StartAngle();
    }

    Point IArc.GetStartPoint()
    {
        Point endPoint = new Point
        {
            X = new AltiumHelper().CoordToMMsX(this.iArc.GetState_StartX()),
            Y = new AltiumHelper().CoordToMMsY(this.iArc.GetState_StartY())
        };
        return endPoint;
    }

    double IArc.GetSweepAngle()
    {
        return this.iArc.GetState_EndAngle() - this.iArc.GetState_StartAngle();
    }
}