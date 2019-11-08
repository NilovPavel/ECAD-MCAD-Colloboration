// File:    Cuprum.cs
// Author:  nilov_pg
// Created: 19 октября 2018 г. 9:10:37
// Purpose: Definition of Class Cuprum

using System;

public class Cuprum
{
   private ICuprum iCuprum;
   
   public System.Collections.Generic.List<Track> track;
   
   /// <summary>
   /// Property for collection of Track
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Track> Track
   {
      get
      {
         if (track == null)
            track = new System.Collections.Generic.List<Track>();
         return track;
      }
      set
      {
         RemoveAllTrack();
         if (value != null)
         {
            foreach (Track oTrack in value)
               AddTrack(oTrack);
         }
      }
   }
   
   /// <summary>
   /// Add a new Track in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddTrack(Track newTrack)
   {
      if (newTrack == null)
         return;
      if (this.track == null)
         this.track = new System.Collections.Generic.List<Track>();
      if (!this.track.Contains(newTrack))
         this.track.Add(newTrack);
   }
   
   /// <summary>
   /// Remove an existing Track from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveTrack(Track oldTrack)
   {
      if (oldTrack == null)
         return;
      if (this.track != null)
         if (this.track.Contains(oldTrack))
            this.track.Remove(oldTrack);
   }
   
   /// <summary>
   /// Remove all instances of Track from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllTrack()
   {
      if (track != null)
         track.Clear();
   }
   public System.Collections.Generic.List<Pad> pad;
   
   /// <summary>
   /// Property for collection of Pad
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Pad> Pad
   {
      get
      {
         if (pad == null)
            pad = new System.Collections.Generic.List<Pad>();
         return pad;
      }
      set
      {
         RemoveAllPad();
         if (value != null)
         {
            foreach (Pad oPad in value)
               AddPad(oPad);
         }
      }
   }
   
   /// <summary>
   /// Add a new Pad in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddPad(Pad newPad)
   {
      if (newPad == null)
         return;
      if (this.pad == null)
         this.pad = new System.Collections.Generic.List<Pad>();
      if (!this.pad.Contains(newPad))
         this.pad.Add(newPad);
   }
   
   /// <summary>
   /// Remove an existing Pad from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemovePad(Pad oldPad)
   {
      if (oldPad == null)
         return;
      if (this.pad != null)
         if (this.pad.Contains(oldPad))
            this.pad.Remove(oldPad);
   }
   
   /// <summary>
   /// Remove all instances of Pad from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllPad()
   {
      if (pad != null)
         pad.Clear();
   }
   public System.Collections.Generic.List<Region> region;
   
   /// <summary>
   /// Property for collection of Region
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Region> Region
   {
      get
      {
         if (region == null)
            region = new System.Collections.Generic.List<Region>();
         return region;
      }
      set
      {
         RemoveAllRegion();
         if (value != null)
         {
            foreach (Region oRegion in value)
               AddRegion(oRegion);
         }
      }
   }
   
   /// <summary>
   /// Add a new Region in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddRegion(Region newRegion)
   {
      if (newRegion == null)
         return;
      if (this.region == null)
         this.region = new System.Collections.Generic.List<Region>();
      if (!this.region.Contains(newRegion))
         this.region.Add(newRegion);
   }
   
   /// <summary>
   /// Remove an existing Region from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveRegion(Region oldRegion)
   {
      if (oldRegion == null)
         return;
      if (this.region != null)
         if (this.region.Contains(oldRegion))
            this.region.Remove(oldRegion);
   }
   
   /// <summary>
   /// Remove all instances of Region from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllRegion()
   {
      if (region != null)
         region.Clear();
   }
   public System.Collections.Generic.List<Polygon> polygon;
   
   /// <summary>
   /// Property for collection of Polygon
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Polygon> Polygon
   {
      get
      {
         if (polygon == null)
            polygon = new System.Collections.Generic.List<Polygon>();
         return polygon;
      }
      set
      {
         RemoveAllPolygon();
         if (value != null)
         {
            foreach (Polygon oPolygon in value)
               AddPolygon(oPolygon);
         }
      }
   }
   
   /// <summary>
   /// Add a new Polygon in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddPolygon(Polygon newPolygon)
   {
      if (newPolygon == null)
         return;
      if (this.polygon == null)
         this.polygon = new System.Collections.Generic.List<Polygon>();
      if (!this.polygon.Contains(newPolygon))
         this.polygon.Add(newPolygon);
   }
   
   /// <summary>
   /// Remove an existing Polygon from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemovePolygon(Polygon oldPolygon)
   {
      if (oldPolygon == null)
         return;
      if (this.polygon != null)
         if (this.polygon.Contains(oldPolygon))
            this.polygon.Remove(oldPolygon);
   }
   
   /// <summary>
   /// Remove all instances of Polygon from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllPolygon()
   {
      if (polygon != null)
         polygon.Clear();
   }
   public System.Collections.Generic.List<Fill> fill;
   
   /// <summary>
   /// Property for collection of Fill
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Fill> Fill
   {
      get
      {
         if (fill == null)
            fill = new System.Collections.Generic.List<Fill>();
         return fill;
      }
      set
      {
         RemoveAllFill();
         if (value != null)
         {
            foreach (Fill oFill in value)
               AddFill(oFill);
         }
      }
   }
   
   /// <summary>
   /// Add a new Fill in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddFill(Fill newFill)
   {
      if (newFill == null)
         return;
      if (this.fill == null)
         this.fill = new System.Collections.Generic.List<Fill>();
      if (!this.fill.Contains(newFill))
         this.fill.Add(newFill);
   }
   
   /// <summary>
   /// Remove an existing Fill from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveFill(Fill oldFill)
   {
      if (oldFill == null)
         return;
      if (this.fill != null)
         if (this.fill.Contains(oldFill))
            this.fill.Remove(oldFill);
   }
   
   /// <summary>
   /// Remove all instances of Fill from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllFill()
   {
      if (fill != null)
         fill.Clear();
   }

}