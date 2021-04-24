using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintImage
{
    public interface IPainter
    {
        void DrawLine(System.Drawing.Color color, double x1, double y1, double x2, double y2, double Width);   //Построение линии
        void DrawArc(System.Drawing.Color color, double x0, double y0, double radius, double axis1, double sweepAxis, double width);    //Построение дуги
        void DrawSolidPolygon(Color color, List<Line> lines, List<Arc> arcs);
        void SaveState();
        void RestoreState();
        void RotateGraphics(double Angle, double X, double Y);
    }
}
