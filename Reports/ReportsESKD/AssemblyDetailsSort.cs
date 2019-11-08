// File:    AssemblyDetailsSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:26:45
// Purpose: Definition of Class AssemblyDetailsSort

using System;
using System.Linq;

public class AssemblyDetailsSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      Array.Sort(componentCollection, new AssemblyDetailsComparer());
      return componentCollection;
   }

}