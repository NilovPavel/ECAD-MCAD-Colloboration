using ReportsOutput;
using ReportsOutput.IExcelReports;
using System;
using System.IO;

class TemplateSpecificationAExcel : ITemplateDocument
{
    public int PrimechanieLength { get { return 11; } }

    IDocumentSheet ITemplateDocument.IDocumentFirstSheet
    {
        get
        {
            return new FirstSheetSpecificationAXLS();
        }
    }

    IDocumentSheet ITemplateDocument.IDocumentSecondSheet
    {
        get
        {
            return new SecondSheetSpecificationAXLS();
        }
    }

    IDocumentSheet ITemplateDocument.IDocumentLRISheet
    {
        get
        {
            return new LRISheetSpecificationAXLS();
        }
    }

    int ITemplateDocument.NameLength
    {
        get
        {
            return 26;
        }
    }

    string ITemplateDocument.GetFileName()
    {
        return "SpecificationTemplate.xls";
    }

    string ITemplateDocument.GetResourceName()
    {
        return "SpecificationTemplate";
    }
}