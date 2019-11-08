using ReportsOutput.ExcelConstants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportsOutput
{
    public class ConfigurationWriterSpecificationA : IConfigurationWriter
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

        public ConfigurationWriterSpecificationA(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.Initialization();
        }

        void IConfigurationWriter.Write(ProjectProperties projectProperties, Spisok configuration, List<Spisok> configurations)
        {
            if (configurations.Count > 1 && configuration.Equals(configurations[1]))
                this.WriteVariableData();

            if (!(configuration.Equals(configurations.First()) || configurations.Count == 1))
                this.WriteVariableConfigurationName(projectProperties, configuration);

            if (configuration.Razdels.Count == 0)
            {
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, "Отсутствуют");
                this.WriteChangesOnlyByDrawing(configuration, configurations);
            }
        }

        private void WriteChangesOnlyByDrawing(Spisok configuration, List<Spisok> configurations)
        {
            Spisok firstConfiguration = configurations.First();
            Spisok lastConfiguration = configurations.Last();

            int countConfigurationWithoutChanges = configurations.Where(item => !item.Equals(firstConfiguration)).Count(item => item.Razdels.Count == 0);

            if (configuration.Equals(lastConfiguration))
            {
                this.iDocument.CurrentRowIndex += 2;

                if (this.iDocument.CurrentRowIndex > this.iDocument.CurrentSheet.LastRowIndex)
                    this.iDocument.NextSheet();

                this.iDocument.MergedRegion(this.iDocument.CurrentRowIndex, this.iDocument.CurrentRowIndex, this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentSheet.CountCellColumns.First() - 1);
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, "Различия исполнений см. по сборочному чертежу");
                this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
            }
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
            this.iDocument.MergedRegion(this.iDocument.CurrentRowIndex, this.iDocument.CurrentRowIndex, this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentSheet.CountCellColumns.First() - 1);
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, "Переменные данные для исполнений");
            this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
        }
    }
}