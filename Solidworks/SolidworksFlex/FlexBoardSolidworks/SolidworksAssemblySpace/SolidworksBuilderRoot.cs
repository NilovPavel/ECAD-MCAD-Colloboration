using System;
using System.IO;
using System.Xml.Serialization;

namespace SolidWorksAssemblySpace
{
    class SolidworksBuilderRoot : IBuilderCAD
    {
        public SolidworksBuilderRoot(string pathXml) : base(pathXml)
        {
            new SolidworksAssembly(this.pathXml, this.assembly);
        }
    }
}
