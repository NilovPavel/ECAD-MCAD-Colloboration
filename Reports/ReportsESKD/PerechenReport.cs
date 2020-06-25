// File:    PerechenReport.cs
// Author:  nilov_pg
// Created: 19 июня 2019 г. 15:34:09
// Purpose: Definition of Class PerechenReport

using System;
using System.Collections.Generic;

public class PerechenReport : AbstractReport
{
    private Perechen root;
    private VariantESKD[] variantsESKD;

    private void Initialization()
    {
        this.variantsESKD = this.GetVariantsESKD();
    }

    private VariantESKD[] GetVariantsESKD()
    {
        VariantESKD[] variantsESKD = new VariantESKD[this.assembly.variantCAD.variant.Length];
        for (int variantCount = 0; variantCount < variantsESKD.Length; variantCount++)
            variantsESKD[variantCount] = this.GetVariantESKD(this.assembly.variantCAD.variant[variantCount]);
        return variantsESKD;
    }

    private VariantESKD GetVariantESKD(Variant variant)
    {
        List<RecordESKD> recordsESKD = new List<RecordESKD>();
        foreach (ComponentCAD componentCAD in variant.ComponentCAD)
        {
            RecordESKD recordESKD = new RecordESKD(componentCAD);
            recordsESKD.Add(recordESKD);
        }
        VariantESKD variantESKD = new VariantESKD(variant.VariantName, recordsESKD.ToArray());
        return variantESKD;
    }

    public PerechenReport(Assembly assembly)
   : base(assembly)
   {
        this.Initialization();
        //this.GetVariantsESKD();
        this.root = new Perechen("Перечень элементов", this.assembly.hierarchy, this.variantsESKD);
        this.AddSpisok(this.root);
   }

}