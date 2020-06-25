using System;
using ReportsOutput;

namespace ReportsOutput
{
    internal class TemplatePerechenExcel : ITemplateDocument
    {
        IDocumentSheet ITemplateDocument.IDocumentFirstSheet
        {
            get
            {
                return new FirstSheetPerechenXLS();
            }
        }

        IDocumentSheet ITemplateDocument.IDocumentLRISheet
        {
            get
            {
                return new LRISheetPerechenXLS();
            }
        }

        IDocumentSheet ITemplateDocument.IDocumentSecondSheet
        {
            get
            {
                return new SecondSheetPerechenXLS();
            }
        }

        int ITemplateDocument.NameLength
        {
            get
            {
                return 42;
            }
        }

        string ITemplateDocument.GetFileName()
        {
            return "PerechenTemplate.xls";
        }

        string ITemplateDocument.GetResourceName()
        {
            return "PerechenTemplate";
        }
    }
}