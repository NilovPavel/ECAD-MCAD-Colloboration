using System;
using System.Collections.Generic;
using System.Linq;

internal class PerechenGroup : Spisok
{
    private List<RecordESKD> consolidateElements;
    private IEqualityComparer<RecordESKD> iEqualityComparer;
    private IComparer<string> iDesignatorComparer;
    private List<string> allDesignators;
    private int designatorsCount;

    private void Intiaizliation()
    {
        this.consolidateElements = new List<RecordESKD>();
        this.iEqualityComparer = new RecordESKDOthersEqualityComparer();
        this.iDesignatorComparer = new PerechenDesignatorComparer();
        this.allDesignators = new List<string>();
    }

    private RecordESKD ConsolidateElement(string firstDesignator, string secondDesignator, List<RecordESKD> unitRecords)
    {
        RecordESKD firstRecordESKD = unitRecords.Where(item => item.Designator.Split(',').Contains(firstDesignator)).First();
        RecordESKD secondRecordESKD = secondDesignator == null ? null : unitRecords.Where(item => item.Designator.Split(',').Contains(secondDesignator)).First();

        RecordESKD insertRecordESKD = (RecordESKD) firstRecordESKD.Clone();
        insertRecordESKD.Designator = firstDesignator.Trim();

        if (secondRecordESKD == null)
            return insertRecordESKD;

        if (firstDesignator.Contains(",") || firstDesignator.Contains("-") 
            || secondDesignator.Contains(",") || secondDesignator.Contains("-"))
            return insertRecordESKD;
        
        if(this.iEqualityComparer.Equals(firstRecordESKD, secondRecordESKD))
        {
            insertRecordESKD.Designator +="," + secondDesignator;
            this.designatorsCount++;
        }
        return insertRecordESKD;
    }

    private void Consolidate()
    {
        List<RecordESKD> unitRecords = this.Elements.ToList();//(new UnitRecordRazdelSpCollection(this.Name, this.Elements, this.iEqualityComparer)).GetComparableCollection().ToList();
        unitRecords.ForEach(item => this.allDesignators.AddRange(item.Designator.Split(',')));
        this.allDesignators.Sort(this.iDesignatorComparer);
        this.allDesignators = this.allDesignators.Distinct().ToList();

        for (designatorsCount = 0; designatorsCount < this.allDesignators.Count; designatorsCount++)
        {
            string firstDesignator = this.allDesignators[designatorsCount];
            string secondDesignator = designatorsCount != this.allDesignators.Count-1 ? this.allDesignators[designatorsCount+1] : null;

            RecordESKD inserRecordESKD = this.ConsolidateElement(firstDesignator, secondDesignator, unitRecords);
            inserRecordESKD.Количество = this.GetCountFromDesignator(inserRecordESKD.Designator);

            this.consolidateElements.Add(inserRecordESKD);
        }
    }

    private double GetCountFromDesignator(string designator)
    {
        if (!designator.Contains("-") && !designator.Contains(","))
            return 1;

        if (designator.Contains("-"))
        {
            string[] designators = designator.Split('-');
            return Designator.GetIndexFromDesignator(designators.Last()) - Designator.GetIndexFromDesignator(designators.First()) + 1;
        }

        if (designator.Contains(","))
            return 2;

        return 0;
    }

    public PerechenGroup(string name, RecordESKD[] elements) : base(name, elements)
    {
        this.Intiaizliation();
        this.Consolidate();
        this.Elements = this.consolidateElements.ToArray();
    }
}