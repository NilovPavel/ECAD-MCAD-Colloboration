// File:    IteratorCollection.cs
// Author:  nilov_pg
// Created: 9 августа 2019 г. 12:09:16
// Purpose: Definition of Class IteratorCollection

using System;

public class IteratorCollection : IIteratorAction
{
   private System.Collections.Generic.List<RecordESKD> recordESKD;
   
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
   
   public void ElementAction(RecordESKD recordESKD)
   {
      RecordESKD recordESKDClone = new RecordESKD
      {
               CadID = recordESKD.Designator,
               Designator = recordESKD.Designator,
               Формат = recordESKD.Формат,
               Обозначение = recordESKD.Обозначение,
               Наименование = recordESKD.Наименование,
               PartNumber = recordESKD.PartNumber,
               КодДокумента = recordESKD.КодДокумента,
               ЕдИзм = recordESKD.ЕдИзм,
               Количество = recordESKD.Количество,
               Fitted = recordESKD.Fitted,
               Примечание = recordESKD.Примечание,
               РазделСп = recordESKD.РазделСп,
               Позиция = recordESKD.Позиция
       };
       this.AddRecordESKD(recordESKDClone);
   }
   
   public void RazdelAction(Spisok spisok)
   {
      return;
   }

}