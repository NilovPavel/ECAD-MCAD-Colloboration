// File:    SpecificationRazdelStandartGroup.cs
// Author:  nilov_pg
// Created: 7 августа 2019 г. 12:22:54
// Purpose: Definition of Class SpecificationRazdelStandartGroup

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class SpecificationRazdelStandartGroup : Spisok
{
   private StandartElement standartElement;
   private Dictionary<string, string> pristavki;
   
   private void Initialization()
   {
      pristavki = new Dictionary<string, string>(){
               { "ая", "ые" },
               { "ба", "бы" },
               { "ва", "вы" },
               { "дь", "ди" },
               { "ик", "ики" },                            
               { "ка", "ки" },                                        
               { "ко", "ки" },                                        
               { "ок", "ки" },
               { "н", "ны" },
               { "на", "ны" },
               { "ое", "ые" },
               { "п", "пы" },
               { "са", "сы" },
               { "т", "ты"},
               { "ток", "тки" },                                        
               { "цо", "ца" },                                        
               { "ч", "чи" },                                        
               { "ый", "ые" }                                        
               };
      this.iSort = new StandartSort();
      this.standartElement = new StandartElement(this.Name);
   }
   
   private string GetPlural(string name)
   {
      string[] pristavki = this.pristavki.Keys.ToArray();
      foreach (string pristavka in pristavki)
      {
         Regex regex = new Regex(pristavka + "$");
         Match match = regex.Match(name);
         string machValue;
   
         if (match.Success && this.pristavki.TryGetValue(match.Value, out machValue))
         {
            name = regex.Replace(name, machValue);
            break;
         }
      }
      return name;
   }
   
   private string GetGroupName()
   {
      StandartElement standartElement = new StandartElement(this.Name);
      string name = this.GetName(standartElement.Name) + " " + 
      standartElement.NameStandart + " " + 
         (standartElement.NumberStandart != 0 ? standartElement.NumberStandart.ToString() : string.Empty) + 
         (standartElement.JearStandart != 0 ? "-" + standartElement.JearStandart.ToString() : string.Empty );
      return name.Trim();
   }
   
   private string GetName(string name)
   {
      Regex regex = new Regex("\\s+");
      string[] splitName = regex.Split(name);
      string newName = string.Empty;
      foreach (string namePart in splitName)
         newName += this.GetPlural(namePart) + " ";
      return newName.Trim();
   }
   
   public StandartElement StandartElement
   {
      get
      {
         return standartElement;
      }
   }
   
   public SpecificationRazdelStandartGroup(string name, RecordESKD[] elements)
   : base(name, elements)
   {
      this.Initialization();
      this.Name = this.GetGroupName();
      this.Elements = this.iSort.Sort(this.Elements);
   
      this.iSort = new UpDownSort();
      this.Elements = this.iSort.Sort(this.Elements);
   }

}