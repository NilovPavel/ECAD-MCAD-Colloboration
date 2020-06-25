using System.Collections.Generic;

namespace SearchESKD
{
    public class ProxySearch : ISearchESKD
    {
        private ISearchESKD iSearch;
        private Dictionary<DataESKD, string> cache;

        private void Initialization()
        {
            this.cache = new Dictionary<DataESKD, string>(new SearchESKDComparer());
        }

        string ISearchESKD.GetPath(DataESKD eskdData)
        {
            string path;
            if (!this.cache.TryGetValue(eskdData, out path))
            {
                path = ((ISearchESKD)this.iSearch).GetPath(eskdData);
                cache.Add(eskdData, path);
            }
            return path;
        }

        public ProxySearch(ISearchESKD iSearch)
        {
            this.iSearch = iSearch;
            this.Initialization();
        }
    }
}
