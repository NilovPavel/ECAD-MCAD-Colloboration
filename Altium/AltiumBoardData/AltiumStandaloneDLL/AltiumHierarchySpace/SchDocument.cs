using DXP;
using EDP;
using SCH;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System;

namespace HierarchySpace
{
    class SchDocument : IComponentID
    {
        private ISch_ServerInterface schServer;
        private ISch_Document currentSheet;
        private string uniqueID;
        private string parentId;

        public string FileName
        { get; private set; }
        
        public List<SheetSymbol> SchSheetSymbols
        { get; private set; }
        public List<SchComponent> SchComponents
        { get; private set; }
        

        private void Initialization()
        {
            DXP.GlobalVars.Client.StartServer(EDP.EDPConstant.DocKindSch);
            this.schServer = SCH.GlobalVars.SchServer;
            this.currentSheet = this.schServer.LoadSchDocumentByPath(this.FileName);
            this.FileName = Path.GetFileName(this.FileName);
            this.uniqueID = this.currentSheet.GetState_UniqueId();
            string handle = this.currentSheet.GetState_Handle();
            this.parentId = handle.Substring(0, handle.IndexOf(@"\"));
        }

        private void GetSchSheetSymbols()
        {
            ISch_Iterator iterator = this.currentSheet.SchIterator_Create();

            this.SchSheetSymbols = new List<SheetSymbol>();

            SCH.TObjectSet objectSet = new SCH.TObjectSet();
            objectSet.Add(SCH.TObjectId.eSheetSymbol);
            iterator.AddFilter_ObjectSet(objectSet);

            for (ISch_SheetSymbol sheetSymbol = (ISch_SheetSymbol)iterator.FirstSchObject(); sheetSymbol != null; sheetSymbol = (ISch_SheetSymbol)iterator.NextSchObject())
                this.SchSheetSymbols.Add(new SheetSymbol(sheetSymbol));

            this.SchSheetSymbols.RemoveAll(x=>x.IsCompiledMask);

            this.currentSheet.SchIterator_Destroy(ref iterator);
        }

        private void GetSchComponents()
        {
            ISch_Iterator iterator = this.currentSheet.SchIterator_Create();

            this.SchComponents = new List<SchComponent>();

            SCH.TObjectSet objectSet = new SCH.TObjectSet();
            objectSet.Add(SCH.TObjectId.eSchComponent);
            iterator.AddFilter_ObjectSet(objectSet);

            for (ISch_Component schComponent = (ISch_Component)iterator.FirstSchObject(); schComponent != null; schComponent = (ISch_Component)iterator.NextSchObject())
                this.SchComponents.Add(new SchComponent(schComponent));

            this.SchComponents.RemoveAll(x => x.IsCompiledMask);

            this.currentSheet.SchIterator_Destroy(ref iterator);
        }

        public void UniqueIdtoUniqueIdSheetSymbol(string UniqueIdSheetSymbol)
        {
            this.uniqueID = UniqueIdSheetSymbol;
        }


        string IComponentID.GetUniqueID()
        {
            return this.uniqueID;
        }

        string IComponentID.GetParentID()
        {
            return this.parentId;
        }

        string IComponentID.GetDesignator()
        {
            return string.Empty;
        }

        public SchDocument(string FileName)
        {
            this.FileName = FileName;
            this.Initialization();
            this.GetSchSheetSymbols();
            this.GetSchComponents();
        }
    }
}
