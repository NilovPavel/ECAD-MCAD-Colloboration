using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportsOutput
{
    public class SpecificationA
    {
        private string filePath;
        private SpecificationReport report;
        private IDocument iDocument;
        private ProjectProperties projectProperties;
        private ISpecificationWriter specificationWriterSpecificationA;
        private IConfigurationWriter configurationWriterSpecificationA;
        private IRazdelSpWriter razdelSpWriterSpecificationA;
        private IPodRazdelSpWriter podrazdelSpWriterSpecificationA;

        private void Initialization()
        {
            this.filePath = Path.GetDirectoryName(this.filePath) + @"\" + Path.GetFileNameWithoutExtension(this.filePath) + ".xls";
            this.projectProperties = this.report.Assembly.projectProperties;
            this.specificationWriterSpecificationA = new SpecificationWriterSpecificationA(this.iDocument);
            this.configurationWriterSpecificationA = new ConfigurationWriterSpecificationA(this.iDocument);
            this.razdelSpWriterSpecificationA = new RazdelSpWriterSpecificationA(this.iDocument);
            this.podrazdelSpWriterSpecificationA = new PodrazdelSpWriterSpecificationA(this.iDocument);
        }

        public SpecificationA(string filePath, AbstractReport report, IDocument iDocument)
        {
            this.filePath = filePath;
            this.report = (SpecificationReport)report;
            this.iDocument = iDocument;
            this.Initialization();
            this.WriteSpecifications();
            this.RestructureSheets();
            this.WriteProperties();
            this.WriteDocument();
        }

        private void WriteProperties()
        {
            this.iDocument.WriteDocumentProperties();
        }

        private void RestructureSheets()
        {
            this.iDocument.RemoveUnusedSheets();
            this.iDocument.WriteCountToFirstSheet();
        }

        private void WriteSpecifications()
        {
            foreach (Spisok specification in this.report.Spisok)
            {
                this.specificationWriterSpecificationA.Write(projectProperties, specification, this.report.Spisok);
                foreach (Spisok configuration in specification.Razdels)
                {
                    this.configurationWriterSpecificationA.Write(projectProperties, configuration, specification.Razdels);
                    foreach (Spisok razdel in configuration.Razdels)
                    {
                        this.razdelSpWriterSpecificationA.Write(razdel);
                        /*foreach (Spisok podrazdel in razdel.Razdels)
                            this.podrazdelSpWriterSpecificationA.Write(podrazdel, razdel.Name);*/
                    }
                }
            }
        }

        private void WriteDocument()
        {
            this.iDocument.WriteDocument(filePath);
        }
    }
}