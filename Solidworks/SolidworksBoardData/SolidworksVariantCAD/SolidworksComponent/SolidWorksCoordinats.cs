using SolidWorks.Interop.sldworks;

namespace SolidworksBoardData.SolidworksVariantCAD
{
    internal class SolidWorksCoordinats : ICoordinats
    {
        private Component2 component;
        private MathUtility mathUtility;
        private double X;
        private double Y;
        private double Z;
        private Quaternion quaternion;

        public SolidWorksCoordinats(Component2 component, MathUtility mathUtility)
        {
            this.component = component;
        }

        double ICoordinats.GetAngle()
        {
            return 0;
        }

        double[] ICoordinats.GetQuaternion()
        {
            return new double[3];
        }

        double ICoordinats.GetX()
        {
            return 0;
        }

        double ICoordinats.GetY()
        {
            return 0;
        }

        double ICoordinats.GetZ()
        {
            return 0;
        }
    }
}