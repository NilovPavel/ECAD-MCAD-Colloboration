// File:    NotesMaterialsESKD.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 16:19:08
// Purpose: Definition of Class NotesMaterialsESKD

using System;

public class NotesMaterialsESKD : INotesRazdelESKD
{
   private MaterialData[] materialData;
   
   public string[] GetColumnNames()
   {
      return new string[] { "Наименование", "Количество", "Ед.изм.", "Примечание" };
   }
   
   public string GetDefaultValue()
   {
      return "0";
   }

}