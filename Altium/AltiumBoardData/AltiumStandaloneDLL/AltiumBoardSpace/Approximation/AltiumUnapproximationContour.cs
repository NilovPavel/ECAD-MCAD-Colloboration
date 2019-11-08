using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BoardSpace
{
    class AltiumUnapproximationContour : IContour
    {
        private IContour boardOutLineContour;
        private IContour iBodyContour;
        private List<ILine> bodyLines;
        private List<ILine> outlineLines;
        private List<Point> outlinePoints;
        private List<ILine> resultLines;
        private List<IArc> resultArcs;

        public AltiumUnapproximationContour(IContour iBodyContour, IContour boardOutLineContour)
        {
            this.iBodyContour = iBodyContour;
            this.boardOutLineContour = boardOutLineContour;
            this.Initialization();
            this.Reacalc();
        }

        private void Initialization()
        {
            this.bodyLines = this.iBodyContour.GetILines().ToList();
            this.outlineLines = this.boardOutLineContour.GetILines().ToList();
            this.outlinePoints = this.outlineLines.Select(x => x.GetStartPoint()).ToList().Concat(this.outlineLines.Select(x => x.GetEndPoint())).ToList();
            this.resultLines = new List<ILine>();
            this.resultArcs = new List<IArc>();
        }

        private void CalcCircuits(List<ILine> arcLines)
        {
            for (int i = 0; i < arcLines.Count; i++)
            {
                List<ILine> currentNexus = new List<ILine>();
                ILine nextILineCurrentNexus = arcLines.First(x => this.outlinePoints.Exists(y => y.X == x.GetStartPoint().X && y.Y == x.GetStartPoint().Y));
                do
                {
                    arcLines.Remove(nextILineCurrentNexus);
                    currentNexus.Add(nextILineCurrentNexus);
                }
                while ((nextILineCurrentNexus = arcLines.Find(x => x.GetStartPoint().X == nextILineCurrentNexus.GetEndPoint().X && x.GetStartPoint().Y == nextILineCurrentNexus.GetEndPoint().Y)) != null);
                i = 0;
                this.resultArcs.Add(new AltiumApproximationArc(currentNexus));
                //MessageBox.Show(string.Join("\r\n", currentNexus.Select(x => "X1=" + x.GetStartPoint().X + ";\tY1=" + x.GetStartPoint().Y + ";\tX2=" + x.GetEndPoint().X + ";\tY2=" + x.GetEndPoint().Y).ToArray()));
            }
        }

        private void Reacalc()
        {
            this.resultLines = this.bodyLines.Where(x => (this.outlinePoints.Exists(y => y.X == x.GetStartPoint().X && y.Y == x.GetStartPoint().Y))
                                                      && (this.outlinePoints.Exists(y => y.X == x.GetEndPoint().X && y.Y == x.GetEndPoint().Y))
                                                                 ).ToList();

            List<ILine> arcLines = this.bodyLines.Except(this.resultLines).ToList();

            if (arcLines.Count == 0)
                return;

            this.CalcCircuits(arcLines);
        }


        IArc[] IContour.GetIArcs()
        {
            return this.resultArcs.ToArray();
            //return this.iBodyContour.GetIArcs();
        }

        ILine[] IContour.GetILines()
        {
            return this.resultLines.ToArray();
        }
    }
}