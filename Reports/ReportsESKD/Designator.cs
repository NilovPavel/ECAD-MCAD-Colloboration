// File:    Designator.cs
// Author:  nilov_pg
// Created: 8 августа 2019 г. 11:25:22
// Purpose: Definition of Class Designator

using System;
using System.Text.RegularExpressions;

public class Designator
{
   public static string GetSymbolsFromDesignator(string designator)
   {
      Regex regEx = new Regex("[^][\\d+]");
      Match match = regEx.Match(designator);
      designator = match.Success ? match.Value : designator;
      return designator;
   }

}