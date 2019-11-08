using Help;
using PCB;

namespace BoardSpace
{
    class AltiumSegment
    {
        private PolySegment firstPolySegment;
        private PolySegment secondPolySegment;

        public bool isArc
        { get { return this.firstPolySegment.Kind == TPolySegmentType.ePolySegmentArc; } }

        public double AngleStart
        { get { return this.firstPolySegment.Angle1; } }

        public double AngleEnd
        { get { return this.firstPolySegment.Angle2; } }

        public Point OriginalPoint
        {
            get
            {
                return new Point
                {
                    X = new AltiumHelper().CoordToMMsX(this.firstPolySegment.Cx),
                    Y = new AltiumHelper().CoordToMMsY(this.firstPolySegment.Cy)
                };
            }
        }

        public Point PointStart
        {
            get
            {
                return new Point
                {
                    X = new AltiumHelper().CoordToMMsX(this.firstPolySegment.Vx),
                    Y = new AltiumHelper().CoordToMMsY(this.firstPolySegment.Vy)
                };
            }
        }

        public Point PointEnd
        {
            get
            {
                return new Point
                {
                    X = new AltiumHelper().CoordToMMsX(this.secondPolySegment.Vx),
                    Y = new AltiumHelper().CoordToMMsY(this.secondPolySegment.Vy)
                };
            }
        }

        public double Radius
        { get { return new AltiumHelper().CoordToMMs(this.firstPolySegment.Radius); } }

        public AltiumSegment(PolySegment firstPolySegment, PolySegment secondPolySegment)
        {
            this.firstPolySegment = firstPolySegment;
            this.secondPolySegment = secondPolySegment;
        }
    }
}
