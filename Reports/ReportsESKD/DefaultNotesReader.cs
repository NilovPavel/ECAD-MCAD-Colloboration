// File:    DefaultNotesReader.cs
// Author:  nilov_pg
// Created: 12 июля 2019 г. 17:20:16
// Purpose: Definition of Class DefaultNotesReader

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

public class DefaultNotesReader
{
   private string razdelSp;
   private string filePath;
   private string[] cods;
   
   private List<SectionPair> sectionPair;
   
   /// <summary>
   /// Property for collection of SectionPair
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public List<SectionPair> SectionPair
   {
      get
      {
         if (sectionPair == null)
            sectionPair = new List<SectionPair>();
         return this.sectionPair;
      }
      set
      {
         RemoveAllSectionPair();
         if (value != null)
         {
            foreach (SectionPair oSectionPair in value)
               AddSectionPair(oSectionPair);
         }
      }
   }
   
   /// <summary>
   /// Add a new SectionPair in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddSectionPair(SectionPair newSectionPair)
   {
      if (newSectionPair == null)
         return;
      if (this.sectionPair == null)
         this.sectionPair = new List<SectionPair>();
      if (!this.sectionPair.Contains(newSectionPair))
         this.sectionPair.Add(newSectionPair);
   }
   
   /// <summary>
   /// Remove an existing SectionPair from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveSectionPair(SectionPair oldSectionPair)
   {
      if (oldSectionPair == null)
         return;
      if (this.sectionPair != null)
         if (this.sectionPair.Contains(oldSectionPair))
            this.sectionPair.Remove(oldSectionPair);
   }
   
   /// <summary>
   /// Remove all instances of SectionPair from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllSectionPair()
   {
      if (sectionPair != null)
         sectionPair.Clear();
   }
   
   private void Initialization()
   {
      this.filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + "DefaultValues.xml";
   }
   
   private void CalcSectionPairCollection()
   {
      string xmlString = File.ReadAllText(this.filePath, Encoding.UTF8);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xmlString);
   
      XmlNodeList nodesList = xmlDocument.SelectNodes("/root/" + razdelSp + "/items/item");
      for (int nodesCount = 0; nodesCount < nodesList.Count; nodesCount++)
      {
         XmlAttributeCollection attributeCollection = nodesList.Item(nodesCount).Attributes;
         this.AddSectionPair(new SectionPair(attributeCollection.GetNamedItem("name").Value, attributeCollection.GetNamedItem("value").Value));
      }
      this.cods = this.SectionPair.Distinct().Select(item => item.Value).ToArray();
   }
   
   public string[] Cods
   {
      get
      {
         return cods;
      }
   }
   
   public void AddCodes(string section, string key)
   {
      this.AddSectionPair(new SectionPair(section, key));
      this.SectionPair = this.SectionPair.Distinct().ToList();
      this.cods = this.SectionPair.Distinct().Select(item => item.Value).ToArray();
   }
   
   public DefaultNotesReader(string razdelSp)
   {
      this.razdelSp = razdelSp;
      this.Initialization();
      this.CalcSectionPairCollection();
   }

}