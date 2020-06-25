using System;
using Kompas6Constants;
using BoardBuilder.KompasBoardSpace;
using KompasAPI7;

namespace BoardBuilder.KompasAssemblySpace
{
    public class KompasAssembly
    {
        private string xmlPath;
        private IApplication kompasApp;
        private Assembly assembly;
        private IKompasDocument kompasAssembly;
        private KompasBoard kompasBoard;

        public KompasAssembly(string xmlPath, IApplication kompasApp, Assembly assembly)
        {
            this.xmlPath = xmlPath;
            this.kompasApp = kompasApp;
            this.assembly = assembly;
            this.Initialization();
            this.CreateAssembly();
            this.InsertBoard();
            /*this.WriteProjectProperties();
            this.WriteNotes();
            this.CreateVariants();
            this.Save();*/

        }

        private void InsertBoard()
        {
            this.kompasBoard = new KompasBoard(this.xmlPath, this.assembly);
            this.kompasBoard.Save();/**/
        }

        private void CreateAssembly()
        {
            this.kompasAssembly = this.kompasApp.Application.Documents.Add(DocumentTypeEnum.ksDocumentAssembly);
            /*this.kompasAssemblyDoc = this.kompasApp.Document3D();
            this.kompasAssemblyDoc.Create(false, false);*/
        }

        private void Initialization()
        {
        }
    }
}
