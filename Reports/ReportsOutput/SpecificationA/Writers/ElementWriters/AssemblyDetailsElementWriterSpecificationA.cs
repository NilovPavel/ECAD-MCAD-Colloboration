using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReportsOutput
{
    internal class AssemblyDetailsElementWriterSpecificationA : ElementWriterSpecificationA
    {
        private string GetRootRecordObozn(string value)
        {
            Regex regex = new Regex("\\D{4}.\\d{6}.\\d{3}");
            Match match = regex.Match(value);
            string oboznString = regex.IsMatch(value) ? match.Value : value;
            return oboznString;
        }

        private string GetCorrectObozn(RecordESKD recordESKD, Spisok spisok)
        {
            string thisObozn = recordESKD.Обозначение;
            string rootObozn = this.GetRootRecordObozn(thisObozn);
            IEnumerable<string> allIdemObozn = spisok.Elements.Where(item => item.Обозначение.IndexOf(rootObozn)!=-1).Select(item => item.Обозначение);
            string firstObozn = allIdemObozn.FirstOrDefault() ?? string.Empty;
            if (allIdemObozn.Count() > 1 && !thisObozn.Equals(firstObozn) && !string.IsNullOrEmpty(rootObozn))
                return thisObozn.Replace(rootObozn, string.Empty);
            return rootObozn;
        }

        protected override string GetOboznachenie(RecordESKD recordESKD, Spisok spisok)
        {
            return this.GetCorrectObozn(recordESKD, spisok);
        }

        public AssemblyDetailsElementWriterSpecificationA(IDocument iDocument) : base(iDocument)
        {
        }
    }
}