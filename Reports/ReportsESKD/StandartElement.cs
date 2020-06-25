// File:    StandartElement.cs
// Author:  nilov_pg
// Created: 8 июля 2019 г. 10:29:19
// Purpose: Definition of Class StandartElement

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public class StandartElement
{
    private string fullName;
    private string name;
    private string nameStandart;
    private int numberStandart;
    private string fullStandartName;
    private int jearStandart;
    private double diametr;
    private double length;
    private string nameInGroup;
    private string[] splitStrings;
    private string generalParameters;

    private void Initialization()
    {
    }

    private void CalcParameters()
    {
        this.name = this.GetName();

        this.nameStandart = this.GetNameStandart();

        string numberStandartWithJear = this.GetNumberWithJearStandartString();
        string numberStandartString = this.GetNumberStandartString(numberStandartWithJear);
        this.numberStandart = this.GetIntValue(numberStandartString);

        string jearString = this.GetJearString(numberStandartWithJear);
        this.jearStandart = this.GetIntValue(jearString);

        this.fullStandartName = (this.nameStandart + " " + this.numberStandart + (this.jearStandart!=0 ? "-" + this.jearStandart : string.Empty));
        //if (this.fullStandartName.Equals("ГОСТ Р ИСО 4762")) //"ГОСТ 8752-79" "ГОСТ 22376-77"|| this.fullStandartName.Equals("ГОСТ 18678-73")) //|| this.fullStandartName.Equals("ОСТ 4"));

        this.nameInGroup = this.GetNameInGroup(numberStandartWithJear);

        this.generalParameters = this.GetGeneralParameters();

        string diameterString = this.GetDiameterString();
        this.diametr = this.GetDoubleValue(diameterString);

        string lengthString = this.GetLengthString();
        this.length = this.GetDoubleValue(lengthString);
        //
    }
    
    private bool IsDeniedWordInName(string word)
    {
        string[] standartNames = Enum.GetNames(typeof(StandartNames));
        foreach (string standartName in standartNames)
            if (word.IndexOf(standartName) != -1)
                return true;
        Regex numAndSymbols = new Regex("\\w+");
        Regex numDenied = new Regex("\\d+");
        return !(numAndSymbols.IsMatch(word) && !numDenied.IsMatch(word));
    }

    private string GetName()
    {
        Regex regex = new Regex("\\s+");
        this.splitStrings = regex.Split(this.fullName);
        string nameString = string.Empty;
        foreach (string word in this.splitStrings)
            if (!this.IsDeniedWordInName(word.Trim()))
                nameString += word + " ";
            else break;
        return nameString.Trim();
    }

    private string GetNameStandart()
    {
        string[] standartNames = Enum.GetNames(typeof(StandartNames));
        string[] interSectStandartNames = standartNames.Intersect(this.splitStrings).ToArray();
        string nameStandartStart = interSectStandartNames.Length > 0 ? interSectStandartNames.First() : string.Empty;

        int firstEquality = this.fullName.IndexOf(nameStandartStart);
        Regex regex = new Regex("[\\d]+");
        Match match = regex.Match(this.fullName.Substring(firstEquality));

        string nameStandart = match.Success ? this.fullName.Substring(firstEquality, match.Index) : nameStandartStart;
        return nameStandart.Trim();
    }

    private string GetNumberWithJearStandartString()
    {
        int endIndexStandartName = this.fullName.IndexOf(this.nameStandart) + this.nameStandart.Length;
        string afterStandartName = this.fullName.Substring(endIndexStandartName);

        Regex regex = new Regex("[\\d]+[-]*([\\d]+)*");
        Match match = regex.Match(afterStandartName);

        string numberInStandartString = match.Success ? match.Value : string.Empty;
        return numberInStandartString;
    }

    private string GetNumberStandartString(string numberStandartWithJear)
    {
        Regex regex = new Regex("[\\d]+");
        Match match = regex.Match(numberStandartWithJear);

        string numberInStandartString = match.Success ? match.Value : string.Empty;
        return numberInStandartString;
    }

    private string GetJearString(string numberStandartWithJear)
    {
        Regex regex = new Regex("-\\d+$");
        Match match = regex.Match(numberStandartWithJear);

        string numberInStandartString = match.Success ? match.Value.Replace("-", string.Empty) : string.Empty;
        return numberInStandartString;
    }

    private string GetGeneralParameters()
    {
        Regex regex = new Regex("[M|М|A|А]*[\\d]+[,\\d]*[\\s]*(x|х)*([\\s]*[\\d]+[,\\d]*)*");
        Match match = regex.Match(this.nameInGroup);

        string generalParameters = match.Success ? match.Value : string.Empty;
        return generalParameters;
    }


    private string GetDiameterString()
    {
        Regex regex = new Regex("[M|М|A|А]*[\\d]+[,\\d]*");
        Match match = regex.Match(this.generalParameters);

        string diameterStringWithX = match.Success ? match.Value : string.Empty;
        regex = new Regex("[\\d]+[,\\d]*");
        match = regex.Match(diameterStringWithX);

        string diameterStringWithoutX = match.Success ? match.Value.Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) : string.Empty;

        return diameterStringWithoutX;
    }

    private string GetLengthString()
    {
        Regex regex = new Regex("(x|х)*[\\d]+[,\\d]*$");
        Match match = regex.Match(this.generalParameters);

        string lengthString = match.Success ? match.Value : string.Empty;
        regex = new Regex("[\\d]+[,\\d]*$");
        match = regex.Match(lengthString);

        lengthString = match.Success ? match.Value.Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) : string.Empty;

        return lengthString;
    }

    private string GetNameInGroup(string numberStandartWithJear)
    {
        string paramsAndOthers = this.fullName.Replace(this.fullStandartName, string.Empty).Replace(this.name, string.Empty);

        Regex regex = new Regex("[\\w]");
        Match match = regex.Match(paramsAndOthers);

        paramsAndOthers = match.Success ? paramsAndOthers.Substring(match.Index) : string.Empty;

        return paramsAndOthers;
    }

    private int GetIntValue(string intString)
    {
        int intValue;
        int.TryParse(intString, out intValue);
        return intValue;
    }

    private double GetDoubleValue(string doubleString)
    {
        double doubleValue;
        double.TryParse(doubleString, out doubleValue);
        return doubleValue;
    }

    public string FullName
    {
        get
        {
            return fullName;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public string NameStandart
    {
        get
        {
            return nameStandart;
        }
    }

    public int NumberStandart
    {
        get
        {
            return numberStandart;
        }
    }

    public int JearStandart
    {
        get
        {
            return jearStandart;
        }
    }

    public double Diametr
    {
        get
        {
            return diametr;
        }
    }

    public string FullStandartName
    {
        get
        {
            return fullStandartName;
        }
    }

    public double Length
    {
        get
        {
            return length;
        }
    }

    public string SmallName
    {
        get
        {
            return nameInGroup;
        }
    }

    public string GeneralParameters
    {
        get
        {
            return this.generalParameters;
        }
    }

    public StandartElement(string fullName)
    {
        this.fullName = fullName;
        this.Initialization();
        this.CalcParameters();
    }

}