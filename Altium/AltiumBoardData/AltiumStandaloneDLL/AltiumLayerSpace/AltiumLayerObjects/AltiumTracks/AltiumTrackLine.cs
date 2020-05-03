using Help;
using PCB;

internal class AltiumTrackLine : ILine
{
    private IPCB_Track iLine;

    public double Width
    {
        get
        {
            return new AltiumHelper().CoordToMMs(this.iLine.GetState_Width());
        }
    }

    public AltiumTrackLine(IPCB_Track iLine)
    {
        this.iLine = iLine;
    }

    Point ILine.GetEndPoint()
    {
        Point endPoint = new Point
        {
            X = new AltiumHelper().CoordToMMsX(this.iLine.GetState_X2()),
            Y = new AltiumHelper().CoordToMMsY(this.iLine.GetState_Y2())
        };
        return endPoint;
    }

    Point ILine.GetStartPoint()
    {
        Point startPoint = new Point
        {
            X = new AltiumHelper().CoordToMMsX(this.iLine.GetState_X1()),
            Y = new AltiumHelper().CoordToMMsY(this.iLine.GetState_Y1())
        };
        return startPoint;
    }
}