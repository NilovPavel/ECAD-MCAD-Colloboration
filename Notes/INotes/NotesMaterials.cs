// File:    NotesMaterialsESKD.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 16:19:08
// Purpose: Definition of Class NotesMaterialsESKD

using System;

public class NotesMaterials : INotesRazdel
{
   public string[] GetColumnNames()
   {
      return new string[] { "Наименование", "Количество", "Едизм", "Примечание" };
   }
   
   public string GetDefaultValue()
   {
      return "0";
   }

}