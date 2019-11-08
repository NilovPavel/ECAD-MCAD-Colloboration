// File:    SpecificationRazdelPerechen.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:09:12
// Purpose: Definition of Class SpecificationRazdelPerechen

using System;
using System.Linq;

public class SpecificationRazdelPerechen : Spisok
{
   private void Initialization()
   {
      this.iSort = this.OtherElementsConcretSort();
   }
   
   private ISort OtherElementsConcretSort()
   {
      ISort currentSortOtherElements;
      switch (this.Name)
      {
         case "Конденсаторы":
            currentSortOtherElements = new OtherElementPassiveERISort("Ф");
            break;
         case "Резисторы":
            currentSortOtherElements = new OtherElementPassiveERISort("Ом");
            break;
         case "Катушки индуктивности":
            currentSortOtherElements = new OtherElementPassiveERISort("Гн");
            break;
         default:
            currentSortOtherElements = new OtherElementComputerSort();
            break;
      }
      return currentSortOtherElements;
   }
   
   public SpecificationRazdelPerechen(string name, RecordESKD[] elements)
   : base(name, elements)
   {
      this.Initialization();
      this.Elements = this.iSort.Sort(this.Elements);
   
      this.iSort = new UpDownSort();
      this.Elements = this.iSort.Sort(this.Elements);
   }

}