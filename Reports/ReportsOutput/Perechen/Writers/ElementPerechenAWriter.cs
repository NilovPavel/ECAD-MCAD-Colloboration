using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOutput.Perechen.Writers
{
    class ElementPerechenAWriter : IElementWriter
    {
        private IDocument iDocument;
        private IDocumentCellStyle iDocumentCellStyle;
        private IDocumentFont iDocumentFont;

        void IElementWriter.Write(RecordESKD recordESKD, Spisok spisok)
        {
            //Костыль
            if (string.IsNullOrEmpty(recordESKD.Designator.Trim()))
                return;

            if (this.iDocument.CurrentRowIndex >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();
            
            this.WritePosition(recordESKD);
            this.WriteCount(recordESKD);
            this.WriteName(recordESKD);

            
        }

        private void WriteName(RecordESKD recordESKD)
        {
            string elementName = recordESKD.Fitted ? recordESKD.Наименование : "Не устанавливать";
            Razbivka razbivkaElement = new Razbivka(elementName, this.iDocument.ITemplateDocument.NameLength);
            foreach (string elementPartName in razbivkaElement.StringAllowLengthArray)
            {
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, elementPartName);
                this.iDocument.CurrentRowIndex++;
            }
        }

        private void WritePosition(RecordESKD recordESKD)
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.PositionCellColumn, this.iDocument.CurrentRowIndex, recordESKD.Designator);
        }

        private void WriteCount(RecordESKD recordESKD)
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.CountCellColumns[0], this.iDocument.CurrentRowIndex, recordESKD.Количество.ToString());
        }

        public ElementPerechenAWriter(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.SetStyleSettings();
        }

        private void SetStyleSettings()
        {
            this.iDocumentCellStyle = this.iDocument.CellStyle;
            this.iDocumentFont = this.iDocumentCellStyle.IDocumentFont;

            this.iDocumentFont.Bold = false;
            this.iDocumentFont.Italic = true;
            this.iDocumentFont.UnderLine = false;

            this.iDocumentCellStyle.SetFont(this.iDocumentFont);

            this.iDocumentCellStyle.HorizlontalCenter = false;
            this.iDocumentCellStyle.VerticalCenter = true;
        }


    }
}
