using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltiumLayerSpace
{
    class AltiumLayersConstants
    {
        private TV6_Layer[] layerNotForAllows;

        private void Initialization()
        {
            this.layerNotForAllows = new TV6_Layer[] { TV6_Layer.eV6_BottomOverlay, TV6_Layer.eV6_BottomPaste, TV6_Layer.eV6_BottomSolder,
                TV6_Layer.eV6_TopOverlay, TV6_Layer.eV6_TopPaste, TV6_Layer.eV6_TopSolder};
        }

        public bool IsAllowForWriting(TV6_Layer layerType)
        {
            return Array.Exists(this.layerNotForAllows, x => x == layerType);
        }

        public AltiumLayersConstants()
        {
            this.Initialization();
        }
    }
}
