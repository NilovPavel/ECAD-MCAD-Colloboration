// File:    StandartParts.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 14:31:58
// Purpose: Definition of Class StandartParts

using System;

public class StandartParts
{
   protected string[] GetColumnNames()
   {
      return new string[] { "Обозначение", "Наименование", "Примечание" };
   }
   
   public StandartParts(Notes notes)
   {
   }

}