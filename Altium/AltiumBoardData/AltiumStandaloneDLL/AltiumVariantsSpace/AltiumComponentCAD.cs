using System;
using EDP;
using PCB;
using System.Collections.Generic;
using AltiumVariants;

namespace AltiumVariantsSpace
{
    public class AltiumComponentCAD : IComponentCAD
    {
        private IComponent component;
        private IPCB_Component pcbComponent;
        private bool isFitted;

        public AltiumComponentCAD(IComponent component, IPCB_Component pcbComponent)
        {
            this.component = component;
            this.pcbComponent = pcbComponent;
            this.isFitted = true;
        }

        string IComponentCAD.GetConfiguration()
        {
            return this.component.DM_Footprint() == null ? string.Empty : this.component.DM_Footprint();
        }

        string IComponentCAD.GetDesignator()
        {
            return this.component.DM_PhysicalDesignator();
        }

        bool IComponentCAD.GetFitted()
        {
            return this.isFitted;
        }

        string IComponentCAD.GetUniqueID()
        {
            return this.component.DM_UniqueId();
        }

        void IComponentCAD.SetFitted(bool isFitted)
        {
            this.isFitted = isFitted;
        }

        ICoordinats IComponentCAD.GetICoordinats()
        {
            return this.pcbComponent != null ? new AltiumCoordinats(this.pcbComponent) : null;
        }

        IComponentCAD IComponentCAD.Clone()
        {
            return new AltiumComponentCAD(this.component, this.pcbComponent);
        }

        IDataESKD IComponentCAD.GetIComponentData()
        {
            return new AltiumComponentData(this.component);
        }

        string IComponentCAD.GetLogicalDesignator()
        {
            return this.component.DM_LogicalDesignator();
        }
    }
}