using BoardSpace;
using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltiumLayerSpace
{
    class AltiumLayerStackManager
    {
        private IPCB_MasterLayerStack masterLayerStack;
        private List<AltiumLayer> layers;
        private List<AltiumLayerStack> layerStacks;
        private AltiumLayersConstants layerConstants;

        public List<AltiumLayer> Layers
        {
            get
            {
                return this.layers;
            }
        }

        private void Initialization()
        {
            this.layerConstants = new AltiumLayersConstants();
            this.layers = this.GetLayers();
            this.layerStacks = this.GetLayerStacks();
        }

        private List<AltiumLayer> GetLayers()
        {
            List<AltiumLayer> currentLayers = new List<AltiumLayer>();

            for (IPCB_LayerObject layer_object = this.masterLayerStack.First(TLayerClassID.eLayerClass_All); layer_object != null; layer_object = this.masterLayerStack.Next(TLayerClassID.eLayerClass_All, layer_object))
                currentLayers.Add(new AltiumLayer(layer_object));

            currentLayers.RemoveAll(layer => this.layerConstants.IsAllowForWriting(layer.Type));
            currentLayers.Reverse();

            return currentLayers;
        }

        private List<AltiumLayerStack> GetLayerStacks()
        {
            List<AltiumLayerStack> currentLayerStacks = new List<AltiumLayerStack>();

            for (int i = 0; i < masterLayerStack.SubstackCount(); i++)
                currentLayerStacks.Add(new AltiumLayerStack(masterLayerStack.GetState_Substacks(i)));

            foreach (AltiumLayerStack altiumLayerStack in currentLayerStacks)
                altiumLayerStack.RemoveNotAllow(this.layerConstants);

            return currentLayerStacks;
        }

        public double GetHeightByLayerStackID(string layerStackID)
        {
            return this.layerStacks.Where(x => x.GetID().Equals(layerStackID)).ToArray()[0].GetStackTotalHeight();
        }

        public double GetHeightFirstLayer(string layerStackID)
        {
            AltiumLayer altiumLayer = this.layerStacks.Where(x => x.GetID().Equals(layerStackID)).ToArray()[0].FirstLayer();
            return this.layers.Find(x => x.GetUniqueID().Equals(altiumLayer.GetUniqueID())).OriginHeight;
        }

        public AltiumLayerStackManager(IPCB_MasterLayerStack masterLayerStack)
        {
            this.masterLayerStack = masterLayerStack;
            this.Initialization();
            this.WriteHeightToLayers();
        }

        private void WriteHeightToLayers()
        {
            //string a = "";
            double currentHeight = 0;

            for (int i = 0; i < this.layers.Count; i++)
            {
                AltiumLayer currentLayer = this.layers[i];
                int layersInStacks = this.layerStacks.Count(x => x.isLayerExists(currentLayer.GetUniqueID()));
                currentLayer.OriginHeight = currentHeight;

                if (layersInStacks != 0)
                    currentHeight += currentLayer.GetThickness();

                if (layersInStacks == 1 && this.layerStacks.Count > 1)
                    currentLayer.OriginHeight -= currentLayer.GetThickness();

                //a += layersInStacks + "\t" + currentLayer.GetUniqueID() + "\t" + currentHeight + "\t" + currentLayer.LayerName() + "\t" + currentLayer.GetThickness() + "\t" + currentLayer.OriginHeight + "\r\n";
            }
            //MessageBox.Show(a);
        }

        public double GetLayerHeightById(uint ord)
        {
            return this.layers.Find(x => x.GetUniqueID().Equals(ord.ToString())).OriginHeight;
        }

        public double GetLayerHeightByIdWithSelf(uint ord)
        {
            AltiumLayer altiumLayer = this.layers.Find(x => x.GetUniqueID().Equals(ord.ToString()));
            return altiumLayer.OriginHeight + altiumLayer.GetThickness();
        }
    }
}
