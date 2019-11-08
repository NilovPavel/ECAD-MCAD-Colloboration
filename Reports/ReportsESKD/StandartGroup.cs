// File:    StandartGroup.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:23:53
// Purpose: Definition of Class StandartGroup

using System;
using System.Collections.Generic;
using System.Linq;

public class StandartGroup : IGroup
{
   private IEqualityComparer<StandartElement> equalityComparer;
   
   private void Initialization()
   {
      this.equalityComparer = new StandartElementEqualityComparer();
   }
   
   private string[] GetGroupElementsNames(string standartElementName, List<StandartElement> standartElements)
   {
      StandartElement standartElement = new StandartElement(standartElementName);
      return standartElements.Where(item => this.equalityComparer.Equals(item, standartElement)).Select(item => item.FullName).ToArray();
   }
   
   public Spisok[] Group(RecordESKD[] groupCollection)
   {
      List<SpecificationRazdelStandartGroup> standartsGroup = new List<SpecificationRazdelStandartGroup>();
      List<StandartElement> standartElements = new List<StandartElement>();
      Array.ForEach(groupCollection, item => standartElements.Add(new StandartElement(item.Наименование)));
      string[] standartGroupsNames = standartElements.Distinct(new StandartElementEqualityComparer()).Select(item => item.FullName).ToArray();
      foreach (string standartGroupName in standartGroupsNames)
      {
         string[] groupElementNames = this.GetGroupElementsNames(standartGroupName, standartElements);
         RecordESKD[] elementsGroup = groupCollection.Where(item => groupElementNames.Contains(item.Наименование)).ToArray();
         standartsGroup.Add(new SpecificationRazdelStandartGroup(standartGroupName, elementsGroup));
      }
      standartsGroup.Sort(new StandartGroupComparer());
      return standartsGroup.ToArray();
   }
   
   public StandartGroup()
   {
      this.Initialization();
   }

}