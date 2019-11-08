// File:    OtherElementsGroup.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:24:36
// Purpose: Definition of Class OtherElementsGroup

using System;
using System.Collections.Generic;
using System.Linq;

public class OtherElementsGroup : IGroup
{
   private DeviceCatalog deviceCatalog;
   
   private void Initialization()
   {
      this.deviceCatalog = new DeviceCatalog();
   }
   
   private void AddUnknownDevices(RecordESKD[] groupCollection)
   {
      string[] designators = groupCollection.Select(item => Designator.GetSymbolsFromDesignator(item.Designator)).Distinct().Where(item => this.deviceCatalog.GetDeviceByDesinator(item) == null).ToArray();
      if (designators.Length == 0)
         return;
      foreach (string designator in designators)
         this.deviceCatalog.AddElement(designator, designator);
      this.deviceCatalog.Sort();
   }
   
   public Spisok[] Group(RecordESKD[] groupCollection)
   {
      List<Spisok> razdels = new List<Spisok>();
      this.AddUnknownDevices(groupCollection);
      string[] designators = this.deviceCatalog.Designators;
      for (int oneDesignator = 0; oneDesignator < designators.Length; oneDesignator++)
      {
         string currentPerechenRazdelName = this.deviceCatalog.GetDeviceByDesinator(designators[oneDesignator]);
         RecordESKD[] currentPerechenRazdelElements = groupCollection.Where(element => Designator.GetSymbolsFromDesignator(element.Designator).Equals(designators[oneDesignator])).ToArray();
         if(currentPerechenRazdelElements.Length > 0)
            razdels.Add(new SpecificationRazdelPerechen(currentPerechenRazdelName, currentPerechenRazdelElements));
      }
      return razdels.ToArray();
   }
   
   public OtherElementsGroup()
   {
      this.Initialization();
   }

}