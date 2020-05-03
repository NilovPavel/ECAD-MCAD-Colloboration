// File:    DocumentData.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 15:47:04
// Purpose: Definition of Class DocumentData

using System;
using System.Linq;

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
        set
        {
            this.формат = value;
        }
   }
   
   public string Обозначение
   {
      get
      {
         return обозначение;
      }
        set
        {
            this.обозначение = value;
        }
    }
   
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
            this.кодДокумента = sectionPair != null ? sectionPair.Value : string.Empty;
        }
    }
   
   public string КодДокумента
   {
      get
      {
         return кодДокумента;
      }
        set
        {
            this.кодДокумента = value;
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
   
   public DocumentData(DefaultNotesReader defaultNotesReader)
   {
        this.defaultNotesReader = defaultNotesReader;
   }

}