using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    abstract class AbstractAltiumContour
    {
        protected List<AltiumSegment> Segments;

        public List<ILine> Lines
        { get; private set; }

        public List<IArc> Arcs
        { get; private set; }

        abstract protected void GetPrimitives();

        private void CalcPrimitives()
        {
            foreach (AltiumSegment segment in this.Segments)
            {
                if (!segment.isArc)
                    this.Lines.Add(new AltiumLine(segment));
                else this.Arcs.Add(new AltiumArc(segment));
            }
        }

        protected void Initialization()
        {
            this.Segments = new List<AltiumSegment>();
            this.Lines = new List<ILine>();
            this.Arcs = new List<IArc>();
            this.GetPrimitives();
            this.CalcPrimitives();
        }
    }
}
