using FlexBoardSolidworks;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksBoardSpace
{
    class FaceRay
    {
        private Line line;
        private Face2 face;
        private double faceZ;
        private Point middlePoint;
        private BendingLine bendingLine;

        public double RayRadius
        { get; private set; }

        public double WorldX
        { get; private set; }

        public double WorldY
        { get; private set; }

        public double WorldZ
        { get; private set; }

        public double RayVectorX
        { get; private set; }

        public double RayVectorY
        { get; private set; }

        public double RayVectorZ
        { get; private set; }

        private void Initialization()
        {
            this.line = this.bendingLine.Line;
            object[] edges = this.face.GetEdges();
            double[] point = (double[])((Edge)edges[0]).IGetStartVertex().GetPoint();
            this.faceZ = point[2];
            this.RayRadius = 0.001;
        }

        private void ShowLine()
        {
            Console.WriteLine("FoldIndexLine: " + this.bendingLine.FoldIndex
                              + "\r\nMiddlePoint:\t" + "X:" + this.middlePoint.X + "\tY:" + this.middlePoint.Y
                              + "\r\nTiltAngle:\t" + this.line.GetTiltAngle()
                              + "\r\n");
        }

        private void CalcWorld()
        {
            this.middlePoint = this.line.GetMiddlePoint();
            //this.ShowLine();

            NormalDeltaPoint normalDeltaPoint = new NormalDeltaPoint(this.line, this.bendingLine.Radius/2, false);
            Point point = normalDeltaPoint.MiddlePointWithDelta();
            this.WorldX = point.X / 1000;
            this.WorldY = point.Y / 1000;
            this.WorldZ = this.faceZ;
        }

        private void CalcRayVector()
        {
            //-0.499999999999997, -0.707106781186554, -0.499999999999995
            this.RayVectorX = -0.5;
            this.RayVectorY = -(Math.Sqrt(2) / 2);
            this.RayVectorZ = -0.5;
            /*this.RayVectorX = 0;
            this.RayVectorY = 0;
            this.RayVectorZ = 1;*/
        }

        /*public FaceRay(Line line, Face2 face)
        {
            this.line = line;
            this.face = face;
            this.Initialization();
            this.CalcWorld();
            this.CalcRayVector();
        }*/

        public FaceRay(BendingLine bendingLine, Face2 face)
        {
            this.bendingLine = bendingLine;
            this.face = face;
            this.Initialization();
            this.CalcWorld();
            this.CalcRayVector();
        }
    }
}
