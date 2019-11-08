// File:    AssemblyDetails.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 14:29:31
// Purpose: Definition of Class AssemblyDetails

using System;

public class AssemblyDetails
{
   protected string[] GetColumnNames()
   {
      return new string[] { "Формат", "Обозначение", "Наименование", "Примечание" };
   }
   
   public AssemblyDetails(Notes notes)
   {
   }

}