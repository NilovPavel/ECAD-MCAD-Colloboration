using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReportsOutput
{
    class StandartElementWriterSpecificationA : ElementWriterSpecificationA
    {
        private string[] standartNames;
        public StandartElementWriterSpecificationA(IDocument iDocument) : base(iDocument)
        {
            this.Initialization();
        }

        private void Initialization()
        {
            this.standartNames = Enum.GetNames(typeof(StandartNames));
        }

        private bool IsRight(string currentString, string oneString, string[] splitStrings)
        {
            bool isStandartName = Array.Exists(this.standartNames, item => oneString.IndexOf(item) != -1);
            int indexStandart = Array.IndexOf(splitStrings, oneString);
            if (isStandartName && indexStandart + 1 < splitStrings.Length)
                return currentString.Length + oneString.Length + splitStrings[indexStandart+1].Length + 2 >= this.iDocument.ITemplateDocument.NameLength;
            return currentString.Length + oneString.Length >= this.iDocument.ITemplateDocument.NameLength;
        }

        public string[] GetElementSplitArray(string elementName)
        {
            List<string> stringAllowLength = new List<string>();
            string[] splitStrings = Regex.Split(elementName, "\\s+");
            string currentString = string.Empty;
            foreach (string oneString in splitStrings)
            {
                if (this.IsRight(currentString, oneString, splitStrings))
                {
                    stringAllowLength.Add(currentString.Trim());
                    currentString = oneString;
                }
                else currentString += " " + oneString;
            }
            stringAllowLength.Add(currentString.Trim());
            return stringAllowLength.ToArray();
        }

        protected override string[] GetNaimenovanieArray(RecordESKD recordESKD, Spisok spisok)
        {
            if (!recordESKD.Fitted)
                return new string[] { "Не устанавливать" };

            if (spisok.Elements.Length > 1)
                return new string[] { new StandartElement(recordESKD.Наименование).SmallName };

            return this.GetElementSplitArray(recordESKD.Наименование);
        }
    }
}