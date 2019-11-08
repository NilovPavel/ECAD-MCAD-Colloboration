using SolidWorks.Interop.sldworks;
using SolidworksBoardData.SolidworksVariantCAD;
using System.Collections.Generic;
using System;

namespace SolidworksBoardData
{
    public class SpecificationVariantSolidworksCAD : VariantSolidworksCAD, IVariantCAD
    {
        private FeatureManager featureManager;
        private List<string> electricalInstallationCollection;

        public List<string> ElectricalInstallationCollection
        {
            get
            {
                return this.electricalInstallationCollection;
            }
        }

        public SpecificationVariantSolidworksCAD(AssemblyDoc assemblyModelDoc) : base(assemblyModelDoc)
        {
            this.topLevel = true;
            this.readCoordinats = false;
            this.Initialization();
            this.ReadMontazh();
        }

        private void Initialization()
        {
            this.electricalInstallationCollection = new List<string>();
            this.featureManager = ((ModelDoc2)this.assemblyModelDoc).FeatureManager;
        }

        private string[] GetNamesElectricalInstallationPattern(Feature electricalInstallation)
        {
            List<string> featureNames = new List<string>();

            for (Feature swSubFeat = (Feature)electricalInstallation.GetFirstSubFeature(); swSubFeat != null; swSubFeat = swSubFeat.GetNextSubFeature())
                featureNames.Add(swSubFeat.Name);

            return featureNames.ToArray();
        }

        private void ReadMontazh()
        {
            for (Feature swFeat = (Feature)((ModelDoc2)this.assemblyModelDoc).FirstFeature(); swFeat != null; swFeat = (Feature)swFeat.GetNextFeature())
            {
                if ((swFeat.Name.IndexOf("Электромонтаж") != -1 & !swFeat.Name.Contains("EndTag")))
                {
                    IFeatureFolder swFeatFolder = (IFeatureFolder)swFeat.GetSpecificFeature2();
                    object[] Features = (object[])swFeatFolder.GetFeatures();

                    for (int i = 0; i < swFeatFolder.GetFeatureCount(); i++)
                    {
                        Feature swFeatMontazh = (Feature)Features[i];
                        if (swFeatMontazh.GetTypeName2().IndexOf("Pattern") != -1)
                            this.electricalInstallationCollection.AddRange(this.GetNamesElectricalInstallationPattern(swFeatMontazh));
                        else this.electricalInstallationCollection.Add(swFeatMontazh.Name);
                    }
                }
            }
        }

        IVariant[] IVariantCAD.GetIVariants()
        {
            IVariant[] variants = new IVariant[this.configurationNames.Length];
            for (int variantCount = 0; variantCount < variants.Length; variantCount++)
            {
                string variantName = this.configurationNames[variantCount];
                variants[variantCount] = new SpecificationVariantSolidworks(variantName, this.assemblyModelDoc, this);
            }
            return variants;
        }
    }
}