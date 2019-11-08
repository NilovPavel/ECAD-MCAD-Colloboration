// File:    StandartSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:27:16
// Purpose: Definition of Class StandartSort

using System;
using System.Collections.Generic;
using System.Linq;

public class StandartSort : ISort
{
   private System.Collections.Generic.List<StandartElement> sortedStandartElements;
   
   /// <summary>
   /// Property for collection of StandartElement
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<StandartElement> SortedStandartElements
   {
      get
      {
         if (sortedStandartElements == null)
            sortedStandartElements = new System.Collections.Generic.List<StandartElement>();
         return sortedStandartElements;
      }
      set
      {
         RemoveAllSortedStandartElements();
         if (value != null)
         {
            foreach (StandartElement oStandartElement in value)
               AddSortedStandartElements(oStandartElement);
         }
      }
   }
   
   /// <summary>
   /// Add a new StandartElement in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddSortedStandartElements(StandartElement newStandartElement)
   {
      if (newStandartElement == null)
         return;
      if (this.sortedStandartElements == null)
         this.sortedStandartElements = new System.Collections.Generic.List<StandartElement>();
      if (!this.sortedStandartElements.Contains(newStandartElement))
         this.sortedStandartElements.Add(newStandartElement);
   }
   
   /// <summary>
   /// Remove an existing StandartElement from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveSortedStandartElements(StandartElement oldStandartElement)
   {
      if (oldStandartElement == null)
         return;
      if (this.sortedStandartElements != null)
         if (this.sortedStandartElements.Contains(oldStandartElement))
            this.sortedStandartElements.Remove(oldStandartElement);
   }
   
   /// <summary>
   /// Remove all instances of StandartElement from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllSortedStandartElements()
   {
      if (sortedStandartElements != null)
         sortedStandartElements.Clear();
   }
   
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
      for (int elementCount = 0; elementCount < componentCollection.Length; elementCount++)
      {
         StandartElement currentElement = new StandartElement(componentCollection[elementCount].Наименование);
         this.AddSortedStandartElements(currentElement);
      }
      this.sortedStandartElements.Sort(new StandartElementComparer());
      RecordESKD[] elements = (from elementStandart in this.sortedStandartElements
                               join element in componentCollection
                               on elementStandart.FullName equals element.Наименование
                               select element).Distinct().ToArray();
      return elements;
   }

}