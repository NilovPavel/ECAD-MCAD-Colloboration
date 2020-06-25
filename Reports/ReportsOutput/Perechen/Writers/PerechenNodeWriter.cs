using ReportsOutput.ExcelConstants;
using ReportsOutput.Perechen.Writers;
using System;

namespace ReportsOutput
{
    public class PerechenNodeWriter
    {
        private IDocument iDocument;
        private IDocumentCellStyle iDocumentCellStyle;
        private IDocumentFont iDocumentFont;
        private IElementWriter iElementWriter;
        private IteratorSpisok iteratorSpisok;
        private IteratorStringCount iIteratorAction;

        private int GetCountBeforeElement(Spisok razdel)
        {
            this.iteratorSpisok = new IteratorSpisok(razdel);
            this.iteratorSpisok.SetIteratorAction(this.iIteratorAction);
            this.iteratorSpisok.Iteration();
            return this.iIteratorAction.SpisokCountSpace;
        }

        private void WriteRazdelName(Spisok razdel)
        {
            if (razdel is PerechenTreeNode 
                || string.IsNullOrEmpty(razdel.Name.Trim()) //Костыль
                )
                return;

            if (this.iDocument.CurrentRowIndex + this.GetCountBeforeElement(razdel) >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();

            this.iDocument.CurrentRowIndex++;
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, razdel.Name);
            this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
            this.iDocument.CurrentRowIndex+=2;
        }

        public PerechenNodeWriter(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.Initialization();
            this.SetStyleSettings();
        }

        private void SetStyleSettings()
        {
            this.iDocumentCellStyle = this.iDocument.CellStyle;
            this.iDocumentFont = this.iDocumentCellStyle.IDocumentFont;

            this.iDocumentFont.Bold = true;
            this.iDocumentFont.Italic = true;
            this.iDocumentFont.UnderLine = false;

            this.iDocumentCellStyle.SetFont(this.iDocumentFont);

            this.iDocumentCellStyle.HorizlontalCenter = true;
            this.iDocumentCellStyle.VerticalCenter = true;
        }

        private void Initialization()
        {
            this.iElementWriter = new ElementPerechenAWriter(this.iDocument);
            this.iIteratorAction = new IteratorStringCount(this.iDocument);
        }

        public void WriteNodes(Spisok razdel)
        {
            if (this.iDocument.CurrentRowIndex >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();

            this.WriteRazdelName(razdel);

            Array.ForEach(razdel.Elements, recordESKD => this.iElementWriter.Write(recordESKD, razdel));
            
            razdel.Razdels.ForEach(item => this.WriteNodes(item));
        }
    }
}