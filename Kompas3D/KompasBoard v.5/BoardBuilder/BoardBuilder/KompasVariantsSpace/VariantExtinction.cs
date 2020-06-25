using System.Collections.Generic;
using System.Linq;
using BoardBuilder.KompasAssemblySpace;
using Kompas6API7;
using KompasComparersSpace;
using Kompas6Constants;
using Kompas6API5;

namespace KompasVariantsSpace
{
    class VariantExtinction
    {
        private Variant variant;
        private IEmbodimentsManager embodimentsManager;
        private Dictionary<ComponentCAD, IPart7> conformity;
        private KompasAssembly kompasAssembly;

        private void Initialization()
        {
            int currentEmbodyIndex = 0;
            this.embodimentsManager = (IEmbodimentsManager)this.kompasAssembly.AssemblyDocument;
            for (int variantCount = 0; variantCount < this.embodimentsManager.EmbodimentCount; variantCount++)
                if (this.embodimentsManager.Embodiment[variantCount].
                    GetMarking(ksVariantMarkingTypeEnum.ksVMBaseMarking, false).Equals(this.variant.VariantName))
                {
                    currentEmbodyIndex = variantCount;
                    break;
                }

            this.embodimentsManager.SetCurrentEmbodiment(currentEmbodyIndex);
        }

        private void SetFootprints()
        {
            foreach (KeyValuePair<ComponentCAD, IPart7> componentPair in this.conformity)
            {
                IEmbodimentsManager embodimentsManager = (IEmbodimentsManager)componentPair.Value;
                embodimentsManager.SetCurrentEmbodiment(componentPair.Key.Configuration);
            }
            this.kompasAssembly.AssemblyDocument.TopPart.Update();
        }

        private void Extinction()
        {
            foreach (KeyValuePair<ComponentCAD, IPart7> componentPair in this.conformity)
            {
                IFeature7 feature = ((IFeature7)componentPair.Value);
                feature.Excluded = !componentPair.Key.Fitted;
            }

            this.kompasAssembly.AssemblyDocument.TopPart.Update();
        }

        public VariantExtinction(Variant eachVariant, ref Dictionary<ComponentCAD, IPart7> conformity, KompasAssembly kompasAssembly)
        {
            this.variant = eachVariant;
            this.conformity = conformity;
            this.kompasAssembly = kompasAssembly;
            this.Initialization();
            this.Extinction();
            this.SetFootprints();
        }
    }
}