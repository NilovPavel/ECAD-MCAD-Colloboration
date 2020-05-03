public class AltiumTrack : ITracks
{
    public double Width { get; set; }

    public AltiumTrackContour TrackContour { get; set; }

    IContour ITracks.GetIContour()
    {
        return this.TrackContour;
    }

    double ITracks.GetWidth()
    {
        return this.Width;
    }
}