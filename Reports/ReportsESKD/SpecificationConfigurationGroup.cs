// File:    SpecificationConfigurationGroup.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:22:54
// Purpose: Definition of Class SpecificationConfigurationGroup

using System;
using System.Collections.Generic;
using System.Linq;

public class SpecificationConfigurationGroup : IGroup
{
   private string[] specificationRazdels;
   
   public Spisok[] Group(RecordESKD[] groupCollection)
   {
      List<Spisok> razdels = new List<Spisok>();
      for (int razdelCount = 0; razdelCount < this.specificationRazdels.Length; razdelCount++)
      {
         string name = this.specificationRazdels[razdelCount];
         RecordESKD[] elements = groupCollection.ToList().Where(item => item.РазделСп.Equals(name)).ToArray();
         if (elements.Length == 0)
            continue;
         razdels.Add(new SpecificationRazdel(name, elements));
      }
      return razdels.ToArray();
   }
   
   public SpecificationConfigurationGroup()
   {
      this.specificationRazdels = new string[] { "Документация", "Комплексы", "Сборочные единицы", "Детали", "Стандартные изделия", "Прочие изделия", "Материалы", "Комплекты" };
   }

}