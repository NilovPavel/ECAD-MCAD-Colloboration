// File:    Designator.cs
// Author:  nilov_pg
// Created: 8 августа 2019 г. 11:25:22
// Purpose: Definition of Class Designator

using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Designator
{
    public static int GetIndexFromDesignator(string designator)
    {
        Regex regEx = new Regex("[^\\d+]");
        designator = regEx.Replace(designator, string.Empty);
        if (string.IsNullOrEmpty(designator))
            return 0;
        return int.Parse(designator);
    }

    public static string GetBaseFromDesignators(string[] designators)
    {
        string designator = string.Empty;
        string firstDesignator = designators.FirstOrDefault();
        Regex regEx = new Regex("\\d+");
        designator = regEx.Replace(firstDesignator, string.Empty);
        return designator;
    }

    public static string GetSymbolsFromDesignator(string designator)
   {
        Regex regEx = new Regex("\\w+");
        Match match = regEx.Match(designator);
        string firstDesignator = match.Success ? match.Value : designator;

        regEx = new Regex("[^]^[\\d]+");
        match = regEx.Match(firstDesignator);

        string symbols = match.Success ? match.Value : designator;
        return symbols;
   }

}