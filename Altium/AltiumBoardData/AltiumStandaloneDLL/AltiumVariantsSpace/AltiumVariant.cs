using EDP;
using System.Collections.Generic;
using System;
using PCB;
using System.Windows.Forms;

namespace AltiumVariantsSpace
{
    class AltiumVariant : IVariant
    {
        private IProjectVariant projectVariant;
        private AltiumVariantCAD altiumVariantCAD;
        private List<IComponentCAD> projectComponentsCad;
        private string projectVariantName;
        private Dictionary<string, bool> fittedPairs;

        public AltiumVariant(IProjectVariant projectVariant, AltiumVariantCAD altiumVariantCAD)
        {
            this.projectVariant = projectVariant;
            this.altiumVariantCAD = altiumVariantCAD;
            this.projectVariantName = this.projectVariant.DM_Description();
            this.ReadNotFittedElements();
            this.projectComponentsCad = this.GetComponentFromVariant();
        }

        public AltiumVariant(string projectVariantName, List<IComponentCAD> projectComponentsCad)
        {
            this.projectVariantName = projectVariantName;
            this.projectComponentsCad = projectComponentsCad;
        }

        public void RemoveRepeatInOtherVariants(string[] repeatUniqueIds)
        {
            Array.ForEach(repeatUniqueIds, x => this.projectComponentsCad.RemoveAll(y => y.GetUniqueID().Equals(x)));
        }

        private void ReadNotFittedElements()
        {
            this.fittedPairs = new Dictionary<string, bool>();
            for (int i = 0; i < this.projectVariant.DM_VariationCount(); i++)
                this.fittedPairs.Add(this.projectVariant.DM_Variations(i).DM_UniqueId(), this.projectVariant.DM_Variations(i).DM_VariationKind() != TVariationKind.eVariation_NotFitted);
        }

        private List<IComponentCAD> GetComponentFromVariant()
        {
            List<IComponentCAD> variantComponentsCad = new List<IComponentCAD>();
            for (int i = 0; i < this.altiumVariantCAD.UniqueIDs.Count; i++)
            {
                IComponentCAD iProjectVariantComponent = this.altiumVariantCAD.IComponentsCad.Find(x => x.GetUniqueID().Equals(this.altiumVariantCAD.UniqueIDs[i] + "@" + this.projectVariantName));
                if (iProjectVariantComponent == null)
                    iProjectVariantComponent = this.altiumVariantCAD.IComponentsCad.Find(x => x.GetUniqueID().Equals(this.altiumVariantCAD.UniqueIDs[i]));

                iProjectVariantComponent = iProjectVariantComponent.Clone();

                bool isFitted = !this.fittedPairs.ContainsKey(iProjectVariantComponent.GetUniqueID())? true : this.fittedPairs[iProjectVariantComponent.GetUniqueID()];
                iProjectVariantComponent.SetFitted(isFitted);
                variantComponentsCad.Add(iProjectVariantComponent);
            }
            return variantComponentsCad;
        }

        string IVariant.GetVariantName()
        {
            return this.projectVariantName;
        }

        IComponentCAD[] IVariant.GetCollection()
        {
            return this.projectComponentsCad.ToArray();
        }

        public void AddComponent(IComponentCAD componentBoard)
        {
            this.projectComponentsCad.Add(componentBoard);
        }
    }
}