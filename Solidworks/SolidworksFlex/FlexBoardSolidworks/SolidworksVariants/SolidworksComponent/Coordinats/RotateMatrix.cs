using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksVariants
{
    class RotateMatrix
    {
        public double[] RotateMatrixArray
        { get; private set; }

        public double[] GetQuaternion()
        {
            double[] quaternion = new double[4];
            return quaternion;
        }

        public RotateMatrix(double[] rotateMatrixArray)
        {
            this.RotateMatrixArray = rotateMatrixArray;
        }
    }
}
