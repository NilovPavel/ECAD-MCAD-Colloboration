// File:    Layer.cs
// Author:  nilov_pg
// Created: 12 сентября 2018 г. 11:39:47
// Purpose: Definition of Class Layer

using System;

public class Layer
{
   private string name;
   private int number;
   private double originalHeight;
   private string typeName;
   private double thickness;
   private string uniqueID;
   
   private ILayer iLayer;
   
   public string Name
   {
      get
      {
         return name;
      }
      set
      {
         this.name = value;
      }
   }
   
   public int Number
   {
      get
      {
         return number;
      }
      set
      {
         this.number = value;
      }
   }
   
   public string TypeName
   {
      get
      {
         return typeName;
      }
      set
      {
         this.typeName = value;
      }
   }
   
   public double OriginalHeight
   {
      get
      {
         return originalHeight;
      }
      set
      {
         this.originalHeight = value;
      }
   }
   
   public double Thickness
   {
      get
      {
         return thickness;
      }
      set
      {
         this.thickness = value;
      }
   }
   
   public string UniqueID
   {
      get
      {
         return uniqueID;
      }
      set
      {
         this.uniqueID = value;
      }
   }
   
   public Layer()
   {
   }
   
   public Layer(ILayer iLayer)
   {
      this.iLayer = iLayer;
           this.name = this.iLayer.LayerName();
           this.number = this.iLayer.LayerNumber();
           this.thickness = this.iLayer.GetThickness();
           this.typeName = this.iLayer.GetTypeName();
           this.originalHeight = this.iLayer.GetOriginHeight();
   }

}