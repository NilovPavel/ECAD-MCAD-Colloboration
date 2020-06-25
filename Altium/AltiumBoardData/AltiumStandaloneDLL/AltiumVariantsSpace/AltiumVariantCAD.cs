using BoardSpace;
using EDP;
using PCB;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms;

namespace AltiumVariantsSpace
{
    class AltiumVariantCAD : IVariantCAD
    {
        private IBoardCAD iBoard;
        private IProject project;
        private List<IVariant> variants;
        private int[] readersComponentKind;

        public List<string> UniqueIDs
        { get; private set; }

        private List<IComponent> ProjectComponents
        { get; set; }

        private List<IPCB_Component> PcbComponents
        { get; set; }

        public List<IComponentCAD> IComponentsCad
        { get; set; }

        public AltiumVariantCAD(IProject project, IBoardCAD iBoard)
        {
            this.project = project;
            this.iBoard = iBoard;
            this.Initialization();
            if (this.project.DM_LogicalDocumentCount() == 0 || this.project.DM_DocumentFlattened() == null) return;
            this.ReadProjectComponents();
            this.ReadBoardComponents();
            if(this.iBoard != null)
                if (!this.CheckProjectWithPCB())
                    return;
            this.BuildPcbSchComponents();
            this.MakeVariants();
        }

        private IComponentCAD GetBoardIComponent()
        {
            IComponentCAD boardComponentCAD = this.iBoard != null ? new AltiumBoardIComponentCAD(this.project, this.iBoard) : default(AltiumBoardIComponentCAD);
            return boardComponentCAD;
        }

        private bool CheckProjectWithPCB()
        {
            bool allRight = false;
            List<string> pcbIds = this.PcbComponents.Select(x => x.GetState_SourceUniqueId()).Where(x=>x!=null).Where(x => x.IndexOf("@") == -1).ToList();

            List<string> difference;
            if (this.UniqueIDs.Count > pcbIds.Count)
            {
                difference = this.UniqueIDs.Except(pcbIds).ToList();
                MessageBox.Show("difference(Project):\r\n" + string.Join("\r\n", this.ProjectComponents.Where(x => difference.ToList().Contains(x.DM_UniqueId())).Select(x => x.DM_PhysicalDesignator())));
            }
            else if (pcbIds.Count > this.UniqueIDs.Count)
            {
                difference = pcbIds.Except(this.UniqueIDs).ToList();
                MessageBox.Show("difference(PCB):\r\n" + string.Join("\r\n", this.PcbComponents.Where(x => difference.ToList().Contains(x.GetState_SourceUniqueId())).Select(x => x.GetState_SourceDesignator())));
            }
            else allRight = true;

            return allRight;
        }

        private void MakeVariants()
        {
            IComponentCAD componentBoard = this.GetBoardIComponent();
            List<AltiumVariant> altiumVariants = new List<AltiumVariant>();
            for (int i = 0; i < this.project.DM_ProjectVariantCount(); i++)
            {
                AltiumVariant altiumVariant = new AltiumVariant(this.project.DM_ProjectVariants(i), this);
                if (componentBoard != null)
                    altiumVariant.AddComponent(componentBoard);
                altiumVariants.Add(altiumVariant);
            }
            this.variants.AddRange(altiumVariants);
            return;
        }

        private void BuildPcbSchComponents()
        {
            /*MessageBox.Show(string.Join("\r\n", this.PcbComponents.Select(item => item.GetState_SourceUniqueId() + "\t" + item.GetState_SourceDesignator()).ToArray()));
            return;*/
            for (int i = 0; i < this.ProjectComponents.Count; i++)
            {
                
                IPCB_Component pcbComponent = this.PcbComponents.Find(x => x.GetState_SourceUniqueId().Equals(this.ProjectComponents[i].DM_UniqueId()));
                string uniqueId = this.ProjectComponents[i].DM_UniqueId().IndexOf("@")!=-1 ?
                     this.ProjectComponents[i].DM_UniqueId().Substring(0, this.ProjectComponents[i].DM_UniqueId().IndexOf("@")) : this.ProjectComponents[i].DM_UniqueId();
                if (pcbComponent== null)
                    pcbComponent = this.PcbComponents.Find(x => x.GetState_SourceUniqueId().Equals(uniqueId));

                this.IComponentsCad.Add(new AltiumComponentCAD(this.ProjectComponents[i], pcbComponent));
            }
        }

        private void ReadBoardComponents()
        {
            this.PcbComponents = new List<IPCB_Component>();
            if (this.iBoard == null) return;
            this.PcbComponents = ((AltiumBoard)this.iBoard).GetBoardComponents().Where(x => this.readersComponentKind.Contains((int) x.GetState_ComponentKind())).ToList();
        }

        private void Initialization()
        {
            this.variants = new List<IVariant>();
            this.readersComponentKind = new int[] { (int)TComponentKind.eComponentKind_Jumper, (int)TComponentKind.eComponentKind_Standard };
            this.IComponentsCad = new List<IComponentCAD>();
        }

        private void ReadProjectComponents()
        {
            IDocument documentFlattened = this.project.DM_DocumentFlattened();
            this.ProjectComponents = new List<IComponent>();
            for (int i = 0; i < documentFlattened.DM_ComponentCount(); i++)
                if(Array.Exists(readersComponentKind, item => item == (int)documentFlattened.DM_Components(i).DM_ComponentKind()))
                    this.ProjectComponents.Add(documentFlattened.DM_Components(i));
            this.UniqueIDs = this.ProjectComponents.Where(x => x.DM_UniqueId().IndexOf("@") == -1).Select(x => x.DM_UniqueId()).ToList();
        }

        IVariant[] IVariantCAD.GetIVariants()
        {
            return this.variants.ToArray();
        }
    }
}