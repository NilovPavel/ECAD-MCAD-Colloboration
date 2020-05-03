// File:    MaterialData.cs
// Author:  nilov_pg
// Created: 12 июля 2019 г. 17:20:16
// Purpose: Definition of Class MaterialData

using System;
using System.Linq;

public class MaterialData
{
   private string наименование;
   private string едизм;
   private string примечание;

    private DefaultNotesReader defaultNotesReader;

   public string Наименование
   {
      get
      {
         return наименование;
      }
        set
        {
            this.наименование = value;
            SectionPair sectionPair = this.defaultNotesReader.SectionPair.Where(item => item.Name.Equals(this.наименование)).FirstOrDefault();
            this.едизм = sectionPair != null ? sectionPair.Value : string.Empty;
        }
    }
   
   public string Едизм
   {
      get
      {
         return едизм;
      }
        set
        {
            this.едизм = value;
        }
    }
   
   public string Примечание
   {
      get
      {
         return примечание;
      }
        set
        {
            this.примечание = value;
        }
    }

    public MaterialData(DefaultNotesReader defaultNotesReader)
   {
        this.defaultNotesReader = defaultNotesReader;
   }
}