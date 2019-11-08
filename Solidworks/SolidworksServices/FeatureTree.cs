using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksServices
{
    class FeatureTree
    {
        private Feature rootFeature;
        private List<Feature> flatFeatures;

        private void buildTree(Feature feature)
        {
            for (Feature swSubFeat = (Feature)feature.GetFirstSubFeature(); swSubFeat != null; swSubFeat = swSubFeat.GetNextSubFeature())
            {
                flatFeatures.Add(swSubFeat);
                this.buildTree(swSubFeat);
            }
        }

        private void Action()
        {
        }

        private void Initialization()
        {
            this.flatFeatures = new List<Feature>();
        }

        public FeatureTree(Feature rootFeature)
        {
            this.rootFeature = rootFeature;
            this.Initialization();
            this.buildTree(this.rootFeature);
            this.flatFeatures.Insert(0, this.rootFeature);
            this.Action();
        }
    }
}
