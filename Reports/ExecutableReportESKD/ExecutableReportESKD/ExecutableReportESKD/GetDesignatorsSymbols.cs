using System;
using System.Text.RegularExpressions;

namespace ExecutableReportESKD
{
    internal class GetDesignatorsSymbols
    {
        private string[] designators = new string[] { "DA1, DA2", "DD1-DD3", "C6-C8", "C13, C40", "ZL1-ZL3" };

        private string GetSymbolsFromDesignator(string designator)
        {
            Regex regEx = new Regex("\\w+");
            Match match = regEx.Match(designator);
            string firstDesignator = match.Success ? match.Value : designator;
            regEx = new Regex("[^]^[\\d]+");

            match = regEx.Match(firstDesignator);
            string symbols = match.Success ? match.Value : designator;
            return symbols;
        }

        public GetDesignatorsSymbols()
        {
            foreach (string designator in this.designators)
                Console.WriteLine(designator + "\t=" + this.GetSymbolsFromDesignator(designator));
        }
    }
}