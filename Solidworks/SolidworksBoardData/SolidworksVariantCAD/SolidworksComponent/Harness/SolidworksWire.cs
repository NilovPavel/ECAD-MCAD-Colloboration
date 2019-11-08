using System;
using SolidWorks.Interop.SWRoutingLib;

namespace SolidworksBoardData.SolidworksVariantCAD.SolidworksComponent.Harness
{
    internal class SolidworksWire : IWire
    {
        private SolidWorks.Interop.SWRoutingLib.Wire wire;

        public SolidworksWire(SolidWorks.Interop.SWRoutingLib.Wire wire)
        {
            this.wire = wire;
        }

        string IWire.GetDestinationConnectorDesignator()
        {
            return this.wire.ToComponentReference;
        }

        string IWire.GetDestinationConnectorPin()
        {
            return this.wire.ToPinReference;
        }

        double INet.GetDiameter()
        {
            return this.wire.GetWireProperty().Diameter;
        }

        double INet.GetLength()
        {
            return this.wire.GetCuttingLength();
        }

        string INet.GetMaterial()
        {
            return this.wire.GetWireProperty().PartNumber;
        }

        string IWire.GetSourceConnectorDesignator()
        {
            return this.wire.FromComponentReference;
        }

        string IWire.GetSourceConnectorPin()
        {
            return this.wire.FromPinReference;
        }
    }
}