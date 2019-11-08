using System;

namespace HierarchySpace
{
    class SchChanell : IComponentID
    {
        public string Designator
        { get; set; }

        public string UniqueID
        { get; set; }

        public string ParentId
        { get; set; }

        string IComponentID.GetDesignator()
        {
            return this.Designator;
        }

        string IComponentID.GetParentID()
        {
            return this.ParentId;
        }

        string IComponentID.GetUniqueID()
        {
            return this.UniqueID;
        }
    }
}
