using PCB;

internal class AltiumLayerPad : IPad
{
    private IPCB_Pad iPcbPad;

    public AltiumLayerPad(IPCB_Pad iPcbPad)
    {
        this.iPcbPad = iPcbPad;
    }

    IContour IPad.GetContour()
    {
        return new AltiumPadContour(this.iPcbPad);
    }
}