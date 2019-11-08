// File:    SpecificationConfigurationSort.cs
// Author:  nilov_pg
// Created: 1 июля 2019 г. 12:15:08
// Purpose: Definition of Class SpecificationConfigurationSort

using System;

public class SpecificationConfigurationSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
       return componentCollection;
   }

}