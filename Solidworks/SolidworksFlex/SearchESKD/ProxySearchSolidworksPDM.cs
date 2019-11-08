using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchESKD
{
    public class ProxySearchSolidworksPDM : ISearchESKD
    {
        private SearchSolidworksPDM seaarchSWEPDM;
        private Dictionary<DataESKD, string> cache;

        private void Initialization()
        {
            this.seaarchSWEPDM = new SearchSolidworksPDM();
            this.cache = new Dictionary<DataESKD, string>(new SearchESKDComparer());
        }

        string ISearchESKD.GetPath(DataESKD eskdData)
        {
            string path;
            if (!this.cache.TryGetValue(eskdData, out path))
            {
                path = ((ISearchESKD)this.seaarchSWEPDM).GetPath(eskdData);
                cache.Add(eskdData, path);
            }
            return path;
        }

        public ProxySearchSolidworksPDM()
        {
            this.Initialization();
        }
    }
}
