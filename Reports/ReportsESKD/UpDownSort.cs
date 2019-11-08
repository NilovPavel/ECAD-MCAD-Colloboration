// File:    UpDownSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:29:16
// Purpose: Definition of Class UpDownSort

using System;
using System.Linq;

public class UpDownSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      componentCollection = (from element in componentCollection
                                  orderby !element.Fitted
                                  select element).ToArray();
      return componentCollection;
   }

}