// File:    PerechenConfiguration.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 15:59:51
// Purpose: Definition of Class PerechenConfiguration

using System;
using System.Collections.Generic;

public class PerechenConfiguration : Spisok
{
    private AssemblyComponentID rootAssemblyComponentID;
    private PerechenTreeNode perechenTreeNode;

    private void BuildTree()
    {
        this.perechenTreeNode = new PerechenTreeNode(string.Empty, this.Elements, this.rootAssemblyComponentID);
        this.AddRazdels(this.perechenTreeNode);
    }

    public PerechenConfiguration(string Name, RecordESKD[] elements, AssemblyComponentID rootAssemblyComponentID) : base(Name, elements)
    {
        this.rootAssemblyComponentID = rootAssemblyComponentID;
        this.BuildTree();
    }
}