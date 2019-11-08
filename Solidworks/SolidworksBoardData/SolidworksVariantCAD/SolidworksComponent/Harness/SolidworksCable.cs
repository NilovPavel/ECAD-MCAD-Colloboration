using System;
using System.Collections.Generic;

namespace SolidworksBoardData.SolidworksVariantCAD.SolidworksComponent.Harness
{
    public class SolidworksCable : ICable
    {
        private SolidWorks.Interop.SWRoutingLib.Cable cable;

        public SolidworksCable(SolidWorks.Interop.SWRoutingLib.Cable cable)
        {
            this.cable = cable;
        }

        IWire[] ICable.GetIWires()
        {
            List<IWire> wires = new List<IWire>();
            object[] cores = (object[])this.cable.GetCores();

            if ((cores != null))
                foreach (object core in cores)
                    wires.Add(new SolidworksWire((SolidWorks.Interop.SWRoutingLib.Wire) core));

            return wires.ToArray();
        }

        double INet.GetLength()
        {
            return this.cable.GetCuttingLength();
        }

        string INet.GetMaterial()
        {
            return this.cable.GetCableProperty().Name;
        }

        double INet.GetDiameter()
        {
            return this.cable.GetCableProperty().Diameter;
        }
    }
}