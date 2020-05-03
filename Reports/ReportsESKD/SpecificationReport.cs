// File:    SpecificationReport.cs
// Author:  nilov_pg
// Created: 19 июня 2019 г. 15:33:39
// Purpose: Definition of Class SpecificationReport

using System;
using System.Collections.Generic;
using System.Linq;

public class SpecificationReport : AbstractReport
{
   private VariantESKD[] variants;
   private VariantESKD[] variantsEM;
   
   private IIteratorAction iteratorPosition;
   
   private void Initialization()
   {
      this.variants = new VariantESKD[this.assembly.variantCAD.variant.Length];
      this.variantsEM = new VariantESKD[this.assembly.variantCAD.variant.Length];
      //this.iCheck = new ProxyCheck();
      this.iteratorPosition = new IteratorPosition();
   }

    public int GetNumberByElement(string variantName, ComponentCAD componentCad)
    {
        Spisok specification = componentCad.dataESKD.SpecificationFlag == EskdSpecificationType.general ? 
                            this.Spisok.Where(item => item.Name.Equals("Спецификация")).FirstOrDefault() 
                            : this.Spisok.Where(item => item.Name.Equals("Спецификация МЭ")).FirstOrDefault();
        IIteratorAction iteratorSearchById = new IteratorSearchElement(componentCad.UniqueID);
        IteratorSpisok iteratorSpisok = new IteratorSpisok(specification);
        iteratorSpisok.SetIteratorAction(iteratorSearchById);
        iteratorSpisok.Iteration();
        RecordESKD recordESKD = ((IteratorSearchElement)iteratorSearchById).GetRecordESKD();
        return (recordESKD != null ? recordESKD.Позиция : 0);
    }

    private void CreateVariants()
   {
      for (int variantCount = 0; variantCount < this.assembly.variantCAD.variant.Length; variantCount++)
      {
         Variant currentVariant = this.assembly.variantCAD.variant[variantCount];
         RecordESKD[] collection = this.GetVariantCollection(currentVariant);
         this.variants[variantCount] = new VariantESKD(currentVariant.VariantName, collection);
         RecordESKD[] collectionEM = this.GetVariantCollectionEM(currentVariant);
         this.variantsEM[variantCount] = new VariantESKD(currentVariant.VariantName, collectionEM);
      }
   }

    private RecordESKD[] GetHarnessCollection()
    {
        List<RecordESKD> harnessCollection = new List<RecordESKD>();
        if (this.assembly.harness!=null)
        {
            this.assembly.harness.Cable.ForEach(item => harnessCollection.Add((new CableRazdelESKD(item)).RecordESKD));
            this.assembly.harness.Wire.ForEach(item => harnessCollection.Add((new WireRazdelESKD(item)).RecordESKD));
        }
        return harnessCollection.ToArray();
    }

   private RecordESKD[] GetNotesCollection(Variant variant)
   {
      int variantNumber = this.GetVariantNumber(variant);
      List<NotesRazdelESKD> razdelNotes = new List<NotesRazdelESKD>();
      Notes[] notes = this.assembly.notes.Where(itemNote => itemNote.RazdelName.IndexOf("ЭМ") == -1).ToArray();
      Array.ForEach(notes, itemNote => razdelNotes.Add(new NotesRazdelESKD(itemNote)));
   
      List<RecordESKD> recordsESKD = new List<RecordESKD>();
      razdelNotes.ForEach(item => recordsESKD.AddRange(item.GetRecordsESKD(variantNumber)));
   
      return recordsESKD.ToArray();
   }
   
   private RecordESKD[] GetNotesCollectionEM(Variant variant)
   {
      int variantNumber = this.GetVariantNumber(variant);
      List<NotesRazdelESKD> razdelNotesEM = new List<NotesRazdelESKD>();
      Notes[] notesEM = this.assembly.notes.Where(itemNote => itemNote.RazdelName.IndexOf("ЭМ") != -1).ToArray();
      Array.ForEach(notesEM, itemNote => itemNote.RazdelName = itemNote.RazdelName.Replace(" ЭМ", string.Empty));
      Array.ForEach(notesEM, itemNote => razdelNotesEM.Add(new NotesRazdelESKD(itemNote)));
   
      List<RecordESKD> recordsESKD = new List<RecordESKD>();
      razdelNotesEM.ForEach(item => item.GetRecordsESKD(variantNumber));
   
      return recordsESKD.ToArray();
   }
   
   private RecordESKD[] GetVariantCollection(Variant variant)
   {
      List<RecordESKD> components = new List<RecordESKD>();
      variant.ComponentCAD.Where(x => x.dataESKD.SpecificationFlag == EskdSpecificationType.general).ToList().ForEach(x => components.Add(new RecordESKD(x)));
      /*for (int componentCount = 0; componentCount < components.Count; componentCount++)
         components[componentCount] = this.iCheck.GetCheckingRecordESKD(components[componentCount]);*/
      RecordESKD[] notesRecordsESKD = this.GetNotesCollection(variant);
      components.AddRange(notesRecordsESKD);
      RecordESKD[] harnessCollection = this.GetHarnessCollection();
      components.AddRange(harnessCollection);
      return components.ToArray();
   }
   
   private RecordESKD[] GetVariantCollectionEM(Variant variant)
   {
      List<RecordESKD> components = new List<RecordESKD>();
      variant.ComponentCAD.Where(x => x.dataESKD.SpecificationFlag == EskdSpecificationType.em).ToList().ForEach(x => components.Add(new RecordESKD(x)));
      /*for (int componentCount = 0; componentCount < components.Count; componentCount++)
         components[componentCount] = this.iCheck.GetCheckingRecordESKD(components[componentCount]);*/
      RecordESKD[] notesRecordsESKD = this.GetNotesCollectionEM(variant);
      components.AddRange(notesRecordsESKD);
      return components.ToArray();
   }
   
   private void CreateSpecifications()
   {
         if (!this.IsBadVariants(this.variants))
            this.Spisok.Add(new Specification("Спецификация", this.variants));
         if (!this.IsBadVariants(this.variantsEM))
            this.Spisok.Add(new Specification("Спецификация МЭ", this.variantsEM));
   }
   
   public int GetVariantNumber(Variant variant)
   {
      return Array.IndexOf(this.assembly.variantCAD.variant, variant);
   }
   
   public void Numeration()
   {
      foreach (Spisok spisok in this.spisok)
         ((Specification)spisok).WritePositions((IteratorPosition)this.iteratorPosition);
   }

    /*private void WriteOldRazdel()
    {
        Spisok spisok = this.spisok.Where(item => item.Name.Equals("Спецификация МЭ")).FirstOrDefault();
        if (spisok == null)
            return;

        Array.ForEach(this.variantsEM, item => Array.ForEach(item.Elements, element => element.РазделСп = element.РазделСп + " ЭМ"));
    }*/
   
   public SpecificationReport(Assembly assembly)
   : base(assembly)
   {
      this.Initialization();
      this.CreateVariants();
      this.CreateSpecifications();
      if (this.Spisok.Count == 0)
         return;
      this.Numeration();
   }
}