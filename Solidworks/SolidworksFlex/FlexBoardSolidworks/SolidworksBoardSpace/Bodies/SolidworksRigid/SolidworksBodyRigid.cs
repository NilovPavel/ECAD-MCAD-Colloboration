using SolidWorks.Interop.sldworks;
using SolidworksBoardSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks.SolidworksBoardSpace
{
    class SolidworksBodyRigid : AbstractSolidworksBody
    {
        private static int countRigid;

        public SolidworksBubble SolidworksBubble
        { get; private set; }

        public SolidworksBodyRigid(Body body) : base(body)
        {
            //this.BuildBubble();
            /*SolidworksBubbleBody bubbleBody = new SolidworksBubbleBody(this.body);
            Body bodyBubble = new Body(bubbleBody);
            this.solidworksBubble = new SolidworksBubble(this.body);*/
            /*this.sketchManager.Insert3DSketch(true);
            this.modelDoc.ClearSelection();
            this.sketchManager.CreatePoint(this.solidworksBubble.OriginRectanglePoint.X / 1000, this.solidworksBubble.OriginRectanglePoint.Y / 1000, this.body.originHeight / 1000);
            this.sketchManager.Insert3DSketch(true);*/
        }

        public bool GetPointInContourOrNot()
        {
            return true;
        }

        public void BuildBubble()
        {
            SolidworksBubbleBody bubbleBody = new SolidworksBubbleBody(this.Body);
            Body bodyBubble = new Body(bubbleBody);
            this.SolidworksBubble = new SolidworksBubble(bodyBubble);
        }

        protected override void BuildBody()
        {
            this.absFeature = this.featureManager.FeatureExtrusion(true, false, false, 0, 0, this.Body.totalHeight / 1000, 0, false, false, false, false, 0, 0, false, false, false, false, true, false, false);

            if (this.absFeature != null)
                this.absFeature.Name = "RigidBody" + countRigid++;
        }
    }
}
