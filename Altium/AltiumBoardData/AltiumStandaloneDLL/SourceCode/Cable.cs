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
   
   /// <summary>
   /// Property for collection of Wire
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Wire> Wire
   {
      get
      {
         if (wire == null)
            wire = new System.Collections.Generic.List<Wire>();
         return wire;
      }
      set
      {
         RemoveAllWire();
         if (value != null)
         {
            foreach (Wire oWire in value)
               AddWire(oWire);
         }
      }
   }
   
   /// <summary>
   /// Add a new Wire in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddWire(Wire newWire)
   {
      if (newWire == null)
         return;
      if (this.wire == null)
         this.wire = new System.Collections.Generic.List<Wire>();
      if (!this.wire.Contains(newWire))
         this.wire.Add(newWire);
   }
   
   /// <summary>
   /// Remove an existing Wire from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveWire(Wire oldWire)
   {
      if (oldWire == null)
         return;
      if (this.wire != null)
         if (this.wire.Contains(oldWire))
            this.wire.Remove(oldWire);
   }
   
   /// <summary>
   /// Remove all instances of Wire from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllWire()
   {
      if (wire != null)
         wire.Clear();
   }
   
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
