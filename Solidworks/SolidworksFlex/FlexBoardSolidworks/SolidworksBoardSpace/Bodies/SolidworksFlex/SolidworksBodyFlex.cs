using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksActiveDocSpace;
using System;
using System.Linq;

namespace SolidworksBoardSpace
{
    class SolidworksBodyFlex : AbstractSolidworksBody
    {
        private static int countFlex;

        public SolidworksBodyFlex(Body body) : base(body)
        {
        }

        public void CreateBend()
        {
            object[] faces = this.absFeature.GetFaces();
            Face2 topFace = (Face2)faces[0];
            for (int faceCount = 0; faceCount < faces.Length; faceCount++)
                if (((double[])((Face2)faces[faceCount]).Normal).SequenceEqual(new double[] { 0, 0, 1 }))
                {
                    topFace = (Face2)faces[faceCount];
                    break;
                }

            this.Body.BendingLine.ForEach(bendingLine => new SolidworksBendingLine(topFace, bendingLine));

            /*Entity entity = (Entity)faces[0];
            Face2 face = (Face2) faces[0];
            entity.Select(false);
            for (int i = 0; i < faces.Length; i++)
            {
                Entity face = (Entity)faces[i];
                if (((Face2)face).Normal.SequenceEqual(new double[] { 0, 0, 1 }))
                    face.Select(false);
            }*/
        }

        private Face2 GetTopFace()
        {
            object[] faces = this.absFeature.GetFaces();
            Face2 topFace = (Face2)faces[0];
            for (int faceCount = 0; faceCount < faces.Length; faceCount++)
                if (((double[])((Face2)faces[faceCount]).Normal).SequenceEqual(new double[] { 0, 0, 1 }))
                {
                    topFace = (Face2)faces[faceCount];
                    break;
                }

            return topFace;
        }


        public void CreateBendByFoldIndex(short foldIndex)
        {
            if (!this.Body.BendingLine.Exists(x => x.FoldIndex == foldIndex))
                return;

            BendingLine bendLine = this.Body.BendingLine.Find(x => x.FoldIndex == foldIndex);
            new SolidworksBendingLine(this.GetTopFace(), bendLine);

            
        }

        /*private void CreateBendingLine()
        {
            throw new NotImplementedException();
        }*/

        private CustomBendAllowance CreateCustomBendAllowance()
        {
            CustomBendAllowance CBAObject = this.featureManager.CreateCustomBendAllowance();
            CBAObject.Type = 2;
            CBAObject.KFactor = 0.5;
            return CBAObject;
        }

        protected override void BuildBody()
        {
            this.absFeature = this.featureManager.InsertSheetMetalBaseFlange2(this.Body.totalHeight / 1000, false, 0, 0, 0, true, 0, 0,
                (int)swEndConditions_e.swEndCondOffsetFromSurface, this.CreateCustomBendAllowance(), true, (int)swSheetMetalReliefTypes_e.swSheetMetalReliefNone,
                0, 0, 0, false, false, false, false);

            if (this.absFeature != null)
                this.absFeature.Name = "FlexBody" + countFlex++;
        }
    }
}
