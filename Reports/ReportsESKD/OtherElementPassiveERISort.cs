// File:    OtherElementPassiveERISort.cs
// Author:  nilov_pg
// Created: 3 июля 2019 г. 14:41:22
// Purpose: Definition of Class OtherElementPassiveERISort

using System;
using System.Collections.Generic;
using System.Linq;

public class OtherElementPassiveERISort : ISort
{
   private string edIzm;
   
   private System.Collections.Generic.List<Eri> sortedElementsERI;
   
   /// <summary>
   /// Property for collection of Eri
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Eri> SortedElementsERI
   {
      get
      {
         if (sortedElementsERI == null)
            sortedElementsERI = new System.Collections.Generic.List<Eri>();
         return sortedElementsERI;
      }
      set
      {
         RemoveAllSortedElementsERI();
         if (value != null)
         {
            foreach (Eri oEri in value)
               AddSortedElementsERI(oEri);
         }
      }
   }
   
   /// <summary>
   /// Add a new Eri in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddSortedElementsERI(Eri newEri)
   {
      if (newEri == null)
         return;
      if (this.sortedElementsERI == null)
         this.sortedElementsERI = new System.Collections.Generic.List<Eri>();
      if (!this.sortedElementsERI.Contains(newEri))
         this.sortedElementsERI.Add(newEri);
   }
   
   /// <summary>
   /// Remove an existing Eri from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveSortedElementsERI(Eri oldEri)
   {
      if (oldEri == null)
         return;
      if (this.sortedElementsERI != null)
         if (this.sortedElementsERI.Contains(oldEri))
            this.sortedElementsERI.Remove(oldEri);
   }
   
   /// <summary>
   /// Remove all instances of Eri from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllSortedElementsERI()
   {
      if (sortedElementsERI != null)
         sortedElementsERI.Clear();
   }
   
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      Array.ForEach(componentCollection, item => this.AddSortedElementsERI(new Eri(item.Наименование, this.edIzm)));
      this.sortedElementsERI = this.sortedElementsERI.Distinct(new EriEqualityComparer()).ToList();
      this.sortedElementsERI.Sort(new PassiveEriComparer());
      componentCollection = (from elementsERI in this.sortedElementsERI
                                  join element in componentCollection
                                  on elementsERI.FullEriName equals element.Наименование
                                  //orderby !element.Fitted
                                  select element).Distinct().ToArray();
      return componentCollection;
   }
   
   public OtherElementPassiveERISort(string edIzm)
   {
      this.edIzm = edIzm;
   }

}