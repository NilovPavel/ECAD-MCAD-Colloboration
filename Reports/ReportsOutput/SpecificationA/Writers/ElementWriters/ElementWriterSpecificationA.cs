using System;
using System.Linq;

namespace ReportsOutput
{
    class ElementWriterSpecificationA : IElementWriter
    {
        protected IDocument iDocument;
        private IDocumentCellStyle iDocumentCellStyle;
        private IDocumentFont iDocumentFont;

        private string format;
        private string position;
        private string oboznchenie;
        private string[] naimenovanie;
        private string count;
        private string[] primechanie;

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

        public ElementWriterSpecificationA(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.SetStyleSettings();
        }

        private void GetRecordCorrectValues(RecordESKD recordESKD, Spisok spisok)
        {
            this.format = this.GetFormat(recordESKD);
            this.position = this.GetPositon(recordESKD);
            this.oboznchenie = this.GetOboznachenie(recordESKD, spisok);
            this.naimenovanie = this.GetNaimenovanieArray(recordESKD, spisok);
            this.count = recordESKD.Количество.ToString();
            this.primechanie = this.GetPrimechanieArray(recordESKD);
        }

        private string GetFormat(RecordESKD recordESKD)
        {
            return (recordESKD.Формат.IndexOf(",") != -1) ? "*)" : recordESKD.Формат;
        }

        protected virtual string GetPositon(RecordESKD recordESKD)
        {
            return recordESKD.Fitted ? recordESKD.Позиция.ToString() : "-";
        }

        protected virtual string GetOboznachenie(RecordESKD recordESKD, Spisok spisok)
        {
            return recordESKD.Обозначение;
        }

        protected virtual string[] GetNaimenovanieArray(RecordESKD recordESKD, Spisok spisok)
        {
            string naimenovanieString = recordESKD.Fitted ? recordESKD.Наименование : "Не устанавливать";
            return new Razbivka(naimenovanieString, this.iDocument.ITemplateDocument.NameLength).StringAllowLengthArray;
        }

        private string GetPrimechanieFullValue(RecordESKD recordESKD)
        {
            string fullPrimechanieString = string.Empty;
            if (recordESKD.Формат.IndexOf(",") != -1)
                fullPrimechanieString = "*)" + recordESKD.Формат;

            if (!string.IsNullOrEmpty(recordESKD.ЕдИзм))
                fullPrimechanieString = string.IsNullOrEmpty(fullPrimechanieString) ? recordESKD.ЕдИзм : "; " + recordESKD.ЕдИзм;

            if (!string.IsNullOrEmpty(recordESKD.Designator))
                fullPrimechanieString = string.IsNullOrEmpty(fullPrimechanieString) ? recordESKD.Designator : "; " + recordESKD.Designator;

            if (!string.IsNullOrEmpty(recordESKD.Примечание))
                fullPrimechanieString = string.IsNullOrEmpty(fullPrimechanieString) ? recordESKD.Designator : "; " + recordESKD.Примечание;

            return fullPrimechanieString;
        }

        private string[] GetPrimechanieArray(RecordESKD recordESKD)
        {
            string primechanieValue = this.GetPrimechanieFullValue(recordESKD);
            string[] primechanieArray = new Razbivka(primechanieValue, ((TemplateSpecificationAExcel)this.iDocument.ITemplateDocument).PrimechanieLength).StringAllowLengthArray;
            return primechanieArray;
        }

        void IElementWriter.Write(RecordESKD recordESKD, Spisok spisok)
        {
            this.GetRecordCorrectValues(recordESKD, spisok);

            if (this.iDocument.CurrentRowIndex + this.naimenovanie.Length >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();

            this.WriteFormat();
            this.WritePosition();
            this.WriteObozn();
            this.SetCellSettings(recordESKD);
            this.WriteNameValue();
            this.WriteCount();
            this.WritePrimechanie();
        }

        private void SetCellSettings(RecordESKD recordESKD)
        {
            byte[] color = new byte[] { 255, 255, 255 };
            switch (recordESKD.Status)
            {
                /*case null:
                case "Разрешен к применению":
                case "":
                    color = 0x000000;
                    break;*/
                case "Для пробного применения":
                    color = new byte[] { 0, 255, 87 };
                    break;
                case "Не использовать в новых разработках":
                    color = new byte[] { 0, 255, 251 };
                    break;
                case "Особые условия закупа":
                    color = new byte[] { 255, 129, 255 };
                    break;
                case "В разработке":
                    color = new byte[] { 0, 255, 0 };
                    break;
                case "Запрещен к применению":
                    color = new byte[] { 255, 79, 102 };
                    break;
                case "Разрешен без ограничений":
                    color = new byte[] { 255, 255, 255 };
                    break;
            }
            this.iDocumentCellStyle.Color = color;
            this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
        }

        private void WritePrimechanie()
        {
            for (int cellCount = 0; cellCount < this.primechanie.Length; cellCount++)
            {
                if (this.iDocument.CurrentRowIndex + cellCount >= this.iDocument.CurrentSheet.LastRowIndex)
                {
                    this.iDocument.NextSheet();
                    this.WriteNameValue();
                    string[] ostatok = new string[this.primechanie.Length - cellCount];
                    Array.Copy(this.primechanie, cellCount, ostatok, 0, ostatok.Length);
                    this.primechanie = ostatok;
                    this.WritePrimechanie();
                    return;
                }
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.PrimechCellColumn, this.iDocument.CurrentRowIndex + cellCount, this.primechanie[cellCount]);
            }
            this.iDocument.CurrentRowIndex += (this.naimenovanie.Length >= this.primechanie.Length) ? this.naimenovanie.Length : this.primechanie.Length;
        }

        private void WriteFormat()
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.FormatCellColumn, this.iDocument.CurrentRowIndex, this.format);
        }

        private void WritePosition()
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.PositionCellColumn, this.iDocument.CurrentRowIndex, this.position);
        }

        private void WriteObozn()
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.OboznCellColumn, this.iDocument.CurrentRowIndex, this.oboznchenie);
        }

        private void WriteNameValue()
        {
            for (int cellCount = 0; cellCount < this.naimenovanie.Length; cellCount++)
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex + cellCount, this.naimenovanie[cellCount]);
        }

        private void WriteCount()
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.CountCellColumns.First(), this.iDocument.CurrentRowIndex, this.count);
        }
    }
}