using KompasApplicationSpace;
using BoardBuilder.KompasAssemblySpace;
using System.Xml.Serialization;
using System.IO;

namespace ExecuteBuilder
{
    public class Builder
    {
        private string xmlPath;
        private KompasApplication kompasApplication;
        private KompasAssembly kompasAssembly;
        private Assembly assembly;

        private void Initialization()
        {
            this.kompasApplication = new KompasApplication();

            this.kompasApplication.CloseOpenDocuments();

            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));

            using (FileStream fs = new FileStream(this.xmlPath, FileMode.Open, FileAccess.Read))
            {
                this.assembly = (Assembly)formatter.Deserialize(fs);
            }

        }

        public Builder(string xmlPath)
        {
            this.xmlPath = xmlPath;
            this.Initialization();
            this.kompasAssembly = new KompasAssembly(this.xmlPath, this.kompasApplication.Kompas, this.assembly);
        }
    }
}
