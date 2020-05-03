// File:    AbstractReport.cs
// Author:  nilov_pg
// Created: 19 июня 2019 г. 15:29:41
// Purpose: Definition of Class AbstractReport

using System;
using System.Linq;

public abstract class AbstractReport
{
   protected Assembly assembly;
   
   //protected ICheck iCheck;
   
   protected bool IsBadVariants(VariantESKD[] variants)
   {
      bool isNullCollection = variants.Count(item => item.RecordESKD == null) > 0;
      if (isNullCollection)
         return true;
      bool variantsIsNot = variants.Length == 0;
      bool isEmptyVariants = variants.Count(item => item.Elements.Count() == 0) > 0;
      return variantsIsNot || isEmptyVariants;
   }
   
   public System.Collections.Generic.List<Spisok> spisok;
   
   /// <summary>
   /// Property for collection of Spisok
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Spisok> Spisok
   {
      get
      {
         if (spisok == null)
            spisok = new System.Collections.Generic.List<Spisok>();
         return spisok;
      }
      set
      {
         RemoveAllSpisok();
         if (value != null)
         {
            foreach (Spisok oSpisok in value)
               AddSpisok(oSpisok);
         }
      }
   }
   
   /// <summary>
   /// Add a new Spisok in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddSpisok(Spisok newSpisok)
   {
      if (newSpisok == null)
         return;
      if (this.spisok == null)
         this.spisok = new System.Collections.Generic.List<Spisok>();
      if (!this.spisok.Contains(newSpisok))
         this.spisok.Add(newSpisok);
   }
   
   /// <summary>
   /// Remove an existing Spisok from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveSpisok(Spisok oldSpisok)
   {
      if (oldSpisok == null)
         return;
      if (this.spisok != null)
         if (this.spisok.Contains(oldSpisok))
            this.spisok.Remove(oldSpisok);
   }
   
   /// <summary>
   /// Remove all instances of Spisok from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllSpisok()
   {
      if (spisok != null)
         spisok.Clear();
   }
   
   public Assembly Assembly
   {
      get
      {
         return assembly;
      }
   }
   
   public AbstractReport(Assembly assembly)
   {
      this.assembly = assembly;
   }

}