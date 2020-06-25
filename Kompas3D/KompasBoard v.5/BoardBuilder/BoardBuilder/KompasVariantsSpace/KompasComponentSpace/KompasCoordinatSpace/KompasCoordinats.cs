using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API7;

namespace BoardBuilder.KompasVariantsSpace.KompasComponentSpace.KompasCoordinatSpace
{
    public class KompasCoordinats
    {
        private Part7 part;

        public KompasCoordinats(Part7 part)
        {
            this.part = part;
            this.Initialization();
        }

        private void Initialization()
        { }

        internal void Transform(Coordinats coordinats)
        {
            this.part.Placement.SetOrigin(coordinats.X, coordinats.Y, coordinats.Z);
            
            double[] transformMatrix = this.part.Placement.GetMatrix3D();

            double[] rotateMatrix = new Quaternion(coordinats.Quaternion).GetRotationMatrix();
            Array.Copy(rotateMatrix, 0, transformMatrix, 0, 3);
            Array.Copy(rotateMatrix, 3, transformMatrix, 4, 3);
            Array.Copy(rotateMatrix, 6, transformMatrix, 8, 3);

            this.part.Placement.InitByMatrix3D(transformMatrix);
            this.part.UpdatePlacement(true);
            this.part.Fixed = true;
        }
    }
}
