// File:    NotSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:26:10
// Purpose: Definition of Class NotSort

using System;
using System.Collections.Generic;

public class NotSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      return componentCollection;
   }

}