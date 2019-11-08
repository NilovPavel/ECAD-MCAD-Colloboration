using SCH;
using System.Windows.Forms;

namespace HierarchySpace
{
    class SheetSymbol : IComponentID
    {
        private ISch_SheetSymbol schSheetSymbol;
        private string uniqueId;
        private string parentId;
        private string designator;
        private bool isStateCompileMask;

        public string FileName
        { get; private set; }

        public bool IsCompiledMask
        { get { return this.isStateCompileMask; } }

        private void Initialization()
        {
            this.uniqueId = this.schSheetSymbol.GetState_UniqueId();
            string handle = this.schSheetSymbol.GetState_Handle();
            this.parentId = handle.Substring(0, handle.IndexOf(@"\"));
            this.designator = this.schSheetSymbol.GetState_Text();
            this.FileName = this.schSheetSymbol.GetState_SchSheetFileName().GetState_Text();
            this.isStateCompileMask = this.schSheetSymbol.GetState_CompilationMasked();
        }

        public SheetSymbol(ISch_SheetSymbol schSheetSymbol)
        {
            this.schSheetSymbol = schSheetSymbol;
            this.Initialization();
        }

        public void UniqueIdtoUniqueIdSheet(string UniqueIdSheet)
        {
            this.uniqueId = UniqueIdSheet;
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