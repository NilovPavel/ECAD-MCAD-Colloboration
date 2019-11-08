using BoardSpace;
using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltiumLayerSpace
{
    class AltiumLayerStack
    {
        private IPCB_LayerStack iLayerStack;
        private List<AltiumLayer> layers;

        public AltiumLayer FirstLayer()
        {
            return this.layers[0];
        }

        private List<AltiumLayer> GetLayers()
        {
            List<AltiumLayer> currentLayers = new List<AltiumLayer>();

            for (IPCB_LayerObject layer_object = this.iLayerStack.First(TLayerClassID.eLayerClass_All); layer_object != null; layer_object = this.iLayerStack.Next(TLayerClassID.eLayerClass_All, layer_object))
                currentLayers.Add(new AltiumLayer(layer_object));

            currentLayers.Reverse();

            return currentLayers;
        }

        private void Initialization()
        {
            this.layers = this.GetLayers();
        }

        public string GetID()
        {
            return this.iLayerStack.ID();
        }

        public AltiumLayerStack(IPCB_LayerStack iLayerStack)
        {
            this.iLayerStack = iLayerStack;
            this.Initialization();
        }

        public bool isLayerExists(string layerId)
        {
            return this.layers.Exists(x => x.GetUniqueID().Equals(layerId));
        }

        public double GetStackTotalHeight()
        {
            return this.layers.Select(x => x.GetThickness()).Sum();
        }

        public void RemoveNotAllow(AltiumLayersConstants layerConstants)
        {
            this.layers.RemoveAll(x => layerConstants.IsAllowForWriting(x.Type));
        }
    }
}
