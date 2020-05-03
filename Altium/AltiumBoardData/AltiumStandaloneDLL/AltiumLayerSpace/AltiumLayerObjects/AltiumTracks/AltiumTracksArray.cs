using PCB;
using System;
using System.Collections.Generic;
using System.Linq;

public class AltiumTracksArray
{
    private List<ITracks> iTracks;
    private List<ILine> iLines;
    private List<IArc> iArcs;

    public AltiumTracksArray()
    {
        this.Initialization();
    }

    private void Initialization()
    {
        this.iArcs = new List<IArc>();
        this.iLines = new List<ILine>();
        this.iTracks = new List<ITracks>();
    }

    public void AddPrimitive(IPCB_Primitive iPcbPrimitive)
    {
        switch (iPcbPrimitive.GetState_ObjectID())
        {
            case TObjectId.eArcObject:
                IPCB_Arc iArc = (IPCB_Arc)iPcbPrimitive;
                this.iArcs.Add(new AltiumTrackArc(iArc));
                break;
            case TObjectId.eTrackObject:
                IPCB_Track iLine = (IPCB_Track)iPcbPrimitive;
                this.iLines.Add(new AltiumTrackLine(iLine));
                break;
        }
    }

    public ITracks[] GetITracks()
    {
        IEnumerable<double> arcWidths = this.iArcs.Select(item => ((AltiumTrackArc)item).Width);
        IEnumerable<double> lineWidths = this.iLines.Select(item => ((AltiumTrackLine)item).Width);
        IEnumerable<double> resultWidths = arcWidths.Concat(lineWidths).Distinct();

        foreach (double width in resultWidths)
        {
            this.iTracks.Add(
                new AltiumTrack
                {
                    Width = width,
                    TrackContour = new AltiumTrackContour
                    {
                        Arcs = this.iArcs.Where(item => ((AltiumTrackArc)item).Width == width),
                        Lines = this.iLines.Where(item => ((AltiumTrackLine)item).Width == width)
                    }
                });
                
        }
        return this.iTracks.ToArray();
    }
}