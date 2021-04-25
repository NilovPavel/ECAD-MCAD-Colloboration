using AltiumNotesSpace;
using AltiumVariantsSpace;
using BoardSpace;
using EDP;
using HierarchySpace;
using IProjectProperties;
using System.Collections.Generic;

namespace IAssembly
{
    public class AltiumAssembly : IAssemblyCAD
    {
        public EDP.IWorkspace Workspace
        { get; private set; }

        public IProject Project
        { get; private set; }

        public string ProjectPath
        { get; private set; }

        public List<IDocument> Documents
        { get; private set; }

        private AltiumBoard iBoard;

        public AltiumAssembly()
        {
            this.Initialization();
            this.GetDocuments();
            this.InitializeInterfaces();
        }

        public void GetDocuments()
        {
            this.Documents = new List<IDocument>();

            for (int i = 0; i < this.Project.DM_LogicalDocumentCount(); i++)
                this.Documents.Add(this.Project.DM_LogicalDocuments(i));
        }

        private void Initialization()
        {
            this.Workspace = (EDP.IWorkspace)DXP.GlobalVars.DXPWorkSpace;

            this.Project = (IProject)this.Workspace.DM_FocusedProject();
            this.ProjectPath = this.Project.DM_ProjectFullPath();

            if (this.Project.DM_NeedsCompile())
                this.Project.DM_Compile();
        }

        private void InitializeInterfaces()
        {
            if (this.Documents.Exists(x => x.DM_DocumentKind().Equals(EDP.EDPConstant.DocKindPcb)))
                this.iBoard = new AltiumBoard(this.Documents.Find(x => x.DM_DocumentKind().Equals(EDP.EDPConstant.DocKindPcb)));
        }

        IProjectPropertiesCAD IAssemblyCAD.GetIProjectPropertiesCAD()
        {
            return new AltiumProjectProperties(this.Project);
        }

        IBoardCAD IAssemblyCAD.GetIBoardCAD()
        {
            return this.iBoard;
        }

        IHierarchyCAD IAssemblyCAD.GetIHierarchyCAD()
        {
            IHierarchyCAD iHierarchy = default(IHierarchyCAD);
            if (this.Documents.Exists(x => x.DM_DocumentKind().Equals(EDP.EDPConstant.DocKindSch)))
                iHierarchy = new AltiumHierarchy3(this.Documents.FindAll(x => x.DM_DocumentKind().Equals(EDP.EDPConstant.DocKindSch)).ToArray());
            return iHierarchy;
        }

        INotesCAD IAssemblyCAD.GetINotesCAD()
        {
            return new AltiumNotes(this.Project);
        }

        IVariantCAD IAssemblyCAD.GetIVariantCAD()
        {
            return new AltiumVariantCAD(this.Project, this.iBoard);
        }

        IHarnessCAD IAssemblyCAD.GetIHarnessCAD()
        {
            return default(IHarnessCAD);
        }
    }
}