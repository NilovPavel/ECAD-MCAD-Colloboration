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
        private GraphicsState graphicsState;
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

        void IPainter.DrawSolidPolygon(Color color, List<Line> lines, List<Arc> arcs)
        {
            SolidBrush brush = new SolidBrush(color);
            Pen pen = new Pen(color, 1);
            GraphicsPath path = new GraphicsPath();

            if (arcs.Count == 0 && lines.Count > 0)
                lines.ForEach(item => path.AddLine((float)item.Point1.X * this.scale, (float)item.Point1.Y * this.scale, (float)item.Point2.X * this.scale, (float)item.Point2.Y * this.scale));

            if (lines.Count == 0 && arcs.Count > 0)
                arcs.ForEach(item => path.AddArc((float)(item.OriginalPoint.X - item.Radius) * this.scale, (float)(item.OriginalPoint.Y - item.Radius) * this.scale,
                                                    (float)(item.Radius * 2) * this.scale, (float)(item.Radius * 2) * this.scale, (float)item.StartAngle, (float)item.SweepAngle));

            if (lines.Count != 0 && arcs.Count != 0)
                this.DrawPrimitivesByQueue(path, lines, arcs);

            this.grafics.FillPath(brush, path);
            this.grafics.DrawPath(pen, path);

        }

        private void DrawPrimitivesByQueue(GraphicsPath path, List<Line> lines, List<Arc> arcs)
        {
            path.StartFigure();
            List<IPrimitive> primitiveQueue = this.GetPrimitivesQueue(lines, arcs);
            primitiveQueue.Reverse();

            for (int i = 0; i < primitiveQueue.Count; i++)
            {
                Arc currentArc = primitiveQueue[i] as Arc;
                if (currentArc != null)
                    path.AddArc((float)(currentArc.OriginalPoint.X - currentArc.Radius) * this.scale,
                    (float)(currentArc.OriginalPoint.Y - currentArc.Radius) * this.scale, (float)(currentArc.Radius * 2) * this.scale,
                    (float)(currentArc.Radius * 2) * this.scale, (float)currentArc.StartAngle, (float)currentArc.SweepAngle);

                Line currentLine = primitiveQueue[i] as Line;
                if (currentLine != null)
                    path.AddLine((float)currentLine.Point1.X * this.scale, (float)currentLine.Point1.Y * this.scale,
                        (float)currentLine.Point2.X * this.scale, (float)currentLine.Point2.Y * this.scale);
            }
            path.CloseFigure();
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

            for (IPrimitive currentPrimitive = sourcePrimitives.First(); currentPrimitive != null;)
            {
                destPrimitives.Add(currentPrimitive);
                IPrimitive nextPrimitive = sourcePrimitives.Where(item => (this.GetBools(currentPrimitive, item)
                                                                    && !destPrimitives.Exists(y => y.Equals(item))
                                                                    )).FirstOrDefault();
                currentPrimitive = nextPrimitive;
            }

            return destPrimitives;
        }

        public ImagePainter(Bitmap bitmap, int scale)
        {
            this.grafics = Graphics.FromImage(bitmap);
            this.scale = scale;
            //this.grafics.CompositingQuality = CompositingQuality.HighQuality;
            this.pointEqualityComparer = new PointEqualityComparer();
        }

        public void RestoreState()
        {
            this.grafics.Restore(this.graphicsState);
        }

        public void RotateGraphics(double Angle, double X, double Y)
        {
            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt((float)Angle, new PointF((float)X, (float)Y));
            this.grafics.Transform = myMatrix;
        }

        public void SaveState()
        {
            this.graphicsState = this.grafics.Save();
        }

        public void DrawText(Color color, double x, double y, string value, double angle, string fontName, double height)
        {
            SolidBrush drawBrush = new SolidBrush(color);
            Font font = new Font(fontName, (float)height * this.scale / 20);
            this.grafics.DrawString(value, font, drawBrush, (float)x * this.scale, (float)y * this.scale);
        }
    }
}
