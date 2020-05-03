using PCB;

public class AltiumLayerPolygon : IPolygon
{
    private IPCB_Polygon iPcbPolygon;

    public AltiumLayerPolygon(IPCB_Polygon iPcbPolygon)
    {
        this.iPcbPolygon = iPcbPolygon;
    }

    IContour IPolygon.GetIContour()
    {
        return new AltiumPolygonContour(this.iPcbPolygon);
    }

    IHatch IPolygon.GetIHatch()
    {
        return new AltiumHatch(this.iPcbPolygon);
        //this.iPcbPolygon.GetState_PolyHatchStyle() == TPolyHatchStyle.;
    }
}