using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltiumVariantsSpace
{
    class AltiumStandoff
    {
        private IPCB_Component pcbComponent;
        private IPCB_GroupIterator bodyIterator;

        public int StandoffHeight
        { get { return this.GetStandoffHeight(); } }

        private void Initialization()
        {
            this.bodyIterator = this.pcbComponent.GroupIterator_Create();
            this.bodyIterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.eComponentBodyObject));
        }

        public int GetStandoffHeight()
        {
            int currentHeightBody = 0;
            for (IPCB_Primitive body_component = bodyIterator.FirstPCBObject(); body_component != null; body_component = bodyIterator.NextPCBObject())
            {
                IPCB_ComponentBody current_body = (IPCB_ComponentBody)body_component;
                if (current_body.GetOverallHeight() > currentHeightBody)
                    currentHeightBody = current_body.GetOverallHeight();
            }
            return currentHeightBody - this.pcbComponent.GetState_Height();
        }

        public AltiumStandoff(IPCB_Component pcbComponent)
        {
            this.pcbComponent = pcbComponent;
            this.Initialization();
        }
    }
}
