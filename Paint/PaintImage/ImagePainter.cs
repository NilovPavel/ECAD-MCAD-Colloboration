using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintImage
{
    public class ImagePainter : IPainter
    {
        private Graphics grafics;
        //private GraphicsState graphicsState;
        private int scale;
        private IEqualityComparer<Point> pointEqualityComparer;

        public void DrawArc(Color color, double axis1, double sweepAxis, double radius, double x0, double y0, double width)
        {
            Pen pen = new Pen(color, (float)width * this.scale);
            x0 -= radius;
            y0 -= radius;

            if (sweepAxis == 0)
                return;

            this.grafics.DrawArc(pen, (float)x0 * this.scale, (float)y0 * this.scale, (float)radius * 2 * this.scale, (float)radius * 2 * this.scale, (float)axis1, (float)sweepAxis);
        }

        public void DrawLine(Color color, double x1, double y1, double x2, double y2, double width)
        {
            Pen pen = new Pen(color, (float)width * this.scale);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            this.grafics.DrawLine(pen, (float)x1 * this.scale, (float)y1 * this.scale, (float)x2 * this.scale, (float)y2 * this.scale);
        }

        /*void IPainter.DrawSolidPolygon(Color color, List<Line> lines, List<Arc> arcs)
        {
            SolidBrush brush = new SolidBrush(color);
            GraphicsPath path = new GraphicsPath();

            lines.ForEach(item => path.AddLine((float)item.Point1.X * this.scale, (float)item.Point1.Y * this.scale, (float)item.Point2.X * this.scale, (float)item.Point2.Y * this.scale));
            
            arcs.ForEach(item => path.AddArc((float)(item.OriginalPoint.X - item.Radius) * this.scale, (float)(item.OriginalPoint.Y - item.Radius) * this.scale,
                                                (float)(item.Radius * 2) * this.scale, (float)(item.Radius * 2) * this.scale, (float)item.StartAngle, (float)item.SweepAngle));

            this.grafics.FillPath(brush, path);
        }*/

        void IPainter.DrawSolidPolygon(Color color, List<Line> lines, List<Arc> arcs)
        {
            SolidBrush brush = new SolidBrush(color);
            GraphicsPath path = new GraphicsPath();

            if (arcs.Count == 0 && lines.Count > 0)
                lines.ForEach(item => path.AddLine((float)item.Point1.X * this.scale, (float)item.Point1.Y * this.scale, (float)item.Point2.X * this.scale, (float)item.Point2.Y * this.scale));

            if (lines.Count == 0 && arcs.Count > 0)
                arcs.ForEach(item => path.AddArc((float)(item.OriginalPoint.X - item.Radius) * this.scale, (float)(item.OriginalPoint.Y - item.Radius) * this.scale,
                                                    (float)(item.Radius * 2) * this.scale, (float)(item.Radius * 2) * this.scale, (float)item.StartAngle, (float)item.SweepAngle));

            if (lines.Count != 0 && arcs.Count != 0)
                this.DrawPrimitivesByQueue(brush, path, lines, arcs);


            this.grafics.FillPath(brush, path);
        }



        /*private void DrawPrimitivesByQueue(SolidBrush brush, GraphicsPath path, List<Line> lines, List<Arc> arcs)
        {
            IPrimitive firstPrimitive = lines[0];
            IPrimitive nextPrimitive = default(IPrimitive);

            this.pointEqualityComparer.Equals()
            do
            {
                nextPrimitive = arcs.Where(item => item.Point1.X == firstPrimitive.GetStartPoint().X && item.Point1.Y == firstPrimitive.GetStartPoint().Y).FirstOrDefault();
                if (nextPrimitive != null)
                {
                    Arc nextArc = (Arc)nextPrimitive;
                    path.AddArc((float)Math.Round(nextArc.OriginalPoint.X - nextArc.Radius, 1) * this.scale, (float)Math.Round(nextArc.OriginalPoint.Y - nextArc.Radius,1) * this.scale,
                                                (float)(nextArc.Radius * 1) * this.scale, (float)(nextArc.Radius * 1) * this.scale, (float)nextArc.StartAngle, (float)nextArc.SweepAngle);
                }
                else
                {
                    nextPrimitive= lines.Where(item => Math.Round(item.Point1.X,1) == Math.Round(firstPrimitive.GetEndPoint().X,1) && Math.Round(item.Point1.Y,1) == Math.Round(firstPrimitive.GetEndPoint().Y,1)).FirstOrDefault();
                    Line nextLine = (Line)nextPrimitive;
                    path.AddLine((float)nextLine.Point1.X * this.scale, (float)nextLine.Point1.Y * this.scale, (float)nextLine.Point2.X * this.scale, (float)nextLine.Point2.Y * this.scale);
                }
                firstPrimitive = nextPrimitive;
            }
            while (nextPrimitive == lines[0]);

        }*/

        private void DrawPrimitivesByQueue(SolidBrush brush, GraphicsPath path, List<Line> lines, List<Arc> arcs)
        {
            List<IPrimitive> primitiveQueue = this.GetPrimitivesQueue(lines, arcs);
            primitiveQueue.Reverse();

            for (int i = 0; i < primitiveQueue.Count; i++)
            {
                Arc currentArc = primitiveQueue[i] as Arc;
                if(currentArc!=null)
                    path.AddArc((float)(currentArc.OriginalPoint.X - currentArc.Radius) * this.scale, 
                    (float)(currentArc.OriginalPoint.Y - currentArc.Radius) * this.scale,(float)(currentArc.Radius * 2) * this.scale,
                    (float)(currentArc.Radius * 2) * this.scale, (float)currentArc.StartAngle, (float)currentArc.SweepAngle);

                Line currentLine = primitiveQueue[i] as Line;
                if(currentLine!=null)
                    path.AddLine((float)currentLine.Point1.X * this.scale, (float)currentLine.Point1.Y * this.scale,
                        (float)currentLine.Point2.X * this.scale, (float)currentLine.Point2.Y * this.scale);
            }
        }

        private bool GetBools(IPrimitive firstPrimitive, IPrimitive secondPrimitive)
        {
            bool firstBool = this.pointEqualityComparer.Equals(firstPrimitive.GetEndPoint(), secondPrimitive.GetStartPoint());

            bool secondBool = this.pointEqualityComparer.Equals(firstPrimitive.GetEndPoint(), secondPrimitive.GetEndPoint()) 
                && !this.pointEqualityComparer.Equals(firstPrimitive.GetStartPoint(), secondPrimitive.GetStartPoint());

            bool thirdBool = this.pointEqualityComparer.Equals(firstPrimitive.GetStartPoint(), secondPrimitive.GetStartPoint())
                && !this.pointEqualityComparer.Equals(firstPrimitive.GetEndPoint(), secondPrimitive.GetEndPoint());

            bool fourBool = this.pointEqualityComparer.Equals(firstPrimitive.GetStartPoint(), secondPrimitive.GetEndPoint());

            bool result = firstBool || secondBool || thirdBool || fourBool;

            return result;
        }


        private List<IPrimitive> GetPrimitivesQueue(List<Line> lines, List<Arc> arcs)
        {
            List<IPrimitive> destPrimitives = new List<IPrimitive>();
            List<IPrimitive> sourcePrimitives = new List<IPrimitive>(lines);
            sourcePrimitives.AddRange(arcs);

            IPrimitive firstPrimitive = sourcePrimitives.First();
            IPrimitive currentPrimitive = firstPrimitive;
            IPrimitive nextPrimitive = default(IPrimitive);

            do
            {
                destPrimitives.Add(currentPrimitive);
                nextPrimitive = sourcePrimitives.Where(item => (this.GetBools(currentPrimitive, item)
                                                                            && !destPrimitives.Exists(y => y.Equals(item))
                                                                            )).FirstOrDefault();

                currentPrimitive = nextPrimitive;
                if (currentPrimitive == null)
                    break;
            }
            while (!currentPrimitive.Equals(firstPrimitive));
            
            return destPrimitives;
        }

        public ImagePainter(Bitmap bitmap, int scale)
        {
            this.grafics = Graphics.FromImage(bitmap);
            this.scale = scale;

            this.pointEqualityComparer = new PointEqualityComparer();
        }

        public void RestoreState()
        {
            throw new NotImplementedException();
        }

        public void RotateGraphics(double Angle, double X, double Y)
        {
            throw new NotImplementedException();
        }

        public void SaveState()
        {
            throw new NotImplementedException();
        }
    }
}
