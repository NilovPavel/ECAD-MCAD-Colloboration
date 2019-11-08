// File:    DocumentData.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 15:47:04
// Purpose: Definition of Class DocumentData

using System;

public class DocumentData
{
   private string формат;
   private string обозначение;
   private string наименование;
   private string кодДокумента;
   private string примечание;
   
   private DefaultNotesReader defaultNotesReader;
   
   public string Формат
   {
      get
      {
         return формат;
      }
   }
   
   public string Обозначение
   {
      get
      {
         return обозначение;
      }
   }
   
   public string Наименование
   {
      get
      {
         return наименование;
      }
   }
   
   public string КодДокумента
   {
      get
      {
         return кодДокумента;
      }
   }
   
   public string Примечание
   {
      get
      {
         return примечание;
      }
   }
   
   public DocumentData(DefaultNotesReader defaultNotesReader)
   {
      this.defaultNotesReader = defaultNotesReader;
   }

}