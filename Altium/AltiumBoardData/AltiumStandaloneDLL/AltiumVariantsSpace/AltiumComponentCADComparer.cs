using System.Collections.Generic;

namespace AltiumVariantsSpace
{
    class AltiumComponentCADComparer : IEqualityComparer<IComponentCAD>
    {
        bool IEqualityComparer<IComponentCAD>.Equals(IComponentCAD x, IComponentCAD y)
        {
            return x.GetUniqueID().Equals(y.GetUniqueID()) && x.GetConfiguration().Equals(y.GetConfiguration()) && x.GetFitted() == y.GetFitted();
        }

        int IEqualityComparer<IComponentCAD>.GetHashCode(IComponentCAD obj)
        {
            return 0;
        }
    }
}
