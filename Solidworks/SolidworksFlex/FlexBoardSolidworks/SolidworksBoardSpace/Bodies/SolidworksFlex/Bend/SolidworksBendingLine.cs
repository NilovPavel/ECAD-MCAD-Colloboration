using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksActiveDocSpace;
using System;

namespace SolidworksBoardSpace
{
    class SolidworksBendingLine
    {
        private Face2 face;
        private BendingLine bendingLine;
        private ModelDoc2 modelDoc;
        private SketchManager sketchManager;
        private FeatureManager featureManager;
        private SketchSegment sketchSegment;

        public SolidworksBendingLine(Face2 face, BendingLine bendingLine)
        {
            this.face = face;
            this.bendingLine = bendingLine;
            this.Initialization();
            this.SelectSurface();
            this.CreateSketch();
            this.Bend();
        }

        private void Initialization()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.modelDoc = swActiveDoc.ModelDoc;
            this.sketchManager = swActiveDoc.SketchManager;
            this.featureManager = swActiveDoc.FeatureManager;
        }

        private void CreateSketch()
        {
            this.sketchManager.InsertSketch(true);
            this.sketchManager.AddToDB = false;
            this.sketchSegment = this.sketchManager.CreateLine(this.bendingLine.Line.Point1.X / 1000, this.bendingLine.Line.Point1.Y / 1000, 0, this.bendingLine.Line.Point2.X / 1000, this.bendingLine.Line.Point2.Y / 1000, 0);
            this.sketchManager.AddToDB = true;
            this.sketchManager.InsertSketch(true);
            this.modelDoc.ClearSelection();
            this.sketchSegment.Select(false);
        }

        private void SelectSurface()
        {
            ((Entity)this.face).Select(true);
        }

        private CustomBendAllowance CreateCustomBendAllowance()
        {
            CustomBendAllowance CBAObject = this.featureManager.CreateCustomBendAllowance();
            CBAObject.Type = 2;
            CBAObject.KFactor = 0.5;
            return CBAObject;
        }

        private void Bend()
        {
            FaceRay faceRay = new FaceRay(this.bendingLine, this.face);
            //this.modelDoc.ShowNamedView2("Спереди", (int)swStandardViews_e.swFrontView);
            this.modelDoc.Extension.SelectByRay(faceRay.WorldX, faceRay.WorldY, faceRay.WorldZ, faceRay.RayVectorX, faceRay.RayVectorY, faceRay.RayVectorZ, faceRay.RayRadius, (int)swSelectType_e.swSelFACES, true, 0, 0);
            this.modelDoc.FeatureManager.InsertSheetMetal3dBend(Math.Abs(this.bendingLine.Angle * Math.PI / 180), false, this.bendingLine.Radius / 1000, this.bendingLine.Angle > 0 ? false:true, 0, this.CreateCustomBendAllowance());
            this.modelDoc.ClearSelection();
        }
    }
}
