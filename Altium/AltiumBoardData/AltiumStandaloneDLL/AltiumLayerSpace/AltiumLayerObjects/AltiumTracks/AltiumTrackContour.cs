using System.Collections.Generic;
using System.Linq;

public class AltiumTrackContour : IContour
{
    public IEnumerable<IArc> Arcs { private get; set; }
    public IEnumerable<ILine> Lines { private get; set; }

    IArc[] IContour.GetIArcs()
    {
        return this.Arcs.ToArray();
    }

    ILine[] IContour.GetILines()
    {
        return this.Lines.ToArray();
    }
}