using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportsOutput
{
    public class SpecificationWriterSpecificationA : ISpecificationWriter
    {
        protected IDocument iDocument;
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

        public SpecificationWriterSpecificationA(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.Initialization();
        }

        private string GetSpecificationHeader(ProjectProperties projectProperties, Spisok specification)
        {
            string header = specification.Name;
            switch (specification.Name)
            {
                case "Спецификация МЭ":
                    header = "Устанавливают по " + projectProperties.GetPropertyByName("Обозначение") + "МЭ";
                    break;
            }
            return header;
        }

        void ISpecificationWriter.Write(ProjectProperties projectProperties, Spisok specification, List<Spisok> specifications)
        {
            if (!specification.Equals(specifications.First()))
                this.iDocument.NextSheet();

            if (!(specification.Equals(specifications.First()) || specifications.Count == 1))
            {
                string header = this.GetSpecificationHeader(projectProperties, specification);
                this.iDocument.MergedRegion(this.iDocument.CurrentRowIndex, this.iDocument.CurrentRowIndex, this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentSheet.CountCellColumns.First() - 1);
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, header);
                this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
                this.iDocument.CurrentRowIndex += 2;
            }
        }
    }
}