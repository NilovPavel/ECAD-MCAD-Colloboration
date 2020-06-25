using System;
using Kompas6API5;
using Kompas6Constants3D;
using BoardBuilder.KompasBoardSpace;
using Kompas6API7;
using System.Runtime.InteropServices;
using System.IO;
using BoardBuilder.KompasVariantsSpace;

namespace BoardBuilder.KompasAssemblySpace
{
    public class KompasAssembly
    {
        private string xmlPath;
        private KompasObject kompasApp;
        private Assembly assembly;
        private ksDocument3D kompasAssemblyDoc;
        private KompasBoard kompasBoard;

        private IKompasDocument3D assemblyDocument;

        public IKompasDocument3D AssemblyDocument
        { get { return this.assemblyDocument; }  }

        public string IAssemblyPath { get; private set; }

        public KompasAssembly(string xmlPath, KompasObject kompasApp, Assembly assembly)
        {
            this.xmlPath = xmlPath;
            this.kompasApp = kompasApp;
            this.assembly = assembly;
            this.Initialization();
            this.CreateAssembly();
            this.InsertBoard();
            /*this.WriteProjectProperties();
            this.WriteNotes();*/
            this.Hide();
            this.CreateVariants();
            this.Save();

        }

        private void Hide()
        {
            this.kompasAssemblyDoc.hideAllPlaces = true;
            this.kompasAssemblyDoc.hideAllPlanes = true;
            this.kompasAssemblyDoc.ZoomPrevNextOrAll((short)2); //изображения,   
        }

        private void CreateVariants()
        {
            new KompasVariants(this.assembly.variantCAD, this);
        }

        private void Save()
        {
            this.kompasAssemblyDoc.SaveAs(this.IAssemblyPath);
            //this.kompasAssemblyDoc.close();
        }

        private void InsertBoard()
        {
            this.kompasBoard = new KompasBoard(this.xmlPath, this.assembly);
            this.kompasBoard.Save();
            Part7 part = this.assemblyDocument.TopPart.Parts.AddFromFile(this.kompasBoard.IPartPath, false, true);
            part.Fixed = true;
        }

        private void CreateAssembly()
        {
            this.kompasAssemblyDoc = this.kompasApp.Document3D();
            this.kompasAssemblyDoc.Create(false, false);

            this.assemblyDocument = (IKompasDocument3D)((IApplication)Marshal.GetActiveObject("KOMPAS.Application.7")).ActiveDocument;
        }

        private void Initialization()
        {
            this.IAssemblyPath = Path.GetDirectoryName(this.xmlPath) + @"\" + Path.GetFileNameWithoutExtension(this.xmlPath) + ".a3d";
        }
    }
}
