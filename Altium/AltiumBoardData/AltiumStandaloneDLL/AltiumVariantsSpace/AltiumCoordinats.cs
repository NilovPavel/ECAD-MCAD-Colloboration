using PCB;
using Help;
using System;
using BoardSpace;

namespace AltiumVariantsSpace
{
    class AltiumCoordinats : ICoordinats
    {
        private IPCB_Component pcbComponent;

        public AltiumCoordinats(IPCB_Component pcbComponent)
        {
            this.pcbComponent = pcbComponent;
        }

        double ICoordinats.GetAngle()
        {
            return this.pcbComponent == null ? 0 : this.pcbComponent.GetState_Rotation() % 360;
        }

        double[] ICoordinats.GetQuaternion()
        {
            if (this.pcbComponent == null) return null;
            //Плохое решение, но по-другому не знаю как
            bool isTop = this.pcbComponent.GetState_Layer().ToString().ToLower().IndexOf("top") != -1;
            double alpha = this.pcbComponent.GetState_Rotation()*(-1);
            double normalAngle = isTop ? 0 : 180;
            double[] normalQuaternion = new double[] { Math.Cos(normalAngle * Math.PI / (2 * 180)), Math.Sin(normalAngle * Math.PI / (2 * 180)), 0, 0 };
            double[] rotateQuaternion = new double[] { Math.Cos(alpha * Math.PI / (2 * 180)), 0, 0, Math.Sin(alpha * Math.PI / (2 * 180)) };
            Quaternion resultQuaternion = new Quaternion(rotateQuaternion).Multiplication(new Quaternion(normalQuaternion));
            return resultQuaternion.QuaternionArray;
        }

        double ICoordinats.GetX()
        {
            return this.pcbComponent == null ? 0 : new AltiumHelper().CoordToMMsX(this.pcbComponent.GetState_XLocation());
        }

        double ICoordinats.GetY()
        {
            return this.pcbComponent == null ? 0 : new AltiumHelper().CoordToMMsY(this.pcbComponent.GetState_YLocation());
        }

        double ICoordinats.GetZ()
        {
            if (this.pcbComponent == null) return 0;
            //Плохое решение, но по-другому не знаю как
            bool isTop = this.pcbComponent.GetState_Layer().ToString().ToLower().IndexOf("top") != -1;
            double z = isTop ? new AltiumHelper().GetHeightLayerById(this.pcbComponent.GetState_V7Layer().Ord) : new AltiumHelper().GetHeightLayerByIdWithSelf(this.pcbComponent.GetState_V7Layer().Ord);
            AltiumStandoff standoff = new AltiumStandoff(this.pcbComponent);
            return z + (isTop ? 1 : -1 )* new AltiumHelper().CoordToMMs(standoff.StandoffHeight);
        }
    }
}