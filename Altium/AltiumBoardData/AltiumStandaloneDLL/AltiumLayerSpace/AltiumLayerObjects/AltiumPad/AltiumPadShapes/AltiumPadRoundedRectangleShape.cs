internal class AltiumPadRoundedRectangleShape : IContour
{
    private IContour iContour;

    public AltiumPadRoundedRectangleShape(IContour iContour)
    {
        this.iContour = iContour;
    }

    public IArc[] GetIArcs()
    {
        return iContour.GetIArcs();
    }

    public ILine[] GetILines()
    {
        return iContour.GetILines();
    }
}