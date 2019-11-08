using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.SWRoutingLib;
using System;
using System.Collections.Generic;

namespace SolidworksBoardData.SolidworksVariantCAD.SolidworksComponent.Harness
{
    class SolidworksHarness : IHarnessCAD
    {
        private AssemblyDoc assemblyDoc;
        private RouteManager routeManager;
        private ElectricalRoute electricalRoute;

        ICable[] IHarnessCAD.GetICables()
        {
            List<ICable> iCables = new List<ICable>();

            object[] vCables = (object[])this.electricalRoute.GetCables();

            if ((vCables != null))
                foreach (object vCable in vCables)
                    iCables.Add(new SolidworksCable((SolidWorks.Interop.SWRoutingLib.Cable)vCable));

            return iCables.ToArray();
        }

        IWire[] IHarnessCAD.GetIWires()
        {
            List<IWire> iWires = new List<IWire>();

            object[] vWires = (object[])this.electricalRoute.GetWires();

            if ((vWires != null))
                foreach (object vWire in vWires)
                    iWires.Add(new SolidworksWire((SolidWorks.Interop.SWRoutingLib.Wire)vWire));

            return iWires.ToArray();
        }

        private void Initialization()
        {
            this.routeManager = (RouteManager)this.assemblyDoc.GetRouteManager();
            this.routeManager.EditRoute();
            this.electricalRoute = this.routeManager.GetElectricalRoute();
        }

        public SolidworksHarness(AssemblyDoc assemblyDoc)
        {
            this.assemblyDoc = assemblyDoc;
            this.Initialization();
        }
    }
}
