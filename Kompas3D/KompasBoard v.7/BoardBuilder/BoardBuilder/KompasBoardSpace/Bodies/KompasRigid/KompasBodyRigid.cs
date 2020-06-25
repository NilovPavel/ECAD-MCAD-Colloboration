using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6Constants3D;
using Kompas6API5;
using KompasAPI7;
using Kompas6API3D5COM;

namespace BoardBuilder.KompasBoardSpace.Bodies.KompasRigid
{
    class KompasBodyRigid
    {
        private static int rigidCount;
        private Body body;
        /*private ksDocument3D kompasBoardDoc;
        private ksPart part;
        private ksEntity topPlane;
        private ksEntity sketch;
        private ksSketchDefinition sketchDefinition;*/
        private IPartDocument iPartDocument;

        public KompasBodyRigid(Body body, IKompasDocument kompasBoard)
        {
            this.body = body;
            this.iPartDocument = (IPartDocument) kompasBoard;
            this.Initialization();
            this.BuildBody();
        }

        private void CreateSketch()
        {
            IPart iPart = (IPart)this.iPartDocument.;
            iPart.NewEntity((short)Obj3dType.o3d_sketch);
            //Console.WriteLine(iPart.DefaultObject[ksObj3dTypeEnum.o3d_axisOX].Owner.FeatureType.ToString());
            /*this.sketch = this.part.NewEntity((short)Obj3dType.o3d_sketch);
            this.sketch.name = "BodyRigid" + rigidCount;
            this.sketchDefinition = sketch.GetDefinition();
            this.sketchDefinition.SetPlane(this.topPlane);
            this.sketch.Create();*/
        }

        private void PaintSketch()
        {
            /*ksDocument2D sketchEdit = (ksDocument2D)this.sketchDefinition.BeginEdit();

            this.body.contour.Line.ForEach(line => sketchEdit.ksLineSeg(line.Point1.X, line.Point1.Y, line.Point2.X, line.Point2.Y, 1));
            this.body.contour.Arc.ForEach(arc => sketchEdit.ksArcBy3Points(arc.Point1.X, arc.Point1.Y, arc.MiddlePoint.X, arc.MiddlePoint.Y, arc.Point2.X, arc.Point2.Y, 1));

            this.sketchDefinition.EndEdit();*/
        }

        private void BuildBody()
        {
            this.CreateSketch();
            this.PaintSketch();
            this.ExtrudeBody();
        }

        private void ExtrudeBody()
        {
            /*ksEntity entityExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition();
            entityExtr.name = "RigidBody" + rigidCount++;
            extrusionDef.directionType = (short)Direction_Type.dtNormal;
            extrusionDef.SetSideParam(true, (short)End_Type.etBlind, this.body.totalHeight, 0, false);
            extrusionDef.SetSketch(sketch);
            entityExtr.Create();*/
        }

        private void Initialization()
        {
            /*rigidCount = 0;
            this.part = this.kompasBoardDoc.GetPart((short)Part_Type.pTop_Part);
            this.topPlane = (ksEntity)this.part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);*/
        }
    }
}
