using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksActiveDocSpace;
using System.Linq;

namespace SolidworksBoardSpace
{
    class SolidworksContour
    {
        private Contour contour;
        private SketchManager sketchManager;

        public bool SketchIsOk
        { get; private set; }

        public SolidworksContour(Contour contour)
        {
            this.contour = contour;
            this.Initialization();
            this.CreateContour();
        }

        /*private bool CheckContour()
        {
            int openCount = 0, closedCount = 0;
            int resultContour = this.sketchManager.ActiveSketch.CheckFeatureUse((int)swSketchCheckFeatureProfileUsage_e.swSketchCheckFeature_BASEEXTRUDE, ref openCount, ref closedCount);
            Console.WriteLine(((swSketchCheckFeatureStatus_e)resultContour).ToString());
            return resultContour == 0;
            return true;
        }*/

        private void CreateContour()
        {
            this.SketchIsOk = this.contour.CheckClosedContour();
            /*if (!this.contour.CheckClosedContour())
                Console.WriteLine("Данный контур не является замкнутым!");*/

            this.sketchManager.AddToDB = true;
            this.sketchManager.DisplayWhenAdded = false;
            //Если точки начала и конца совпадают, значит дуга - окружность и нужно строить по-другому алгоритму
            this.contour.Arc.Where(x => (x.Point1.X == x.Point2.X && x.Point1.Y == x.Point2.Y)).ToList().ForEach(x => this.sketchManager.CreateCircleByRadius(x.OriginalPoint.X / 1000, x.OriginalPoint.Y / 1000, 0, x.Radius / 1000));
            //this.contour.Arc.Where(x => (x.Point1.X == x.Point2.X && x.Point1.Y == x.Point2.Y)).ToList().ForEach(x => this.sketchManager.CreateArc(x.OriginalPoint.X / 1000, x.OriginalPoint.Y / 1000, 0, x.Point1.X / 1000, x.Point1.Y / 1000, 0, x.Point2.X / 1000, x.Point2.Y / 1000, 0, 1));
            this.contour.Arc.Where(x => !(x.Point1.X == x.Point2.X && x.Point1.Y == x.Point2.Y)).ToList().ForEach(x => this.sketchManager.Create3PointArc(x.Point2.X / 1000, x.Point2.Y / 1000, 0, x.Point1.X / 1000, x.Point1.Y / 1000, 0, x.MiddlePoint.X / 1000, x.MiddlePoint.Y / 1000, 0));
            this.contour.Line.ForEach(x => this.sketchManager.CreateLine(x.Point1.X / 1000, x.Point1.Y / 1000, 0, x.Point2.X / 1000, x.Point2.Y / 1000, 0));
            this.sketchManager.AddToDB = false;

            
            //this.SketchIsOk = this.CheckContour();
        }

        private void Initialization()
        {
            this.sketchManager = new SolidworksActiveDoc().SketchManager;
        }
    }
}