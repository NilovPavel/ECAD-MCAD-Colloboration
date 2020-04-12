using System;
using PCB;
using Help;

internal class AltiumHatch : IHatch
{
    private IPCB_Polygon iPcbPolygon;
    private TPolyHatchStyle hatchStyle;

    public AltiumHatch(IPCB_Polygon iPcbPolygon)
    {
        this.iPcbPolygon = iPcbPolygon;
        this.Intialization();
    }

    private void Intialization()
    {
        this.hatchStyle = this.iPcbPolygon.GetState_PolyHatchStyle();
    }

    double IHatch.GetStepHatch()
    {
        return new AltiumHelper().CoordToMMs(this.iPcbPolygon.GetState_ReliefAirGap());
    }

    TypeHatch IHatch.GetTypeHatch()
    {
        TypeHatch hatchType;
        switch (this.hatchStyle)
        {
            case TPolyHatchStyle.ePolyVHatch:
                hatchType = TypeHatch.vertical;
                break;
            case TPolyHatchStyle.ePolyHHatch:
                hatchType = TypeHatch.horizontal;
                break;
            case TPolyHatchStyle.ePolySolid:
                hatchType = TypeHatch.fillSolid;
                break;
            case TPolyHatchStyle.ePolyHatch45:
                hatchType = TypeHatch.grid45;
                break;
            case TPolyHatchStyle.ePolyHatch90:
                hatchType = TypeHatch.grid90;
                break;
            default:
                hatchType = TypeHatch.fillSolid;
                break;
        }
        return hatchType;
    }

    double IHatch.GetWidthHatch()
    {
        return new AltiumHelper().CoordToMMs(this.iPcbPolygon.GetState_ReliefConductorWidth());
    }
}