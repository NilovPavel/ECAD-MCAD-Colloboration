using EDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HierarchySpace
{
    public class AltiumHierarchy2 : IHierarchyCAD
    {
        private IDocument[] iDocuments;
        private List<SchDocument> documentsInHierarchy;
        private List<ISchHierarchyType> flatCollection;
        private List<ISchHierarchyType> roots;

        public AltiumHierarchy2(IDocument[] iDocuments)
        {
            this.iDocuments = iDocuments;
            this.Initialization();
            this.GetFlat();
        }

        private void GetFlat()
        {
            foreach (SchDocument schDocument in this.documentsInHierarchy)
            {
                this.flatCollection.Add(schDocument);
                this.flatCollection.AddRange(schDocument.SchSheetSymbols);
                this.flatCollection.AddRange(schDocument.SchComponents);
            }
        }

        private void Initialization()
        {
            this.documentsInHierarchy = new List<SchDocument>();
            this.flatCollection = new List<ISchHierarchyType>();
            Array.ForEach(iDocuments, item => this.documentsInHierarchy.Add(new SchDocument(item.DM_FullPath())));
        }

        private SchDocument GetSchDocumentFromSheetSymbol(SheetSymbol sheetSymbol)
        {
            return (SchDocument)this.flatCollection.
                Where(item => item.GetHierarchyType() == eHierarchyType.scheetDocument).
                Where( item => ((SchDocument)item).FileName.Equals(sheetSymbol.FileName)).FirstOrDefault();
        }

        private SheetSymbol GetSheetSymbolFromSchDocument(SchDocument scheetDocument)
        {
            return (SheetSymbol)this.flatCollection.
                Where(item => item.GetHierarchyType() == eHierarchyType.scheetSymbol).
                Where(item => ((SheetSymbol)item).FileName.Equals(scheetDocument.FileName)).FirstOrDefault();
        }

        private IComponentID GetParentComponent(string uniqueId)
        {
            ISchHierarchyType componentID = this.flatCollection.Where(item => item.GetUniqueID().Equals(uniqueId)).FirstOrDefault();
            switch (componentID.GetHierarchyType())
            {
                case eHierarchyType.scheetDocument:
                    componentID = this.GetSheetSymbolFromSchDocument((SchDocument)componentID);
                    break;
                default:
                    componentID = this.flatCollection.Where(item => item.GetUniqueID().Equals(componentID.GetParentID())).FirstOrDefault();
                    break;
            }
            return componentID;
        }

        AssemblyComponentID IHierarchyCAD.BuildTree()
        {
            string uniqueId = "IGNIPKMG";

            IComponentID componentID = this.GetParentComponent(uniqueId);

            return new AssemblyComponentID { Designator = string.Empty, ParentID = string.Empty,
                UniqueID = string.Empty, Наименование = string.Empty, Обозначение = string.Empty};
        }
    }
}
