// File:    SpecificationConfiguration.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 15:59:49
// Purpose: Definition of Class SpecificationConfiguration

using System;
using System.Linq;

public class SpecificationConfiguration : Spisok
{
   public void SetPositionNumbers(RecordESKD[] records)
   {
      IteratorSpisok iteratorSpisok = new IteratorSpisok(this);
      IIteratorAction iteratorVariablePositions = new IteratorVariablePositions(records);
      iteratorSpisok.SetIteratorAction(iteratorVariablePositions);
      iteratorSpisok.Iteration();
   }
   
   public SpecificationConfiguration(string name, RecordESKD[] elements)
   : base (name, elements)
   {
      this.iGroup = new SpecificationConfigurationGroup();
      this.iSort = new SpecificationConfigurationSort();
      this.Elements = this.iSort.Sort(this.Elements);
      this.Razdels = this.iGroup.Group(this.Elements).ToList();
      this.Elements = null;
   }

}