// File:    VariantCAD.cs
// Author:  nilov_pg
// Created: 20 декабря 2018 г. 10:36:44
// Purpose: Definition of Class VariantCAD

using System;

public class VariantCAD
{
   private IVariantCAD iVariantCAD;
   
   public Variant[] variant;
   
   public VariantCAD()
   {}
   
   public VariantCAD(IVariantCAD iVariantCAD)
   {
      this.iVariantCAD = iVariantCAD;
         IVariant[] ivariants = this.iVariantCAD.GetIVariants();
         this.variant = new Variant[ivariants.Length];
         for (int i = 0; i < this.variant.Length; i++)
            this.variant[i] = new Variant(ivariants[i]);
   }

}