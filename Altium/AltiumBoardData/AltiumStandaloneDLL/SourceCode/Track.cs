// File:    Track.cs
// Author:  nilov_pg
// Created: 18 декабря 2018 г. 14:35:59
// Purpose: Definition of Class Track

using System;

public class Track
{
   public System.Collections.Generic.List<Contour> contour;
   
   /// <summary>
   /// Property for collection of Contour
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Contour> Contour
   {
      get
      {
         if (contour == null)
            contour = new System.Collections.Generic.List<Contour>();
         return contour;
      }
      set
      {
         RemoveAllContour();
         if (value != null)
         {
            foreach (Contour oContour in value)
               AddContour(oContour);
         }
      }
   }
   
   /// <summary>
   /// Add a new Contour in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddContour(Contour newContour)
   {
      if (newContour == null)
         return;
      if (this.contour == null)
         this.contour = new System.Collections.Generic.List<Contour>();
      if (!this.contour.Contains(newContour))
         this.contour.Add(newContour);
   }
   
   /// <summary>
   /// Remove an existing Contour from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveContour(Contour oldContour)
   {
      if (oldContour == null)
         return;
      if (this.contour != null)
         if (this.contour.Contains(oldContour))
            this.contour.Remove(oldContour);
   }
   
   /// <summary>
   /// Remove all instances of Contour from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllContour()
   {
      if (contour != null)
         contour.Clear();
   }

}