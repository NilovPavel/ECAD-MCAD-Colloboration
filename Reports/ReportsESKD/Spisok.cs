// File:    Spisok.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 15:54:49
// Purpose: Definition of Class Spisok

using System;
using System.Linq;

public abstract class Spisok
{
   private string name;
   private RecordESKD[] elements;
   
   private System.Collections.Generic.List<Spisok> razdels;
   
   /// <summary>
   /// Property for collection of Spisok
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Spisok> Razdels
   {
      get
      {
         if (razdels == null)
            razdels = new System.Collections.Generic.List<Spisok>();
         return razdels;
      }
      set
      {
         RemoveAllRazdels();
         if (value != null)
         {
            foreach (Spisok oSpisok in value)
               AddRazdels(oSpisok);
         }
      }
   }
   
   /// <summary>
   /// Add a new Spisok in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddRazdels(Spisok newSpisok)
   {
      if (newSpisok == null)
         return;
      if (this.razdels == null)
         this.razdels = new System.Collections.Generic.List<Spisok>();
      if (!this.razdels.Contains(newSpisok))
         this.razdels.Add(newSpisok);
   }
   
   /// <summary>
   /// Remove an existing Spisok from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveRazdels(Spisok oldSpisok)
   {
      if (oldSpisok == null)
         return;
      if (this.razdels != null)
         if (this.razdels.Contains(oldSpisok))
            this.razdels.Remove(oldSpisok);
   }
   
   /// <summary>
   /// Remove all instances of Spisok from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllRazdels()
   {
      if (razdels != null)
         razdels.Clear();
   }
   
   protected ISort iSort;
   protected IGroup iGroup;
   
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
   
   public RecordESKD[] Elements
   {
      get
      {
         return elements;
      }
      set
      {
         this.elements = value;
      }
   }
   
   public Spisok(string name, RecordESKD[] elements)
   {
      this.name = name;
      this.elements = elements;
   }
   
   public Spisok(string name, VariantESKD[] variants)
   {
        this.name = name;
      foreach (VariantESKD variantESKD in variants)
         this.AddRazdels(variantESKD);
   }

}