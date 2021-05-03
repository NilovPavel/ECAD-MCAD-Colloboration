using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintImage
{
    public class ImageBuilder
    {
        private IPainter iPainter;
        private Assembly assembly;
        private int[] layerNumbers;
        private string filename;
        private Bitmap image;
        private int scale;

        public ImageBuilder(Assembly assembly, int[] layerNumbers, string filename)
        {
            this.assembly = assembly;
            this.layerNumbers = layerNumbers;
            this.filename = filename;

            this.Initialization();

            this.PaintBoard();

            Array.ForEach(layerNumbers, item => this.PaintLayer(item));

            this.SaveFile();
        }

        Color GetColorFromLayerNumber(int layerNumber)
        {
            switch (layerNumber)
            {
                case 34:
                case 33:
                    return Color.White;
                case 1:
                case 32:
                    return Color.Orange;
                default:
                    return Color.Black;
            }
        }

        private void PaintLayer(int layerNumber)
        {
            Layer layer = this.assembly.board.Layer.Where(item => item.layerNumber == layerNumber).FirstOrDefault();
            Color currentColor = this.GetColorFromLayerNumber(layerNumber);

            if (layer == null)
                return;
            
            this.PaintTracks(layer, currentColor);
            this.PaintPolygons(layer, currentColor);
            //this.PaintText(layer, currentColor);
        }

        private void PaintText(Layer layer, Color color)
        {
            layer.paint.Text.ForEach(item => this.iPainter.DrawText(color, item.x, item.y, item.value, item.angle, item.fontName, item.height));
        }

        private void PaintPolygons(Layer layer, Color color)
        {
            layer.paint.Pad.ForEach(item => this.iPainter.DrawSolidPolygon(color, item.contour.Line, item.contour.Arc));
            //layer.paint.Polygon.ForEach(item => this.iPainter.DrawSolidPolygon(Color.Orange, item.contour.Line, item.contour.Arc));
        }

        private void PaintTracks(Layer layer, Color color)
        {
            layer.paint.Tracks.ForEach(item => item.contour.Arc.ForEach(arc => this.iPainter.DrawArc(color, 
                arc.OriginalPoint.X, arc.OriginalPoint.Y, arc.Radius, arc.StartAngle, arc.SweepAngle, item.width)));

            layer.paint.Tracks.ForEach(item => item.contour.Line.ForEach(line => this.iPainter.DrawLine(color,
                line.Point1.X, line.Point1.Y, line.Point2.X, line.Point2.Y, item.width)));
        }

        private void PaintBoard()
        {
            this.assembly.board.Body.ForEach(body => this.iPainter.DrawSolidPolygon(Color.Green, body.contour.Line, body.contour.Arc));
        }

        private void SaveFile()
        {
            this.image.Save(filename);
        }

        private void Initialization()
        {
            this.scale = 100;
            int height = this.GetHeight()*this.scale;
            int width = this.GetWidth()*this.scale;
            this.image = new Bitmap(width, height);
            this.iPainter = new ImagePainter(this.image, this.scale);
        }

        private int GetHeight()
        {
            int resultHeight = 0;

            foreach (Body eachBody in this.assembly.board.Body)
            {
                double minX = 0, minY = 0, maxX = 0, maxY = 0;
                eachBody.contour.GetExtremums(ref minX, ref minY, ref maxX, ref maxY);
                if (maxY > resultHeight)
                    resultHeight = Convert.ToInt32(maxY);
            }
            return resultHeight;
        }

        private int GetWidth()
        {
            int resultWidth = 0;

            foreach (Body eachBody in this.assembly.board.Body)
            {
                double minX = 0, minY = 0, maxX = 0, maxY = 0;
                eachBody.contour.GetExtremums(ref minX, ref minY, ref maxX, ref maxY);
                if (maxX > resultWidth)
                    resultWidth = Convert.ToInt32(maxX);
            }
            return resultWidth;
        }
    }
}
