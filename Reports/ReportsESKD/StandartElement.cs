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
   private int jearStandart;
   private double diametr;
   private double length;
   private string smallName;
   private string[] splitStrings;
   
   private void Initialization()
   {
      Regex regex = new Regex("\\s+");
      this.splitStrings = regex.Split(this.fullName);
   }

    private void CalcParameters()
   {
      this.name = this.GetName();
      this.nameStandart = this.GetNameStandart();
      string numberStandartString = this.GetNumberStandartString();
      this.numberStandart = this.GetIntValue(numberStandartString);
      string jearString = this.GetJearString();
      this.jearStandart = this.GetIntValue(jearString);
      string diameterString = this.GetDiameterString();
      this.diametr = this.GetDoubleValue(diameterString);
      string lengthString = this.GetLengthString();
      this.length = this.GetDoubleValue(lengthString);
      this.smallName = this.GetSmallName();
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
      string nameStandart = interSectStandartNames.Length > 0 ? interSectStandartNames.First() : string.Empty;
      return nameStandart;
   }
   
   private string GetNumberStandartString()
   {
      int standartNameInArray = Array.IndexOf(this.splitStrings, this.nameStandart);
      string numberInString = string.Empty;
   
      if (standartNameInArray++ < this.splitStrings.Length)
         numberInString = this.splitStrings[standartNameInArray];
   
      Regex regex = new Regex("[\\d]+");
      Match match = regex.Match(numberInString);
      string numberInStandartString = match.Success ? match.Value : string.Empty;
   
      return numberInStandartString;
   }
   
   private string GetJearString()
   {
      int standartNameInArray = Array.IndexOf(this.splitStrings, this.nameStandart);
      string numberInString = string.Empty;
   
      if (standartNameInArray++ < this.splitStrings.Length)
         numberInString = this.splitStrings[standartNameInArray];
   
      Regex regex = new Regex("-\\d+$");
      Match match = regex.Match(numberInString);
   
      string numberInStandartString = match.Success ? match.Value.Replace("-", string.Empty) : string.Empty;
      return numberInStandartString;
   }
   
   private string GetDiameterString()
   {
      Regex regex = new Regex("[\\d]+[,\\d]*[хx]*[\\s]*");
      Match match = regex.Match(this.fullName);
   
      string diameterStringWithX = match.Success ? match.Value : string.Empty;
      string diameterStringWithoutX = diameterStringWithX.Replace("x", string.Empty).Replace("х", string.Empty).Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
      return diameterStringWithoutX;
   }
   
   private string GetLengthString()
   {
      Regex regex = new Regex("[хx][\\s]*[\\d]+[,\\d]*");
      Match match = regex.Match(this.fullName);
      string lengthString = match.Success ? match.Value : string.Empty;
      lengthString = string.IsNullOrEmpty(lengthString) ? string.Empty :
                                                               lengthString.Replace("x", string.Empty)
                                                               .Replace("х", string.Empty)
                                                               .Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
      return lengthString;
   }
   
   private string GetSmallName()
   {
      string resultString = string.Empty;
      Regex regex = new Regex("\\s+");
      int lastNameIndex = Array.IndexOf(this.splitStrings, regex.Split(this.name).Last());
      int standartIndex = Array.IndexOf(this.splitStrings, this.nameStandart);
      for (int splitIndex = lastNameIndex + 1; splitIndex < standartIndex; splitIndex++)
         resultString += this.splitStrings[splitIndex] + " ";
      return resultString.Trim();
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
         return smallName;
      }
   }
   
   public StandartElement(string fullName)
   {
      this.fullName = fullName;
      this.Initialization();
      this.CalcParameters();
   }

}