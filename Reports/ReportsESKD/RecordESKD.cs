// File:    RecordESKD.cs
// Author:  nilov_pg
// Created: 21 июня 2019 г. 9:07:52
// Purpose: Definition of Class RecordESKD

using System;

public class RecordESKD
{
   private ComponentCAD componentCAD;
   private string cadID;
   private bool fitted;
   private string обозначение;
   private string наименование;
   private string разделСп;
   private string partNumber;
   private int позиция;
   private string формат;
   private string примечание;
   private string designator;
   private double количество;
   private string едИзм;
   private string кодДокумента;
   private string status;
   
   public string CadID
   {
      get
      {
         return cadID;
      }
      set
      {
         this.cadID = value;
      }
   }
   
   public bool Fitted
   {
      get
      {
         return fitted;
      }
      set
      {
         this.fitted = value;
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
   
   public string Designator
   {
      get
      {
         return designator;
      }
      set
      {
         this.designator = value;
      }
   }
   
   public double Количество
   {
      get
      {
         return количество;
      }
      set
      {
         this.количество = value;
      }
   }
   
   public string ЕдИзм
   {
      get
      {
         return едИзм;
      }
      set
      {
         this.едИзм = value;
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
   
   public string Status
   {
      get
      {
         return status;
      }
      set
      {
         this.status = value;
      }
   }
   
   public RecordESKD(ComponentCAD componentCAD)
   {
        this.componentCAD = componentCAD;
        this.cadID = this.componentCAD.UniqueID;
        this.формат = this.componentCAD.dataESKD.Формат ?? string.Empty;
        this.designator = this.componentCAD.Designator ?? string.Empty;
        this.partNumber = this.componentCAD.dataESKD.PartNumber == null ? string.Empty : this.componentCAD.dataESKD.PartNumber;
        this.наименование = this.componentCAD.dataESKD.Наименование == null ? string.Empty : this.componentCAD.dataESKD.Наименование;
        this.обозначение = this.componentCAD.dataESKD.Обозначение == null ? string.Empty : this.componentCAD.dataESKD.Обозначение;
        this.разделСп = this.componentCAD.dataESKD.РазделСп == null ? string.Empty : this.componentCAD.dataESKD.РазделСп;
        this.fitted = this.componentCAD.Fitted;
   }
   
   public RecordESKD()
   {
   }

}