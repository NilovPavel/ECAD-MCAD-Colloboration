using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksVariants
{
    class Quaternion
    {
        public double[] QuaternionArray
        { get; private set; }

        public double[] GetRotationMatrix()
        {
            double[] rotationMatrix = new double[9];
            double w = QuaternionArray[0], x = QuaternionArray[1], y = QuaternionArray[2], z = QuaternionArray[3];

            /*Классическая матрица поворота*/
            rotationMatrix[0] = 1 - 2 * y * y - 2 * z * z; rotationMatrix[3] = 2 * x * y - 2 * z * w; rotationMatrix[6] = 2 * x * z + 2 * y * w;

            rotationMatrix[1] = 2 * x * y + 2 * z * w; rotationMatrix[4] = 1 - 2 * x * x - 2 * z * z; rotationMatrix[7] = 2 * y * z - 2 * x * w;

            rotationMatrix[2] = 2 * x * z - 2 * y * w; rotationMatrix[5] = 2 * y * z + 2 * x * w; rotationMatrix[8] = 1 - 2 * x * x - 2 * y * y;

            return rotationMatrix;
        }

        public Quaternion(double[] quaternionArray)
        {
            this.QuaternionArray = quaternionArray;
        }

        public Quaternion Multiplication(Quaternion quaternion)
        {
            double aw = quaternion.QuaternionArray[0], ax = quaternion.QuaternionArray[1], ay = quaternion.QuaternionArray[2], az = quaternion.QuaternionArray[3];
            double bw = this.QuaternionArray[0], bx = this.QuaternionArray[1], by = this.QuaternionArray[2], bz = this.QuaternionArray[3];

            bw = -ax * bx - ay * by - az * bz;
            bx = aw * bx + ay * bz - az * by;
            by = aw * by - ax * bz + az * bx;
            bz = aw * bz + ax * by - ay * bx;

            //return new Quaternion(new double[] { aw, ax, ay, az });
            return this;
        }
    }
}
