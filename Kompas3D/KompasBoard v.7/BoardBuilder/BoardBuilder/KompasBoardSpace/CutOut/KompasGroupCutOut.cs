using Kompas6API5;
using Kompas6Constants3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBuilder.KompasBoardSpace.CutOut
{
    class KompasGroupCutOut
    {
        private ksDocument3D kompasBoardDoc;
        private List<Contour> contours;
        private string name;

        private ksPart extrudePart;
        private ksEntity topPlane;
        private ksEntity sketch;
        private ksSketchDefinition sketchDefinition;

        public KompasGroupCutOut(ksDocument3D kompasBoardDoc, List<Contour> contours, string name)
        {
            this.kompasBoardDoc = kompasBoardDoc;
            this.contours = contours;
            this.name = name;
            this.Initialization();
            this.BuildBody();
        }

        private void BuildBody()
        {
            this.CreateSketch();
            this.PaintSketch();
            this.ExtrudeSketch();
        }

        private void ExtrudeSketch()
        {
            ksEntity eCutExtrPad = (ksEntity)this.extrudePart.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition cutExtrDefPad = (ksCutExtrusionDefinition)eCutExtrPad.GetDefinition();
            eCutExtrPad.name = this.name;
            cutExtrDefPad.SetSketch(this.sketch);
            cutExtrDefPad.directionType = (short)Direction_Type.dtReverse;
            cutExtrDefPad.SetSideParam(true, (short)End_Type.etThroughAll, 0, 0, false);
            cutExtrDefPad.SetThinParam(false, 0, 0, 0);
            eCutExtrPad.Create();
        }

        private void CreateSketch()
        {
            this.sketch = this.extrudePart.NewEntity((short)Obj3dType.o3d_sketch);
            this.sketch.name = "Sketch"+this.name;  
            this.sketchDefinition = this.sketch.GetDefinition();
            this.sketchDefinition.SetPlane(this.topPlane);
            this.sketch.Create();
        }

        private void PaintSketch()
        {
            ksDocument2D sketchEdit = (ksDocument2D)this.sketchDefinition.BeginEdit();

            this.contours.ForEach(contour => contour.Line.ForEach(line => sketchEdit.ksLineSeg(line.Point1.X, line.Point1.Y, line.Point2.X, line.Point2.Y, 1)));
            this.contours.ForEach(contour => contour.Arc.ForEach(arc => sketchEdit.ksArcByAngle(arc.OriginalPoint.X, arc.OriginalPoint.Y, arc.Radius, arc.StartAngle, arc.EndAngle, 1, 1)));

            this.sketchDefinition.EndEdit();
        }

        private void Initialization()
        {
            this.extrudePart = this.kompasBoardDoc.GetPart((short)Part_Type.pTop_Part);
            this.topPlane = this.extrudePart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
        }
    }
}
