using IAssembly;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksBoardSpace;
using SolidworksVariants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace SolidWorksAssemblySpace
{
    class SolidworksAssembly
    {
        private SldWorks app;
        private AssemblyDoc swAssembly;
        private Assembly assembly;
        private string path;

        public AssemblyDoc SwAssembly
        { get { return this.swAssembly; }}

        public SolidworksBoard SolidWorksBoard
        { get; private set; }

        public SolidworksAssembly(string path, Assembly assembly)
        {
            this.path = path;
            this.assembly = assembly;
            this.Initialization();
            this.CreateAssembly();
            this.WriteProjectProperties();
            this.WriteNotes();
            this.InsertBoard();
            this.Save();
            this.CreateVariants();
            this.Save();
        }

        private void WriteNotes()
        {
            INotesCAD iNotesCAD = new SolidworksNotesCAD((ModelDoc2)this.swAssembly);
            Notes[] notes = this.assembly.notes;
            string[] notesRazdelNames = iNotesCAD.GetRazdelNames();
            notes = notes.Where(itemNote => Array.Exists(notesRazdelNames, razdelName => razdelName.Equals(itemNote.RazdelName))).ToArray();
            Array.ForEach(notes, itemNote => iNotesCAD.WriteNotes(itemNote));
        }

        private void WriteProjectProperties()
        {
            IProjectPropertiesCAD projectProperties = new SolidworksProjectProperties((ModelDoc2)this.swAssembly);
            string[] propertieNames = projectProperties.GetPropertieNames();
            foreach (string propertie in propertieNames)
                projectProperties.WriteValue(propertie, this.assembly.projectProperties.Propertie.Where(x => x.Name.Equals(propertie)).Select(x => x.Value).FirstOrDefault());
        }

        private void CreateVariants()
        {
            this.app.DocumentVisible(false, (int) swDocumentTypes_e.swDocPART);
            //this.app.DocumentVisible(true, (int)swDocumentTypes_e.swDocASSEMBLY);
            new SolidWorksVariants(this.assembly.variantCAD, this);
            //this.app.DocumentVisible(true, (int)swDocumentTypes_e.swDocASSEMBLY);
            this.app.DocumentVisible(true, (int)swDocumentTypes_e.swDocPART);
        }

        private void InsertBoard()
        {
            this.SolidWorksBoard = new SolidworksBoard(this.path, this.assembly);
            Component2 component = this.swAssembly.AddComponent4(this.SolidWorksBoard.FilePath, "Flat", 0, 0, 0);
            this.SolidWorksBoard.SetBoardComponent(component);
            MathUtility mathUtility = this.app.IGetMathUtility();
            double[] arrayDataIn = new double[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0  };
            component.Transform2 = mathUtility.CreateTransform(arrayDataIn);
            this.app.CloseDoc(this.SolidWorksBoard.FilePath);
        }

        private void Save()
        {
            ((ModelDoc2)this.swAssembly).SaveAs(this.path);
        }

        private void CreateAssembly()
        {
            this.swAssembly = this.app.INewAssembly();
        }

        private void Initialization()
        {
            this.app = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            string smallFileNameAssembly = !string.IsNullOrEmpty(this.assembly.projectProperties.GetPropertyByName("Обозначение")) ? this.assembly.projectProperties.GetPropertyByName("Обозначение") : Path.GetFileNameWithoutExtension(this.path);
            this.path = Path.GetDirectoryName(this.path) + @"\" + smallFileNameAssembly + ".sldasm";
        }
    }
}