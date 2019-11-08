using SolidWorks.Interop.sldworks;
using SolidworksActiveDocSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksVariants
{
    class SolidworksCoordinats
    {
        private Component2 component;
        private SldWorks swApplication;
        private MathUtility mathUtilites;

        public SolidworksCoordinats(Component2 component)
        {
            this.component = component;
            this.Initialization();
        }

        private void Initialization()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.swApplication = swActiveDoc.App;
            this.mathUtilites = (MathUtility)this.swApplication.GetMathUtility();
        }

        public void Transform(Coordinats coordinats)
        {
            Quaternion quaternion = new Quaternion(coordinats.Quaternion);

            //Доворот происходит из-за несогласованности направления осей в AltiumDesigner Solidworks
            Quaternion dovorot = new Quaternion(new double[] { (Math.Sqrt(2)) / 2, -(Math.Sqrt(2)) / 2, 0, 0 });
            Quaternion result = quaternion.Multiplication(dovorot);

            double[] matrixRotationArray = result.GetRotationMatrix();
            double[] transformationMatrix = new double[16];
            Array.Copy(matrixRotationArray, transformationMatrix, 9);
            transformationMatrix[9] = coordinats.X / 1000;
            transformationMatrix[10] = coordinats.Y / 1000;
            transformationMatrix[11] = coordinats.Z / 1000;
            transformationMatrix[12] = 1;
            this.component.Transform2 = this.mathUtilites.CreateTransform((object)transformationMatrix);
        }
    }
}
