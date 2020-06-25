// File:    Perechen.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 15:59:49
// Purpose: Definition of Class Perechen

using System;
using System.Collections.Generic;
using System.Linq;

public class Perechen : Spisok
{
    private Hierarchy hierarchy;

    private void CreateConfigurations()
    {
        if (this.Razdels.Count == 1)
            return;

        List<RecordESKD> generalVariantCollection = this.Razdels.First().Elements.ToList();
        for (int variantCount = 1; variantCount < this.Razdels.Count; variantCount++)
            generalVariantCollection = generalVariantCollection.Intersect(this.Razdels[variantCount].Elements, new RecordESKDFullEqualityComparer()).ToList();

        this.Razdels.Insert(0, new VariantESKD("General", generalVariantCollection.ToArray()));

        for (int variantCount = 1; variantCount < this.Razdels.Count; variantCount++)
            this.Razdels[variantCount].Elements = this.Razdels[variantCount].Elements.Except(generalVariantCollection, new RecordESKDFullEqualityComparer()).ToArray();
    }

    public Perechen(string name, Hierarchy hierarchy, VariantESKD[] variantsESKD) : base(name, variantsESKD)
    {
        this.hierarchy = hierarchy;
        this.CreateConfigurations();
        for (int razdelCount = 0; razdelCount < this.Razdels.Count; razdelCount++)
            this.Razdels[razdelCount] = new PerechenConfiguration(this.Razdels[razdelCount].Name, this.Razdels[razdelCount].Elements, this.hierarchy.assemblyComponentID);
    }
}