using EDP;
using HierarchySpace;
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

        public AltiumAssembly()
        {
            this.Initialization();
            this.GetActiveAssembly();
            this.GetDocuments();
            this.InitializeInterfaces();
        }

        public void GetDocuments()
        {
            this.Documents = new List<IDocument>();

            for (int i = 0; i < this.Project.DM_LogicalDocumentCount(); i++)
                this.Documents.Add(this.Project.DM_LogicalDocuments(i));
        }

        protected override void Initialization()
        {
            this.Workspace = (EDP.IWorkspace)DXP.GlobalVars.DXPWorkSpace;
        }

        protected override void GetActiveAssembly()
        {
            this.Project = (IProject)this.Workspace.DM_FocusedProject();
            this.ProjectPath = this.Project.DM_ProjectFullPath();
        }

        protected override void InitializeInterfaces()
        {
            if (this.Documents.Exists(x => x.DM_DocumentKind().Equals(EDP.EDPConstant.DocKindSch)))
            {
                this.iHierarchy = new AltiumHierarchy(this.Documents.FindAll(x => x.DM_DocumentKind().Equals(EDP.EDPConstant.DocKindSch)).ToArray());
                this.iHierarchy.BuildTree();
            }
        }
    }
}
