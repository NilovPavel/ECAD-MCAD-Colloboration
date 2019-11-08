// File:    VariantESKD.cs
// Author:  nilov_pg
// Created: 21 июня 2019 г. 9:11:48
// Purpose: Definition of Class VariantESKD

using System;

public class VariantESKD : Spisok
{
   public System.Collections.Generic.List<RecordESKD> recordESKD;
   
   /// <summary>
   /// Property for collection of RecordESKD
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<RecordESKD> RecordESKD
   {
      get
      {
         if (recordESKD == null)
            recordESKD = new System.Collections.Generic.List<RecordESKD>();
         return recordESKD;
      }
      set
      {
         RemoveAllRecordESKD();
         if (value != null)
         {
            foreach (RecordESKD oRecordESKD in value)
               AddRecordESKD(oRecordESKD);
         }
      }
   }
   
   /// <summary>
   /// Add a new RecordESKD in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddRecordESKD(RecordESKD newRecordESKD)
   {
      if (newRecordESKD == null)
         return;
      if (this.recordESKD == null)
         this.recordESKD = new System.Collections.Generic.List<RecordESKD>();
      if (!this.recordESKD.Contains(newRecordESKD))
         this.recordESKD.Add(newRecordESKD);
   }
   
   /// <summary>
   /// Remove an existing RecordESKD from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveRecordESKD(RecordESKD oldRecordESKD)
   {
      if (oldRecordESKD == null)
         return;
      if (this.recordESKD != null)
         if (this.recordESKD.Contains(oldRecordESKD))
            this.recordESKD.Remove(oldRecordESKD);
   }
   
   /// <summary>
   /// Remove all instances of RecordESKD from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllRecordESKD()
   {
      if (recordESKD != null)
         recordESKD.Clear();
   }
   
   public VariantESKD(string name, RecordESKD[] elements)
   : base(name, elements)
   {
   }

}