// File:    SpecificationRazdel.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:03:22
// Purpose: Definition of Class SpecificationRazdel

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpecificationRazdel : Spisok
{
   private SpecificationRazdelCombiner specificationRazdelCombiner;

   private void Initialization()
   {
      switch (this.Name)
      {
         case "Документация":
            this.iSort = new DocumentationSort();
            this.iGroup = new NotGroup();
            this.specificationRazdelCombiner = new DocumentationComplectsCombiner(this.Elements);
               break;
         case "Комплексы":
            this.iSort = new AssemblyDetailsSort();
            this.iGroup = new NotGroup();
            this.specificationRazdelCombiner = new AssemblyDetailsCombiner(this.Elements);
                break;
         case "Сборочные единицы":
            this.iSort = new AssemblyDetailsSort();
            this.iGroup = new NotGroup();
            this.specificationRazdelCombiner = new AssemblyDetailsCombiner(this.Elements);
                break;
         case "Детали":
            this.iSort = new AssemblyDetailsSort();
            this.iGroup = new NotGroup();
            this.specificationRazdelCombiner = new AssemblyDetailsCombiner(this.Elements);
                break;
         case "Стандартные изделия":
            this.iSort = new NotSort();
            this.iGroup = new StandartGroup();
            this.specificationRazdelCombiner = new StandartsCombiner(this.Elements);
                break;
         case "Прочие изделия":
            this.iSort = new NotSort();
            this.iGroup = new OtherElementsGroup();
            this.specificationRazdelCombiner = new OtherCombiner(this.Elements);
                break;
         case "Материалы":
            this.iSort = new MaterialSort();
            this.iGroup = new NotGroup();
            this.specificationRazdelCombiner = new MaterialCombiner(this.Elements);
                break;
         case "Комплекты":
            this.iSort = new DocumentationSort();
            this.iGroup = new NotGroup();
            this.specificationRazdelCombiner = new DocumentationComplectsCombiner(this.Elements);
                break;
      }
   }
   
   public SpecificationRazdel(string name, RecordESKD[] elements)
   : base(name, elements)
   {
      this.Initialization();

      this.Elements = this.specificationRazdelCombiner.ComponentCollection;

      this.Elements = this.iSort.Sort(this.Elements);
      this.Razdels = this.iGroup.Group(this.Elements).ToList();
   
      this.iSort = new UpDownSort();
      this.Elements = this.iSort.Sort(this.Elements);
   
      if (this.Razdels.Count != 0)
         this.Elements = null;
   }

}