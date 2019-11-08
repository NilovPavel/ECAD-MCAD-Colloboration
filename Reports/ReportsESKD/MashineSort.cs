// File:    MashineSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:28:41
// Purpose: Definition of Class MashineSort

using System;
using System.Linq;

public class MashineSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      componentCollection = (from element in componentCollection
                                  orderby element.Наименование
                                  select element).ToArray();
      return componentCollection;
   }

}