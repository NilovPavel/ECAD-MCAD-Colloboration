// File:    Cable.cs
// Author:  nilov_pg
// Created: 26 августа 2019 г. 15:53:41
// Purpose: Definition of Class Cable

using System;







public class Cable
{
   private double length;
   private string material;
   private double crossSection;
   private ICable iCable;
   
   public System.Collections.Generic.List<Wire> wire;
   
   public double Length
   {
      get
      {
         return length;
      }
      set
      {
         this.length = value;
      }
   }
   
   public string Material
   {
      get
      {
         return material;
      }
      set
      {
         this.material = value;
      }
   }
   
   public double CrossSection
   {
      get
      {
         return crossSection;
      }
      set
      {
         this.crossSection = value;
      }
   }
   
   public Cable(ICable iCable)
   {
       this.iCable = iCable;
       foreach(IWire iWire in this.iCable.GetIWires())
           this.wire.Add(new Wire(iWire));
   }
   
   public Cable()
   {}

}
