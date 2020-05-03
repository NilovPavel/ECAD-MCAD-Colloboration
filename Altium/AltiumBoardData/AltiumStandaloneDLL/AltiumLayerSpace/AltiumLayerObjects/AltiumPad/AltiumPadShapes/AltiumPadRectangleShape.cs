internal class AltiumPadRectangleShape : IContour
{
    private IContour iContour;

    public AltiumPadRectangleShape(IContour iContour)
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