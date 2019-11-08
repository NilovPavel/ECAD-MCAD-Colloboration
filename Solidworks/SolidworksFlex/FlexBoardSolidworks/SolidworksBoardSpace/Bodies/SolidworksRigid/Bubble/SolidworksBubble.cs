using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksActiveDocSpace;
using SolidworksBoardSpace;
using System.Linq;
using System;

namespace FlexBoardSolidworks
{
    class SolidworksBubble : AbstractSolidworksBody
    {
        private static int bubbleCount;
        private string bubbleName;
        private Face2 frontPlane;
        private Face2 rightPlane;
        private Face2 topPlane;

        public double X
        { get; private set; }

        public double Y
        { get; private set; }

        public double Z
        { get; private set; }

        public void GetFacesBubble(Component2 componentBoard)
        {
            /*if (this.absFeature != null)
                return;

            this.absFeature = componentBoard.FeatureByName(this.bubbleName);*/

            //object[] faces = this.absFeature.GetFaces();
            object[] faces = componentBoard.FeatureByName(this.bubbleName).GetFaces();
            //Face2 topFace = (Face2)faces[0];
            for (int faceCount = 0; faceCount < faces.Length; faceCount++)
            {
                double[] normal = (double[])((Face2)faces[faceCount]).Normal;
                if (normal.SequenceEqual(new double[] { 0, 0, 1 }))
                    this.topPlane = (Face2)faces[faceCount];
                if (normal.SequenceEqual(new double[] { 0, 1, 0 }))
                    this.frontPlane = (Face2)faces[faceCount];
                if (normal.SequenceEqual(new double[] { 1, 0, 0 }))
                    this.rightPlane = (Face2)faces[faceCount];
            }
        }

        public void SelectRightPlane()
        {
            Entity entity = (Entity)this.rightPlane;
            entity.Select(true);
        }

        public void SelectFrontPlane()
        {
            Entity entity = (Entity)this.frontPlane;
            entity.Select(true);
        }

        public void SelectTopPlane()
        {
            Entity entity = (Entity)this.topPlane;
            entity.Select(true);
        }

        public SolidworksBubble(Body body) : base(body)
        {
            double x_min = 0, y_min = 0, x_max = 0, y_max = 0;
            this.Body.contour.GetExtremums(ref x_min, ref y_min, ref x_max, ref y_max);
            this.X = x_min;
            this.Y = y_min;
            this.Z = body.originHeight;
        }

        protected override void BuildBody()
        {
            this.bubbleName = "Bubble" + bubbleCount++;
            //Нюанс - если не строить "пузырек" до ближайшей поверхности, то при сгибе он "уезжает"
            //this.absFeature = this.featureManager.FeatureCut4(true, false, true, (int)swStartConditions_e.swStartSketchPlane, (int)swEndConditions_e.swEndCondUpToSurface, this.Body.totalHeight, this.Body.totalHeight, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false, false);
            this.absFeature = this.featureManager.FeatureCut3(true, false, true, (int)swStartConditions_e.swStartSketchPlane, (int)swEndConditions_e.swEndCondUpToSurface, 1, 1, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            if (this.absFeature != null)
            {
                this.absFeature.Name = this.bubbleName;
                this.absFeature = null;
            }
        }
    }
}
