using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsOutput
{
    public interface ITemplateDocument
    {
        int NameLength { get; }
        string GetResourceName();
        string GetFileName();
        IDocumentSheet IDocumentFirstSheet { get; }
        IDocumentSheet IDocumentSecondSheet { get; }
        IDocumentSheet IDocumentLRISheet { get; }
    }
}