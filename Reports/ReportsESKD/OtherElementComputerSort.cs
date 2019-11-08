// File:    OtherElementComputerSort.cs
// Author:  nilov_pg
// Created: 3 июля 2019 г. 14:42:51
// Purpose: Definition of Class OtherElementComputerSort

using System;
using System.Linq;

public class OtherElementComputerSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      componentCollection = (from element in componentCollection
                                  orderby element.Наименование
                                  select element).ToArray();
      return componentCollection;
   }

}