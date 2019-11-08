using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks
{
    class SolidWorksPointInFeature
    {
        private SldWorks app;

        private Feature feature;

        private MathUtility mathUtility;
        private MathVector mathVector;

        private List<Surface> surFaces;

        public bool PointInFeature(double X, double Y, double Z)
        {
            double[] arrayDataPoint = new double[] { X, Y, Z };
            MathPoint mathPoint = (MathPoint)this.mathUtility.CreatePoint(arrayDataPoint);
            return true;
        }

        private int CycleBySurfaces(MathPoint mathPoint)
        {
            int count = 0;
            foreach (Surface surFace in surFaces)
            {
                MathPoint interSectPoint = surFace.GetProjectedPointOn(mathPoint, this.mathVector);
                if (interSectPoint != null)
                {
                    count++;
                    this.mathUtility.CreatePoint(interSectPoint.ArrayData);
                }
            }
            return count;
        }

        private void GetAllSurFaces()
        {
            object[] allFacesBody = this.feature.GetFaces();
            allFacesBody.ToList().ForEach( x => this.surFaces.Add(((Face2)x).IGetSurface()));
        }

        private void Initialization()
        {
            this.app = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            this.surFaces = new List<Surface>();
            this.mathUtility = this.app.IGetMathUtility();
            this.mathVector = this.mathUtility.CreateVector(new double[] { 0, 1, 0 });
            this.GetAllSurFaces();
        }

        public SolidWorksPointInFeature(Feature feature)
        {
            this.feature = feature;
        }
    }
}
