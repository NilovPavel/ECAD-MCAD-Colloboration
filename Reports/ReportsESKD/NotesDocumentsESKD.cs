// File:    NotesDocumentsESKD.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 16:09:03
// Purpose: Definition of Class NotesDocumentsESKD

using System;

public class NotesDocumentsESKD : INotesRazdelESKD
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