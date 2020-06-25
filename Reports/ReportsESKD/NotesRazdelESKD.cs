// File:    NotesRazdelESKD.cs
// Author:  nilov_pg
// Created: 12 июля 2019 г. 17:20:16
// Purpose: Definition of Class NotesRazdelESKD

using System;
using System.Collections.Generic;

public class NotesRazdelESKD
{
   private string[] columnNames;
   private Notes notes;
   
   private INotesRazdelESKD iNotesRazdelESKD;
   
   private void Initialization()
   {
      this.iNotesRazdelESKD = this.GetINotesRazdelESKD();
      this.columnNames = this.iNotesRazdelESKD.GetColumnNames();
   }
   
   private double GetCount(string[] splitString, int variantCount)
   {
      string stringValue = this.GetVariantValue(splitString, variantCount);
      double doubleValue;
      double.TryParse(stringValue, out doubleValue);
      return doubleValue;
   }
   
   private string GetParameterValue(string parameterName, string[] splitString)
   {
      int parameterArrayIndex = Array.IndexOf(this.columnNames, parameterName);
      string parameterValue = parameterArrayIndex == -1 ? string.Empty : splitString[parameterArrayIndex];
      return parameterValue;
   }
   
   private string GetVariantValue(string[] splitString, int variantCount)
   {
      string stringValue = (this.columnNames.Length + variantCount < splitString.Length) ? splitString[this.columnNames.Length + variantCount] : this.iNotesRazdelESKD.GetDefaultValue();
      return stringValue;
   }
   
   private INotesRazdelESKD GetINotesRazdelESKD()
   {
      switch(notes.RazdelName)
      {
         case "Документация":
         //case "Документация ЭМ":
            return new NotesDocumentsESKD();
         case "Комплекты":
         //case "Комплекты ЭМ":
            return new NotesComplectsESKD();
         case "Материалы":
         //case "Материалы ЭМ":
            return new NotesMaterialsESKD();
      }
      return default(INotesRazdelESKD);
   }
   
   protected DefaultNotesReader defaultNotesReader;
   
   public string[] ColumnNames
   {
      get
      {
         return this.columnNames;
      }
   }
   
   public RecordESKD[] GetRecordsESKD(int variantCount)
   {
      List<RecordESKD> recordsNotesESKD = new List<RecordESKD>();
      foreach (string elementString in this.notes.RazdelNotesCollection)
      {
         string[] splitStrings = elementString.Split('#');
         recordsNotesESKD.Add(new RecordESKD
         {
            CadID = this.GetParameterValue("Обозначение", splitStrings),
            Designator = string.Empty,
            Формат = this.GetParameterValue("Формат", splitStrings),
            Обозначение = this.GetParameterValue("Обозначение", splitStrings),
            Наименование = this.GetParameterValue("Наименование", splitStrings),
            PartNumber = this.GetParameterValue("Наименование", splitStrings),
            КодДокумента = this.GetParameterValue("Код_документа", splitStrings),
            ЕдИзм = this.GetParameterValue("Ед.изм.", splitStrings),
            Количество = this.GetCount(splitStrings, variantCount),
            Fitted = this.GetVariantValue(splitStrings, variantCount).Equals("0") ? false : true,
            Примечание = this.GetParameterValue("Примечание", splitStrings),
            РазделСп = this.notes.RazdelName,
         });
      }
      return recordsNotesESKD.ToArray();
   }
   
   public void WriteNotes(string[] collection)
   {
      this.notes.RazdelNotesCollection = collection;
      this.notes.WriteNotes();
   }
   
   public NotesRazdelESKD(Notes notes)
   {
      this.notes = notes;
      this.Initialization();
   }

}