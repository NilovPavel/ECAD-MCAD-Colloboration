using ReportsOutput.ExcelConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOutput.Perechen.Writers
{
    public class ConfigurationWriterPerechen : IConfigurationWriter
    {
        private IIteratorAction iteratorAction;
        private IDocument iDocument;
        private IDocumentCellStyle iDocumentCellStyle;
        private IDocumentFont iDocumentFont;

        private void Initialization()
        {
            this.SetCellStyleSettings();
        }

        private void SetCellStyleSettings()
        {
            this.iDocumentCellStyle = this.iDocument.CellStyle;
            this.iDocumentFont = this.iDocumentCellStyle.IDocumentFont;

            this.iDocumentFont.Bold = true;
            this.iDocumentFont.Italic = true;
            this.iDocumentFont.UnderLine = true;

            this.iDocumentCellStyle.SetFont(this.iDocumentFont);

            this.iDocumentCellStyle.HorizlontalCenter = true;
            this.iDocumentCellStyle.VerticalCenter = true;
        }

        void IConfigurationWriter.Write(ProjectProperties projectProperties, Spisok configuration, List<Spisok> configurations)
        {
            if (configurations.Count > 1 && configuration.Equals(configurations[1]))
                this.WriteVariableData();

            if (!(configuration.Equals(configurations.First()) || configurations.Count == 1))
                this.WriteVariableConfigurationName(projectProperties, configuration);

            if (configuration.Razdels.Count == 0 && configuration.Elements.Length == 0)
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, "Отсутствуют");
        }

        private void WriteVariableConfigurationName(ProjectProperties projectProperties, Spisok configuration)
        {
            this.iDocument.CurrentRowIndex += 2;

            string configurationName = projectProperties.GetPropertyByName("Обозначение") + (configuration.Name.Trim().Equals("-00") ? string.Empty : configuration.Name);

            IteratorSpisok iteratorSpisok = new IteratorSpisok(configuration);
            this.iteratorAction = new IteratorStringCount(this.iDocument);
            iteratorSpisok.SetIteratorAction(this.iteratorAction);
            iteratorSpisok.Iteration();

            if (this.iDocument.CurrentRowIndex + ((IteratorStringCount)this.iteratorAction).SpisokCountSpace >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();

            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, configurationName);
            this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
            this.iDocument.CurrentRowIndex += 2;
        }

        private void WriteVariableData()
        {
            this.iDocument.NextSheet();
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, "Переменные данные для исполнений");
            this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
        }

        public ConfigurationWriterPerechen(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.Initialization();
        }
    }
}
