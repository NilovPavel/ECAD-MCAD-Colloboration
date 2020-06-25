// File:    NotesComplectsESKD.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 16:11:26
// Purpose: Definition of Class NotesComplectsESKD

using System;

public class NotesComplectsESKD : INotesRazdelESKD
{
   public string[] GetColumnNames()
   {
      return new string[] { "Формат", "Обозначение", "Наименование", "Код_документа", "Примечание" };
   }
   
   public string GetDefaultValue()
   {
      return "1";
   }

}