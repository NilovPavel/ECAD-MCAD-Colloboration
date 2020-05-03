using System;
using NPOI.SS.UserModel;
using ReportsOutput.IExcelReports;
using NPOI.HSSF.Extractor;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;

namespace ReportsOutput
{
    public class SpecificationADocumentXLS : AbstractDocumentXLS
    {
        private string[] documentProperties;
        public SpecificationADocumentXLS(ProjectProperties projectProperties) : base(projectProperties)
        {
            this.documentProperties = new string[]
                {
                    "Обозначение",
                    "Наименование",
                    "Проект", 
                    "Перв.Примен.", 
                    "п_Разраб", 
                    "п_Пров", 
                    "п_Н_контр", 
                    "п_Доп_графа",
                    "п_Утв"
                };
        }

        public override ITemplateDocument ITemplateDocument
        {
            get
            {
                return new TemplateSpecificationAExcel();
            }
        }

        public override void WriteDocumentProperties()
        {
            ExcelExtractor excelExtractor = new ExcelExtractor((HSSFWorkbook)this.workbook);
            DocumentSummaryInformation documentSummaryInformation = excelExtractor.DocSummaryInformation;
            CustomProperties customProperties = new CustomProperties();
            foreach (string onePropertie in this.documentProperties)
                customProperties.Put(onePropertie, this.projectProperties.GetPropertyByName(onePropertie) ?? string.Empty);
            customProperties.Put("Вид_документа", "Спецификация");
            customProperties.Put("Код_документа", "");
            customProperties.Put("Раздел", "Документация");
            customProperties.Put("Тип_документа", "Рабочая документация");
            documentSummaryInformation.CustomProperties = customProperties;
        }
    }
}