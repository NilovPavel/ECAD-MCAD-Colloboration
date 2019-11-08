// File:    Specification.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 15:59:48
// Purpose: Definition of Class Specification

using System;
using System.Collections.Generic;
using System.Linq;

public class Specification : Spisok
{
   private IIteratorAction iteratorPosition;
   
   private void CreateConfigurations()
   {
      if (this.Razdels.Count == 1)
         return;
   
      List<RecordESKD> generalVariantCollection = this.Razdels.First().Elements.ToList();
      for(int variantCount = 1; variantCount < this.Razdels.Count; variantCount++)
         generalVariantCollection = generalVariantCollection.Intersect(this.Razdels[variantCount].Elements, new RecordESKDFullEqualityComparer()).ToList();
   
      this.Razdels.Insert(0, new VariantESKD("General", generalVariantCollection.ToArray()));
   
      for (int variantCount = 1; variantCount < this.Razdels.Count; variantCount++)
         this.Razdels[variantCount].Elements = this.Razdels[variantCount].Elements.Except(generalVariantCollection, new RecordESKDFullEqualityComparer()).ToArray();
   }
   
   public Spisok GetVariableConfiguration()
   {
      IIteratorAction iteratorCollection = new IteratorCollection();
      List<RecordESKD> listVariablesConfigurations = new List<RecordESKD>();
      for (int specificationCount = 1; specificationCount < this.Razdels.Count; specificationCount++)
      {
         IteratorSpisok iteratorSpisok = new IteratorSpisok(this.Razdels[specificationCount]);
         iteratorSpisok.SetIteratorAction(iteratorCollection);
         iteratorSpisok.Iteration();
         listVariablesConfigurations.AddRange(((IteratorCollection)iteratorCollection).RecordESKD);
      }
      Spisok shareConfiguration = new SpecificationConfiguration("variableGeneral", listVariablesConfigurations.Distinct(new RecordESKDPositionEqualityComparer()).ToArray());
      return shareConfiguration;
   }
   
   public void WritePositionsToConfiguration(Spisok configuration)
   {
      IteratorSpisok iteratorSpisok = new IteratorSpisok(configuration);
      iteratorSpisok.SetIteratorAction(this.iteratorPosition);
      iteratorSpisok.Iteration();
   }
   
   public void WriteVariablePositionsToConfigurations(Spisok variableConfiguration)
   {
      IteratorSpisok iteratorSpisok = new IteratorSpisok(variableConfiguration);
      IIteratorAction iteratorCollection = new IteratorCollection();
      iteratorSpisok.SetIteratorAction(iteratorCollection);
      iteratorSpisok.Iteration();
   
      RecordESKD[] recordsVariableConfiguration = ((IteratorCollection)iteratorCollection).RecordESKD.ToArray();
   
      for (int specificationCount = 1; specificationCount < this.Razdels.Count; specificationCount++)
         ((SpecificationConfiguration)this.Razdels[specificationCount]).SetPositionNumbers(recordsVariableConfiguration);
   }
   
   public void WritePositions(IteratorPosition iteratorPosition)
   {
      this.iteratorPosition = iteratorPosition;
   
      Spisok firstConfiguration = this.Razdels.First();
      Spisok variableConfiguration = this.GetVariableConfiguration();
   
      this.WritePositionsToConfiguration(firstConfiguration);
      this.WritePositionsToConfiguration(variableConfiguration);
   
      this.WriteVariablePositionsToConfigurations(variableConfiguration);
   }
   
   public Specification(string specificationName, VariantESKD[] variants)
   : base(specificationName, variants)
   {
      this.CreateConfigurations();
      for (int razdelCount = 0; razdelCount < this.Razdels.Count; razdelCount++)
         this.Razdels[razdelCount] = new SpecificationConfiguration(this.Razdels[razdelCount].Name, this.Razdels[razdelCount].Elements);
   }

}