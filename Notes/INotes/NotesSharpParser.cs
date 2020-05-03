using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    public class NotesSharpParser : IParser
    {
        private INotesRazdel iNotesRazdel;

        public NotesSharpParser(INotesRazdel iNotesRazdel)
        {
            this.iNotesRazdel = iNotesRazdel;
        }

        public double GetCount(string unparseString, int variantCount)
        {
            string[] splitString = unparseString.Split('#');
            string stringValue = (this.iNotesRazdel.GetColumnNames().Length + variantCount < splitString.Length) ? splitString[this.iNotesRazdel.GetColumnNames().Length + variantCount] : this.iNotesRazdel.GetDefaultValue();
            double doubleValue;
            double.TryParse(stringValue, out doubleValue);
            return doubleValue;
        }

        public string GetParameterValue(string unparseString, string parameterName)
        {
            string[] splitString = unparseString.Split('#');
            int parameterArrayIndex = Array.IndexOf(this.iNotesRazdel.GetColumnNames(), parameterName);
            string parameterValue = parameterArrayIndex == -1 ? string.Empty : splitString[parameterArrayIndex];
            return parameterValue; ; 
        }
    }
}
