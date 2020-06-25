using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutableReportESKD
{
    public class BehaivorAltium
    {
        private Assembly assembly;

        public void RewriteIDs()
        {
            Array.ForEach(this.assembly.variantCAD.variant, variantItem => this.RewriteVariantIDs(variantItem));
        }

        private string GetCorrectAltiumID(string altiumId)
        {
            string altiumLastID = @"\" +  altiumId.Split('\\').Last();
            int dogExist = altiumLastID.IndexOf('@');
            altiumLastID = dogExist == -1 ? altiumLastID : altiumLastID.Substring(0, altiumLastID.IndexOf('@'));
            return altiumLastID;
        }

        private void RewriteVariantIDs(Variant variantItem)
        {
            variantItem.ComponentCAD.ForEach(componentItem => componentItem.UniqueID = this.GetCorrectAltiumID(componentItem.UniqueID));
        }

        public BehaivorAltium(Assembly assembly)
        {
            this.assembly = assembly;

        }
    }
}
