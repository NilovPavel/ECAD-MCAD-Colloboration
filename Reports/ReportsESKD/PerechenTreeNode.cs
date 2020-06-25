using System;
using System.Collections.Generic;
using System.Linq;

public class PerechenTreeNode : Spisok
{
    private AssemblyComponentID rootAssemblyComponentID;
    private List<RecordESKD> elementsNode;
    private List<PerechenTreeNode> treeNode;

    private void Initialization()
    {
        this.elementsNode = new List<RecordESKD>();
        this.treeNode = new List<PerechenTreeNode>();
        this.iGroup = new PerechenConfigurationGroup();
        this.iSort = new PerechenTreeNodeSort();
    }

    private RecordESKD GetRecordFromAssemblyComponentID()
    {
        return new RecordESKD
        {
            Designator = this.rootAssemblyComponentID.Designator,
            Наименование = this.rootAssemblyComponentID.Наименование,
            Обозначение = this.rootAssemblyComponentID.Обозначение,
            Fitted = true,
        };
    }

    private void BuildCollection()
    {
        this.elementsNode.Add(new PerechenAssemblyRecordESKD(this.rootAssemblyComponentID));
        
        string[] elementaryUniqueIDs = this.rootAssemblyComponentID.ElementaryComponentID.Select(item => item.UniqueID).Distinct().ToArray();

        foreach (string elementaryUniqueID in elementaryUniqueIDs)
            this.elementsNode.AddRange(this.Elements.Where(item => item.CadID.Equals(elementaryUniqueID)));

        this.elementsNode = this.iSort.Sort(this.elementsNode.ToArray()).ToList();
        this.Razdels = this.iGroup.Group(this.elementsNode.ToArray()).ToList();

        foreach (AssemblyComponentID subAssembly in this.rootAssemblyComponentID.AssemblyComponentIDB)
            this.Razdels.Add(new PerechenTreeNode(subAssembly.Designator, this.Elements, subAssembly));

        this.Elements = new RecordESKD[0];
    }

    public PerechenTreeNode(string name, RecordESKD[] elements, AssemblyComponentID rootAssemblyComponentID) : base(name, elements)
    {
        this.rootAssemblyComponentID = rootAssemblyComponentID;
        this.Initialization();
        this.BuildCollection();
    }

    
}