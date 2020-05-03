using System;
using PCB;
using System.Collections.Generic;
using BoardSpace;

public class AltiumPolygonContour : IContour
{
    private IPCB_Polygon iPcbPolygon;
    private List<IArc> iArcs;
    private List<ILine> iLines;

    public AltiumPolygonContour(IPCB_Polygon iPcbPolygon)
    {
        this.iPcbPolygon = iPcbPolygon;
        this.Initialization();
        this.GetPrimitives();
    }

    private void GetPrimitives()
    {
        for (int primitiveCount = 0; primitiveCount < this.iPcbPolygon.GetState_PointCount(); primitiveCount++)
        {
            int j = primitiveCount == this.iPcbPolygon.GetState_PointCount() - 1 ? 0 : primitiveCount + 1;

            PolySegment firstPolySegment = this.iPcbPolygon.GetState_Segments(primitiveCount);
            PolySegment secondPolySegment = this.iPcbPolygon.GetState_Segments(primitiveCount);

            switch (firstPolySegment.Kind)
            {
                case TPolySegmentType.ePolySegmentArc:
                    this.iArcs.Add(new AltiumArc(new AltiumSegment(firstPolySegment, secondPolySegment)));
                    break;
                case TPolySegmentType.ePolySegmentLine:
                    this.iLines.Add(new AltiumLine(new AltiumSegment(firstPolySegment, secondPolySegment)));
                    break;
            }
        }

    }

    private void Initialization()
    {
        this.iArcs = new List<IArc>();
        this.iLines = new List<ILine>();
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