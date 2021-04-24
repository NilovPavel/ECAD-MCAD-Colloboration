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

        public void DrawArc(Color color, double axis1, double sweepAxis, double radius, double x0, double y0, double width)
        {
            Pen pen = new Pen(color, (float)width * this.scale);
            x0 -= radius;
            y0 -= radius;

            if (sweepAxis == 0)
                return;

            this.grafics.DrawArc(pen, (float)x0*this.scale, (float)y0 * this.scale, (float)radius * 2 * this.scale, (float)radius * 2 * this.scale, (float)axis1, (float)sweepAxis);
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

            //if(arcs.Count == 0 && lines.Count > 0)
                lines.ForEach(item => path.AddLine((float)item.Point1.X * this.scale, (float)item.Point1.Y * this.scale, (float)item.Point2.X * this.scale, (float)item.Point2.Y * this.scale));

            //if(lines.Count == 0 && arcs.Count > 0)
            arcs.ForEach(item => path.AddArc((float)(item.OriginalPoint.X - item.Radius) * this.scale, (float)(item.OriginalPoint.Y - item.Radius) * this.scale,
                                                (float)(item.Radius * 2) * this.scale, (float)(item.Radius * 2) * this.scale, (float)item.StartAngle, (float)item.SweepAngle));

            /*if (lines.Count != 0 && arcs.Count != 0)
                this.DrawPrimitivesByQueue(brush, path, lines, arcs);*/


            this.grafics.FillPath(brush, path);
        }

        /*private void GetNextPrimitive(IPrimitive currentPrimitive)
        {
            IPrimitive nextPrimitive = arcs.Where(item => item.Point1.X == firstPrimitive.GetEndPoint().X && item.Point1.Y == firstPrimitive.GetEndPoint().Y).FirstOrDefault();
            nextPrimitive = nextPrimitive ?? lines.Where(item => item.Point1.X == firstPrimitive.GetEndPoint().X && item.Point1.Y == firstPrimitive.GetEndPoint().Y).FirstOrDefault();
            return nextPrimitive;
        }*/

        private void DrawPrimitivesByQueue(SolidBrush brush, GraphicsPath path, List<Line> lines, List<Arc> arcs)
        {
            /*IPrimitive firstPrimitive = lines[0];
            IPrimitive nextPrimitive = default(IPrimitive);


            do
            {
                nextPrimitive = arcs.Where(item => item.Point1.X == firstPrimitive.GetEndPoint().X && item.Point1.Y == firstPrimitive.GetEndPoint().Y).FirstOrDefault();
                if (nextPrimitive != null)
                {
                    Arc nextArc = (Arc)nextPrimitive;
                    if (nextPrimitive == null)
                        return;
                    path.AddArc((float)Math.Round(nextArc.OriginalPoint.X - nextArc.Radius, 2) * this.scale, (float)Math.Round(nextArc.OriginalPoint.Y - nextArc.Radius,2) * this.scale,
                                                (float)(nextArc.Radius * 2) * this.scale, (float)(nextArc.Radius * 2) * this.scale, (float)nextArc.StartAngle, (float)nextArc.SweepAngle);
                }
                else
                {
                    nextPrimitive= lines.Where(item => Math.Round(item.Point1.X,2) == Math.Round(firstPrimitive.GetEndPoint().X,2) && Math.Round(item.Point1.Y,2) == Math.Round(firstPrimitive.GetEndPoint().Y,2)).FirstOrDefault();
                    if (nextPrimitive == null)
                        return;
                    Line nextLine = (Line)nextPrimitive;
                    path.AddLine((float)nextLine.Point1.X * this.scale, (float)nextLine.Point1.Y * this.scale, (float)nextLine.Point2.X * this.scale, (float)nextLine.Point2.Y * this.scale);
                }
                firstPrimitive = nextPrimitive;
            }
            while (nextPrimitive == lines[0]);*/

        }

        public ImagePainter(Bitmap bitmap, int scale)
        {
            this.grafics = Graphics.FromImage(bitmap);
            this.scale = scale;
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
