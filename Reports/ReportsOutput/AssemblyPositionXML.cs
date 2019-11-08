using System;
using System.IO;
using System.Xml.Serialization;

namespace ReportsOutput
{
    internal class AssemblyPositionXML
    {
        private SpecificationReport report;
        private Assembly assembly;

        public AssemblyPositionXML(SpecificationReport report)
        {
            this.report = report;
            this.Initialization();
            this.WritePositionToAssembly();
        }

        private void GetPositionFromVariant(string variantName, ComponentCAD componentCad)
        {
            componentCad.dataESKD.Позиция = this.report.GetNumberByElement(variantName, componentCad);
        }

        private void WritePositionToAssembly()
        {
            Array.ForEach(this.report.Assembly.variantCAD.variant, item => item.ComponentCAD.ForEach(element => this.GetPositionFromVariant(item.VariantName, element)));
        }

        public void WritePosition(string currentPath)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));
            using (FileStream fs = new FileStream(currentPath, FileMode.Create))
            {
                formatter.Serialize(fs, this.assembly);
            }
        }

        private void Initialization()
        {
            this.assembly = this.report.Assembly;
        }
    }
}