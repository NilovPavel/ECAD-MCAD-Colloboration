// File:    Paint.cs
// Author:  Павел
// Created: 29 марта 2020 г. 21:48:24
// Purpose: Definition of Class Paint

using System;

public class Paint
{
   private System.Collections.Generic.List<Text> text;
   
   /// <summary>
   /// Property for collection of Text
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Text> Text
   {
      get
      {
         if (text == null)
            text = new System.Collections.Generic.List<Text>();
         return text;
      }
      set
      {
         RemoveAllText();
         if (value != null)
         {
            foreach (Text oText in value)
               AddText(oText);
         }
      }
   }
   
   /// <summary>
   /// Add a new Text in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddText(Text newText)
   {
      if (newText == null)
         return;
      if (this.text == null)
         this.text = new System.Collections.Generic.List<Text>();
      if (!this.text.Contains(newText))
         this.text.Add(newText);
   }
   
   /// <summary>
   /// Remove an existing Text from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveText(Text oldText)
   {
      if (oldText == null)
         return;
      if (this.text != null)
         if (this.text.Contains(oldText))
            this.text.Remove(oldText);
   }
   
   /// <summary>
   /// Remove all instances of Text from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllText()
   {
      if (text != null)
         text.Clear();
   }
   private System.Collections.Generic.List<Polygon> polygon;
   
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
   private System.Collections.Generic.List<Tracks> tracks;
   
   /// <summary>
   /// Property for collection of Tracks
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Tracks> Tracks
   {
      get
      {
         if (tracks == null)
            tracks = new System.Collections.Generic.List<Tracks>();
         return tracks;
      }
      set
      {
         RemoveAllTracks();
         if (value != null)
         {
            foreach (Tracks oTracks in value)
               AddTracks(oTracks);
         }
      }
   }
   
   /// <summary>
   /// Add a new Tracks in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddTracks(Tracks newTracks)
   {
      if (newTracks == null)
         return;
      if (this.tracks == null)
         this.tracks = new System.Collections.Generic.List<Tracks>();
      if (!this.tracks.Contains(newTracks))
         this.tracks.Add(newTracks);
   }
   
   /// <summary>
   /// Remove an existing Tracks from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveTracks(Tracks oldTracks)
   {
      if (oldTracks == null)
         return;
      if (this.tracks != null)
         if (this.tracks.Contains(oldTracks))
            this.tracks.Remove(oldTracks);
   }
   
   /// <summary>
   /// Remove all instances of Tracks from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllTracks()
   {
      if (tracks != null)
         tracks.Clear();
   }
   private IPaint iPaint;
   
   public Paint()
   {
   }
   
   public Paint(IPaint iPaint)
   {
        this.iPaint = this.iPaint;
   }

}