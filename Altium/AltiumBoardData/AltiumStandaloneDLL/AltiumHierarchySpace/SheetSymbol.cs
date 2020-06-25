using SCH;
using System.Windows.Forms;
using System;

namespace HierarchySpace
{
    class SheetSymbol : IComponentID
    {
        private ISch_SheetSymbol schSheetSymbol;
        private ISch_Iterator schIterator;
        private string uniqueId;
        private string parentId;
        private string designator;
        private bool isStateCompileMask;
        private string oboznachenie;
        private string naimenovanie;

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
            this.naimenovanie = string.Empty;
            this.oboznachenie = string.Empty;
            this.GetGeneralParameters();
        }

        private void GetGeneralParameters()
        {
            this.schIterator = this.schSheetSymbol.SchIterator_Create();
            this.schIterator.AddFilter_ObjectSet(new SCH.TObjectSet(SCH.TObjectId.eParameter));

            for (ISch_Parameter currentParameter = (ISch_Parameter)this.schIterator.FirstSchObject(); currentParameter != null; currentParameter = (ISch_Parameter)this.schIterator.NextSchObject())
            {
                switch (currentParameter.GetState_Name())
                {
                    case "Наименование":
                        this.naimenovanie = currentParameter.GetState_Text();
                        break;
                    case "Обозначение":
                        this.oboznachenie = currentParameter.GetState_Text();
                        break;
                }
            }

            this.schSheetSymbol.SchIterator_Destroy(ref this.schIterator);
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

        public string GetOboznachenie()
        {
            return this.oboznachenie;
        }

        public string GetNaimenovanie()
        {
            
            return this.naimenovanie;
        }
    }
}