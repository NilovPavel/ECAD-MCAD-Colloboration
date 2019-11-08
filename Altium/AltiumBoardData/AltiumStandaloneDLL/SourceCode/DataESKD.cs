// File:    DataESKD.cs
// Author:  nilov_pg
// Created: 22 января 2019 г. 8:15:15
// Purpose: Definition of Class DataESKD

using System;

public class DataESKD
{
   private string формат;
   private string обозначение;
   private string наименование;
   private string разделСп;
   private string partNumber;
   private string примечание;
   private int позиция;
   private EskdSpecificationType specificationFlag;
   
   private IDataESKD iDataESKD;
   
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
      }
   }
   
   public string РазделСп
   {
      get
      {
         return разделСп;
      }
      set
      {
         this.разделСп = value;
      }
   }
   
   public string PartNumber
   {
      get
      {
         return partNumber;
      }
      set
      {
         this.partNumber = value;
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
   
   public int Позиция
   {
      get
      {
         return позиция;
      }
      set
      {
         this.позиция = value;
      }
   }
   
   public EskdSpecificationType SpecificationFlag
   {
      get
      {
         return specificationFlag;
      }
      set
      {
         this.specificationFlag = value;
      }
   }
   
   public DataESKD()
   {
   }
   
   public DataESKD(IDataESKD iDataESKD)
   {
      this.iDataESKD = iDataESKD;
      if (this.iDataESKD == null)
         return;
      this.формат = this.iDataESKD.GetFormat();
      this.обозначение = this.iDataESKD.GetObozn();
      this.наименование = this.iDataESKD.GetName();
      this.разделСп = this.iDataESKD.GetRazdelSP();
      this.примечание = this.iDataESKD.GetPrimechanie();
      this.partNumber = this.iDataESKD.GetPartNumber();
   }

}