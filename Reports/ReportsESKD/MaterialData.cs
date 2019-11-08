// File:    MaterialData.cs
// Author:  nilov_pg
// Created: 12 июля 2019 г. 17:20:16
// Purpose: Definition of Class MaterialData

using System;

public class MaterialData
{
   private string наименование;
   private string количество;
   private string едизм;
   private string примечание;
   
   private DefaultNotesReader defaultNotesReader;
   
   public string Наименование
   {
      get
      {
         return наименование;
      }
   }
   
   public string Количество
   {
      get
      {
         return количество;
      }
   }
   
   public string Едизм
   {
      get
      {
         return едизм;
      }
   }
   
   public string Примечание
   {
      get
      {
         return примечание;
      }
   }
   
   public MaterialData(DefaultNotesReader defaultNotesReader)
   {
      this.defaultNotesReader = defaultNotesReader;
   }

}