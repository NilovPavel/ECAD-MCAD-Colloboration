using ReportsOutput;
using ReportsOutput.Perechen.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOutput
{
    public class PerechenA
    {
        private string filePath;
        private PerechenReport report;
        private IDocument iDocument;
        private ProjectProperties projectProperties;
        private IConfigurationWriter configurationWriterPerechen;
        private PerechenNodeWriter nodeWriterPerechen;

        private void Initialization()
        {
            this.filePath = Path.GetDirectoryName(this.filePath) + @"\" + Path.GetFileNameWithoutExtension(this.filePath) + " ПЭ3.xls";
            this.projectProperties = this.report.Assembly.projectProperties;
            this.configurationWriterPerechen = new ConfigurationWriterPerechen(this.iDocument);
            this.nodeWriterPerechen = new PerechenNodeWriter(this.iDocument);
            //this.podrazdelSpWriterSpecificationA = new PodrazdelSpWriterSpecificationA(this.iDocument);
        }

        public PerechenA(string filePath, AbstractReport report, IDocument iDocument)
        {
            this.filePath = filePath;
            this.report = (PerechenReport)report;
            this.iDocument = iDocument;
            this.Initialization();
            this.WriteConfigurations();
            this.RestructureSheets();
            this.WriteProperties();
            this.WriteDocument();
        }

        private void WriteConfigurations()
        {
            Spisok perechenRoot = this.report.Spisok.FirstOrDefault();
            foreach (Spisok configuration in perechenRoot.Razdels)
            {
                this.configurationWriterPerechen.Write(projectProperties, configuration, perechenRoot.Razdels);

                configuration.Razdels.ForEach(item => this.nodeWriterPerechen.WriteNodes(item));
            }
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

        private void WriteDocument()
        {
            this.iDocument.WriteDocument(filePath);
        }
    }
}
