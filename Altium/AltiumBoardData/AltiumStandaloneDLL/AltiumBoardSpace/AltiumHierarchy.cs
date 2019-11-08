using EDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HierarchySpace
{
    class AltiumHierarchy : IHierarchy
    {
        private IDocument[] iDocuments;
        private List<SchDocument> DocumentsInHierarchy;
        //private bool again_search;
        private List<IComponentID> flat_collection;
        private AssemblyComponentID root;
        private List<string> sheetSymbolsId;

        private void Initialization()
        {
            //this.again_search = true;
            this.DocumentsInHierarchy = new List<SchDocument>();
            this.iDocuments.ToList().ForEach(x => this.DocumentsInHierarchy.Add(new SchDocument(x.DM_FullPath())));
        }

        private void GetLinksDocuments()
        {
            List<SchDocument> searchInLinks = new List<SchDocument>();

            for (int i = 0; i < this.DocumentsInHierarchy.Count; i++)
                for (int j = 0; j < this.DocumentsInHierarchy[i].SchSheetSymbols.Count; j++)
                    if (!this.DocumentsInHierarchy.Exists(x => x.FileName.Equals(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName)))
                        searchInLinks.Add(new SchDocument(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName));

            //this.again_search = searchInLinks.Count!=0;
            this.DocumentsInHierarchy.AddRange(searchInLinks);
        }   //Неизвестно, нужен ли этот метод. Он служит для того, чтобы добавить пути файлов на которые ссылаются шит-девайсы. 
            //Если принять за условие, что в проекте все схемы, на которые ссылаются шит-девайсы, в наличии, то он не нуж

        public AltiumHierarchy(IDocument[] iDocuments)
        {
            this.iDocuments = iDocuments;
            this.Initialization();
            /*while(this.again_search)
                this.GetLinksDocuments();*/
            this.GetFlat();
            this.RewriteSSUid();
        }

        private void GetFlat()
        {
            this.flat_collection = new List<IComponentID>();
            foreach (SchDocument schDocument in this.DocumentsInHierarchy)
            {
                this.flat_collection.Add(schDocument);
                this.flat_collection.AddRange(schDocument.SchSheetSymbols);
                this.flat_collection.AddRange(schDocument.SchComponents);
            }
        }

        AssemblyComponentID subAssembly(AssemblyComponentID component)
        {
            return component;
        }

        private void RewriteSSUid()
        {
            for (int i = 0; i < this.DocumentsInHierarchy.Count; i++)
                for (int j = 0; j < this.DocumentsInHierarchy[i].SchSheetSymbols.Count; j++)
                    this.DocumentsInHierarchy[i].SchSheetSymbols[j].UniqueIdtoUniqueIdSheet(((IComponentID)this.DocumentsInHierarchy.Find(x => x.FileName.Equals(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName))).GetUniqueID());

            /*for (int i = 0; i < this.DocumentsInHierarchy.Count; i++)
                for (int j = 0; j < this.DocumentsInHierarchy[i].SchSheetSymbols.Count; j++)
                    for (int k = 0; k < this.DocumentsInHierarchy.Count; k++)
                        if (this.DocumentsInHierarchy[k].FileName.Equals(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName))
                            this.DocumentsInHierarchy[k].UniqueIdtoUniqueIdSheetSymbol(((IComponentID)this.DocumentsInHierarchy[i].SchSheetSymbols[j]).GetUniqueID());*/
        }

        private void AddComponentsToRoot()
        {
            //Компоненты, найденные в листах, но не присутствующие в шитсимболах будут добавлены в корень
            List<IComponentID> root_sch_component = this.flat_collection.Where(x => !this.sheetSymbolsId.ToList().Exists(y => y.Equals(x.GetParentID())) && x is SchComponent).ToList();
            foreach (IComponentID componentID in root_sch_component)
                this.root.AddElementaryComponentID(new ElementaryComponentID
                { Designator = componentID.GetDesignator(), ParentID = componentID.GetParentID(), UniqueID = componentID.GetUniqueID() });
        }

        private void AddSheetSymbolsTree()
        {
            this.sheetSymbolsId = this.flat_collection.Where(x => x is SheetSymbol).Select(x => x.GetUniqueID()).ToList();
            if (this.sheetSymbolsId.Count==0)
                return;
            IComponentID rootSheetSymbol = this.flat_collection.Find(x=>x.GetUniqueID().Equals(this.sheetSymbolsId[0]));
            IComponentID parent = this.GetParentComponentID(rootSheetSymbol.GetUniqueID());
            while (parent is SchDocument)
            {
                parent = this.GetParentComponentID(rootSheetSymbol.GetUniqueID());
                rootSheetSymbol = parent;
            }

            this.flat_collection.Remove(rootSheetSymbol);

            AssemblyComponentID assembly = new AssemblyComponentID
            { ParentID=this.root.UniqueID, UniqueID = rootSheetSymbol.GetUniqueID(), Designator = rootSheetSymbol.GetDesignator()};
            this.root.AddAssemblyComponentIDB(this.BuildComponentIDTree(assembly));

            this.AddSheetSymbolsTree();
        }

        private IComponentID GetParentComponentID(string childId)
        {
            return this.flat_collection.Find(x => x.GetUniqueID().Equals(childId));
        }

        private List<IComponentID> GetChildComponentID(string parentId)
        {
            return this.flat_collection.FindAll(x => x.GetParentID().Equals(parentId));
        }

        private AssemblyComponentID BuildComponentIDTree(AssemblyComponentID parent)
        {
            List<IComponentID> elementaryComponent = this.GetChildComponentID(parent.UniqueID).Where(x=>x is SchComponent).ToList();
            foreach (IComponentID componentId in elementaryComponent)
                parent.AddElementaryComponentID(new ElementaryComponentID { Designator = componentId.GetDesignator(), ParentID = componentId.GetParentID(), UniqueID = componentId.GetUniqueID()});
            List<IComponentID> assemblyComponent = this.GetChildComponentID(parent.UniqueID).Where(x => x is SheetSymbol).ToList();
            foreach (IComponentID componentId in assemblyComponent)
                parent.AddAssemblyComponentIDB(new AssemblyComponentID { Designator = componentId.GetDesignator(), ParentID = componentId.GetParentID(), UniqueID = componentId.GetUniqueID() });
            return parent;
        }

        void IHierarchy.BuildTree()
        {
            this.root = new AssemblyComponentID();
            this.root.UniqueID = "root";

            this.sheetSymbolsId = this.flat_collection.Where(x => x is SheetSymbol).Select(x => x.GetUniqueID()).ToList();
            this.AddComponentsToRoot();

            this.AddSheetSymbolsTree();
        }
    }
}
