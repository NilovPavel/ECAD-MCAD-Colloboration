// File:    DocumentationSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:26:30
// Purpose: Definition of Class DocumentationSort

using System;
using System.Linq;

public class DocumentationSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      //componentCollection = componentCollection.Where(item => item.Fitted == true).ToArray();
      Array.Sort(componentCollection, new DocumentationComparer());
      return componentCollection;
   }

}