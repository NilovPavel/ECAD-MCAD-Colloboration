using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksAssemblySpace;
using SolidworksBoardSpace;
using FlexBoardSolidworks.SolidworksBoardSpace;
using FlexBoardSolidworks;

namespace SolidworksVariants
{
    class SolidworksComponent
    {
        private ComponentCAD componentCAD;
        private SolidworksAssembly solidworksAssembly;
        private SolidworksComponentPrimitives swComponentPrimitives;
        private Component2 root;
        private SolidworksBubble bubble;
        private Coordinats coordinatsDekart;
        private int mateError;

        public Component2 Component
        { get; private set; }

        private void Initialization()
        {
            this.coordinatsDekart = this.componentCAD.coordinats;
        }

        public SolidworksComponent(ComponentCAD componentCAD)
        {
            this.componentCAD = componentCAD;
            this.Initialization();
        }

        public void InsertComponent(ModelDoc2 modelDoc, SolidworksAssembly solidworksAssembly)
        {
            this.solidworksAssembly = solidworksAssembly;
            this.root = ((ModelDoc2)this.solidworksAssembly.SwAssembly).ConfigurationManager.ActiveConfiguration.IGetRootComponent2();
            this.Component = solidworksAssembly.SwAssembly.AddComponent5(modelDoc.GetPathName(), (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "Default", false, "Default", 0, 0, 0);
            this.Component.ComponentReference = this.componentCAD.Designator;
            SolidworksCoordinats coordinats = new SolidworksCoordinats(this.Component);
            coordinats.Transform(this.coordinatsDekart);
            this.AddMates();
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
        }

        private SolidworksBubble GetBubble(SolidworksBoard solidWorksBoard)
        {
            Point point = new Point
            {
                X = this.componentCAD.coordinats.X,
                Y = this.componentCAD.coordinats.Y
            };
            SolidworksBodyRigid componentRigid = null;
            foreach (SolidworksBodyRigid eachRigid in solidWorksBoard.SolidworksRigides)
                if (eachRigid.Body.contour.PointInContour(point))
                {
                    componentRigid = eachRigid;
                    break;
                }
            return componentRigid.SolidworksBubble;
        }

        private void AddMateCoincident()
        {
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
            this.bubble.SelectTopPlane();
            this.swComponentPrimitives.SelectFrontPlane(this.root);
            double delta = this.coordinatsDekart.Z - this.bubble.Z;
            double[] quaternion = this.componentCAD.coordinats.Quaternion;
            bool flip = delta < 0;
            int mateAlign = flip ? (int)swMateAlign_e.swMateAlignANTI_ALIGNED : (int)swMateAlign_e.swMateAlignALIGNED;
            this.solidworksAssembly.SwAssembly.AddMate5((int)swMateType_e.swMateDISTANCE, mateAlign, flip, Math.Abs(delta) / 1000, Math.Abs(delta) / 1000, Math.Abs(delta) / 1000, 0.001, 0.001, 0, 0, 0, false, false, 0, out this.mateError);/**/
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
        }

        private void AddMateAxisByX()
        {
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
            this.bubble.SelectRightPlane();
            this.swComponentPrimitives.SelectGeneralAxis(this.root);
            double delta = this.coordinatsDekart.X - this.bubble.X;
            bool flip = delta < 0;
            this.solidworksAssembly.SwAssembly.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, flip, Math.Abs(delta) / 1000, Math.Abs(delta) / 1000, Math.Abs(delta) / 1000, 0.001, 0.001, 0, 0, 0, false, false, 0, out this.mateError);
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
        }

        private void AddMateAxisByY()
        {
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
            this.bubble.SelectFrontPlane();
            this.swComponentPrimitives.SelectGeneralAxis(this.root);
            double delta = this.coordinatsDekart.Y - this.bubble.Y;
            bool flip = delta < 0;
            this.solidworksAssembly.SwAssembly.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, flip, Math.Abs(delta) / 1000, Math.Abs(delta) / 1000, Math.Abs(delta) / 1000, 0.001, 0.001, 0, 0, 0, false, false, 0, out this.mateError);
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
        }

        private void AddMates()
        {
            this.bubble = this.GetBubble(this.solidworksAssembly.SolidWorksBoard);
            if (this.bubble == null)
                return;
            this.bubble.GetFacesBubble(this.solidworksAssembly.SolidWorksBoard.Component);
            this.swComponentPrimitives = new SolidworksComponentPrimitives(this.Component);
            this.AddMateCoincident();
            this.AddMateAxisByX();
            this.AddMateAxisByY();
            this.AddMateAngle();/**/
        }

        private void AddMateAngle()
        {
            ((ModelDoc2)this.solidworksAssembly.SwAssembly).ClearSelection2(true);
            double[] matrix = this.Component.Transform2.ArrayData;
            double angleX = this.GetProperAngle(Math.Atan2(matrix[7], matrix[8]));
            double angleY = this.GetProperAngle(Math.Atan2(-matrix[6], Math.Sqrt(matrix[7] * matrix[7] + matrix[8] * matrix[8])));
            double angleZ = this.GetProperAngle(Math.Atan2(matrix[3], matrix[0]));
            this.bubble.SelectFrontPlane();//.SelectFrontPlane();
            this.swComponentPrimitives.SelectTopPlane(this.root);
            this.solidworksAssembly.SwAssembly.AddMate5((int)swMateType_e.swMateANGLE, (int)swMateAlign_e.swMateAlignALIGNED, false, 0, 0, 0, 0, 0, angleY, angleY, angleY, false, false, 0, out this.mateError);
        }


        private double GetProperAngle(double rad)
        {
            double grad = rad * (180 / Math.PI);
            grad += 360;
            grad %= 360;
            rad = grad * (Math.PI / 180);
            //Console.WriteLine(grad);
            return rad;
        }
    }
}
