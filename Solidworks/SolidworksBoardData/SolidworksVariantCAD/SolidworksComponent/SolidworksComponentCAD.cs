using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidworksBoardData.SolidworksVariantCAD
{
    public class SolidworksComponentCAD : IComponentCAD
    {
        private Component2 component;
        private ICoordinats coordiants;
        private IDataESKD iDataESKD;
        private string designator;
        private string configurationName;
        private string uniqueId;
        private bool isFitted;

        private void Initialization()
        {
            this.uniqueId = this.component.Name2;
            this.coordiants = default(ICoordinats);
            this.isFitted = !this.component.IsSuppressed();
            this.designator = this.component.ComponentReference;
            this.configurationName = this.component.ReferencedConfiguration;
            bool isBadComponent = !this.isFitted ? this.component.SetSuppression2((int)swComponentSuppressionState_e.swComponentResolved) != (int)swSuppressionError_e.swSuppressionChangeOk || this.component.IsSuppressed() : false;
            this.iDataESKD = !isBadComponent ?
                ((IDataESKD)new SolidworksDataESKD(this.component)) 
                : ((IDataESKD)new DefaultSolidworksDataESKD(this.component));
            if (!this.isFitted && !isBadComponent)
                this.component.SetSuppression2((int)swComponentSuppressionState_e.swComponentSuppressed);
        }

        public SolidworksComponentCAD(Component2 component)
        {
            this.component = component;
            this.Initialization();
        }

        public SolidworksComponentCAD()
        {
        }

        IComponentCAD IComponentCAD.Clone()
        {
            return (IComponentCAD) new SolidworksComponentCAD
            {
                uniqueId = this.uniqueId,
                configurationName = this.configurationName,
                designator = this.designator,
                isFitted = this.isFitted,
                coordiants = this.coordiants,
                iDataESKD = this.iDataESKD,
            };
        }

        string IComponentCAD.GetConfiguration()
        {
            return this.configurationName;
        }

        string IComponentCAD.GetDesignator()
        {
            return this.designator;
        }

        bool IComponentCAD.GetFitted()
        {
            return this.isFitted;
        }

        IDataESKD IComponentCAD.GetIComponentData()
        {
            return this.iDataESKD;
        }

        ICoordinats IComponentCAD.GetICoordinats()
        {
            return this.coordiants;
        }

        string IComponentCAD.GetLogicalDesignator()
        {
            return this.designator;
        }

        string IComponentCAD.GetUniqueID()
        {
            return this.uniqueId;
        }

        void IComponentCAD.SetFitted(bool isFitted)
        {
            this.isFitted = isFitted;
        }
    }
}