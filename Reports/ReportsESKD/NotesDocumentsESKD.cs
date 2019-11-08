// File:    NotesDocumentsESKD.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 16:09:03
// Purpose: Definition of Class NotesDocumentsESKD

using System;

public class NotesDocumentsESKD : INotesRazdelESKD
{
   private System.Collections.Generic.List<DocumentData> documentData;
   
   /// <summary>
   /// Property for collection of DocumentData
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<DocumentData> DocumentData
   {
      get
      {
         if (documentData == null)
            documentData = new System.Collections.Generic.List<DocumentData>();
         return documentData;
      }
      set
      {
         RemoveAllDocumentData();
         if (value != null)
         {
            foreach (DocumentData oDocumentData in value)
               AddDocumentData(oDocumentData);
         }
      }
   }
   
   /// <summary>
   /// Add a new DocumentData in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddDocumentData(DocumentData newDocumentData)
   {
      if (newDocumentData == null)
         return;
      if (this.documentData == null)
         this.documentData = new System.Collections.Generic.List<DocumentData>();
      if (!this.documentData.Contains(newDocumentData))
         this.documentData.Add(newDocumentData);
   }
   
   /// <summary>
   /// Remove an existing DocumentData from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveDocumentData(DocumentData oldDocumentData)
   {
      if (oldDocumentData == null)
         return;
      if (this.documentData != null)
         if (this.documentData.Contains(oldDocumentData))
            this.documentData.Remove(oldDocumentData);
   }
   
   /// <summary>
   /// Remove all instances of DocumentData from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllDocumentData()
   {
      if (documentData != null)
         documentData.Clear();
   }
   
   public string[] GetColumnNames()
   {
      return new string[] { "Формат", "Обозначение", "Наименование", "Код_документа", "Примечание" };
   }
   
   public string GetDefaultValue()
   {
      return "1";
   }

}