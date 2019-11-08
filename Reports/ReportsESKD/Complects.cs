// File:    Complects.cs
// Author:  nilov_pg
// Created: 12 июля 2019 г. 17:20:16
// Purpose: Definition of Class Complects

using System;

public class Complects
{
   protected string[] GetColumnNames()
   {
      return new string[] { "Формат", "Обозначение", "Наименование", "Код_документа", "Примечание" };
   }
   
   public Complects(Notes notes)
   {
   }

}