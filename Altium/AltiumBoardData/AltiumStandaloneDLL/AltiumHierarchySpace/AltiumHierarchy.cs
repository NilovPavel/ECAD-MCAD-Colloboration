using EDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HierarchySpace
{
    class AltiumHierarchy : IHierarchyCAD
    {
        private IDocument[] iDocuments;
        private List<SchDocument> DocumentsInHierarchy;
        private List<IComponentID> flat_collection;
        private AssemblyComponentID root;
        private List<string> uniqueIds;

        public AssemblyComponentID Root
        { get { return this.root; } } 

        private void Initialization()
        {
            this.uniqueIds = new List<string>();
            this.DocumentsInHierarchy = new List<SchDocument>();
            this.iDocuments.ToList().ForEach(x => this.DocumentsInHierarchy.Add(new SchDocument(x.DM_FullPath())));
        }

        /*private void GetLinksDocuments()
        {
            List<SchDocument> searchInLinks = new List<SchDocument>();

            for (int i = 0; i < this.DocumentsInHierarchy.Count; i++)
                for (int j = 0; j < this.DocumentsInHierarchy[i].SchSheetSymbols.Count; j++)
                    if (!this.DocumentsInHierarchy.Exists(x => x.FileName.Equals(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName)))
                        searchInLinks.Add(new SchDocument(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName));

            this.DocumentsInHierarchy.AddRange(searchInLinks);
        } */

        public AltiumHierarchy(IDocument[] iDocuments)
        {
            this.iDocuments = iDocuments;
            this.Initialization();
            this.GetFlat();
            this.RewriteSSUid();
        }

        private void GetFlat()
        {
            this.flat_collection = new List<IComponentID>();
            foreach (SchDocument schDocument in this.DocumentsInHierarchy)
            {
                this.flat_collection.Add(schDocument);
                //string fullDescription = schDocument.FileName.ToString() + "\r\n";
                this.flat_collection.AddRange(schDocument.SchSheetSymbols);
                //fullDescription += schDocument.SchSheetSymbols.Select(item => item.);
                this.flat_collection.AddRange(schDocument.SchComponents);
                //string[] schComponents = schDocument.SchComponents.Select(item => ((IComponentID)item).GetDesignator()).ToArray();
                //fullDescription += (string.Join("\r\n", schComponents));
                //MessageBox.Show(fullDescription);
                //MessageBox.Show(schDocument.SchSheetSymbols.Count.ToString());
            }
        }

        private void RewriteSSUid()
        {
            for (int i = 0; i < this.DocumentsInHierarchy.Count; i++)
                for (int j = 0; j < this.DocumentsInHierarchy[i].SchSheetSymbols.Count; j++)
                    this.DocumentsInHierarchy[i].SchSheetSymbols[j].UniqueIdtoUniqueIdSheet(((IComponentID)this.DocumentsInHierarchy.Find(x => x.FileName.Equals(this.DocumentsInHierarchy[i].SchSheetSymbols[j].FileName))).GetUniqueID());
        }

        private IComponentID GetParentComponentID(string childId)
        {
            return this.flat_collection.Find(x => x.GetUniqueID().Equals(childId));
        }

        private List<IComponentID> GetChildComponentID(string parentId)
        {
            return this.flat_collection.FindAll(x => x.GetParentID().Equals(parentId));
        }

        private List<IComponentID> ConcatEqualsChildrens(List<IComponentID> components)
        {
            List<IComponentID> concatCollection = new List<IComponentID>();
            string[] equalsUniqueIDs = components.Select(x=>x.GetUniqueID()).Distinct().ToArray();
            foreach(string equalsUniqueID in equalsUniqueIDs)
            {
                int count = components.Count(x => x.GetUniqueID().Equals(equalsUniqueID));
                concatCollection.Add( count > 1 ? new SchChanell
                {
                    UniqueID = equalsUniqueID
                    ,Designator = "Repeat("+ components.Find(x => x.GetUniqueID().Equals(equalsUniqueID)).GetDesignator() + ",1,"
                    + count + ")"
                } : components.Find(x => x.GetUniqueID().Equals(equalsUniqueID)));
            }
            return concatCollection;
        }

        private AssemblyComponentID BuildComponentIDTree(AssemblyComponentID parent)
        {
            string[] split_uids = parent.UniqueID.Split('\\');
            string parentUniqueId = split_uids.Last();

            if (parentUniqueId.Equals("MYMSXVOS"))
                ;

            List<IComponentID> elementaryComponent = this.GetChildComponentID(parentUniqueId).Where(x=>x is SchComponent).ToList();
            foreach (IComponentID componentId in elementaryComponent)
            {
                parent.AddElementaryComponentID(new ElementaryComponentID { Designator = componentId.GetDesignator(), ParentID = parent.UniqueID, UniqueID = @"\" + componentId.GetUniqueID() });
                this.uniqueIds.Add(componentId.GetUniqueID());
            }

            List<IComponentID> assemblyComponent = this.ConcatEqualsChildrens(this.GetChildComponentID(parentUniqueId).Where(x => x is SheetSymbol).ToList());
            foreach (IComponentID componentId in assemblyComponent)
                if (!split_uids.Contains(componentId.GetUniqueID()))
                    parent.AddAssemblyComponentIDB(this.BuildComponentIDTree(new AssemblyComponentID { Designator = componentId.GetDesignator()
                        , ParentID = parent.UniqueID, UniqueID = parent.UniqueID + @"\" + componentId.GetUniqueID(),
                        Наименование = ((SheetSymbol)componentId).GetNaimenovanie(), Обозначение = ((SheetSymbol)componentId).GetOboznachenie()}));

            return parent;
        }

        AssemblyComponentID IHierarchyCAD.BuildTree()
        {
            //Чтобы не уйти в бесконечную рекурсию, вводится коллекция ID, которые уже присутствовали в "подъеме по дереву"
            IComponentID root_sheet = this.flat_collection.Where(x => x is SchDocument).First();
            IComponentID parent_sheetSymdol;
            while ((parent_sheetSymdol = this.flat_collection.Find(x => x is SheetSymbol && x.GetUniqueID().Equals(root_sheet.GetUniqueID()))) != null && !uniqueIds.Exists(x => x.Equals(root_sheet.GetParentID())))
            {
                this.uniqueIds.Add(root_sheet.GetParentID());
                root_sheet = this.flat_collection.Find(x => x is SchDocument && x.GetUniqueID().Equals(parent_sheetSymdol.GetParentID()));
            }
            
            this.root = this.BuildComponentIDTree(new AssemblyComponentID { Designator = root_sheet.GetDesignator(), ParentID = root_sheet.GetUniqueID(),  UniqueID = @"\" +root_sheet.GetUniqueID() });

            IEnumerable<IComponentID> notInUniqueIds = this.flat_collection.Where(item => !this.uniqueIds.Contains(item.GetUniqueID()) && item is SchComponent);
            notInUniqueIds.ToList().ForEach(item => this.root.AddElementaryComponentID(new ElementaryComponentID { Designator = item.GetDesignator(), ParentID = this.root.UniqueID, UniqueID = @"\" + item.GetUniqueID() }));

            return this.root;
        }

    }
}
