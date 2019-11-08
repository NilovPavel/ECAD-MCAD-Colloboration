using System;
using SCH;

namespace HierarchySpace
{
    class SchComponent : IComponentID
    {
        private ISch_Component schComponent;
        private string uniqueId;
        private string parentId;
        private string designator;
        private bool isStateCompileMask;

        public bool IsCompiledMask
        { get { return this.isStateCompileMask; } }

        private void Initialization()
        {
            this.uniqueId = this.schComponent.GetState_UniqueId();
            string handle = this.schComponent.GetState_Handle();
            this.parentId = handle.Substring(0, handle.IndexOf(@"\"));
            this.designator = this.schComponent.GetState_Text();
            this.isStateCompileMask = this.schComponent.GetState_CompilationMasked();
        }
        public SchComponent(ISch_Component schComponent)
        {
            this.schComponent = schComponent;
            this.Initialization();
        }
        string IComponentID.GetUniqueID()
        {
            return this.uniqueId;
        }
        string IComponentID.GetParentID()
        {
            return this.parentId;
        }
        string IComponentID.GetDesignator()
        {
            return this.designator;
        }
    }
}