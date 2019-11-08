using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Razbivka
{
    private string myString;
    private int allowSymbolCount;

    public string[] StringAllowLengthArray
    { get; private set; }

    private string[] GetStringsArray()
    {
        List<string> stringAllowLength = new List<string>();
        string[] splitString = Regex.Split(this.myString, "\\s+");
        string currentString = string.Empty;
        foreach (string oneString in splitString)
        {
            if (currentString.Length + oneString.Length >= allowSymbolCount)
            {
                stringAllowLength.Add(currentString.Trim());
                currentString = oneString;
            }
            else currentString += " " + oneString;
        }
        stringAllowLength.Add(currentString.Trim());
        return stringAllowLength.ToArray();
    }

    public Razbivka(string myString, int allowSymbolCount)
    {
        this.myString = myString;
        this.allowSymbolCount = allowSymbolCount;
        this.StringAllowLengthArray = string.IsNullOrEmpty(myString) ? new string[1] { string.Empty } : this.GetStringsArray().Where(item => !string.IsNullOrEmpty(item)).ToArray();
    }
}