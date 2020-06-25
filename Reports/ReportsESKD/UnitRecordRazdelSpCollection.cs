// File:    AbstractUnitRecordCollection.cs
// Author:  nilov_pg
// Created: 18 сентября 2019 г. 16:14:32
// Purpose: Definition of Interface AbstractUnitRecordCollection

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class UnitRecordRazdelSpCollection
{
    private string razdelSp;
    private RecordESKD[] componentCollection;
    private IEqualityComparer<RecordESKD> comparer;

    private void Initialization()
    {
    }

    

    private string GetDesignatorUnionString(string[] designators)
    {
        string baseString = Designator.GetBaseFromDesignators(designators);
        int[] designatorIndexes = designators.Where(item => !string.IsNullOrEmpty(item)).Select(item => Designator.GetIndexFromDesignator(item)).OrderBy(item => item).ToArray();
        string beutyString = string.Empty;
        for (int indexCount = 0; indexCount < designatorIndexes.Length; indexCount++)
        {
            int currentCount = 0;
            string firstDesignator = baseString + designatorIndexes[indexCount];
            while (indexCount + 1 < designatorIndexes.Length)
            {
                if (designatorIndexes[indexCount] != designatorIndexes[indexCount + 1] - 1)
                    break;
                indexCount++;
                currentCount++;
            }
            string secondDesignator = baseString + designatorIndexes[indexCount];
            beutyString += (currentCount >= 2 ? firstDesignator + "-" + secondDesignator : !firstDesignator.Equals(secondDesignator) ? firstDesignator + ", " + secondDesignator : secondDesignator) + ", ";
            currentCount = 0;
        }
        return beutyString.Trim(' ', ',');
    }

    public RecordESKD[] GetComparableCollection()
    {
        List<RecordESKD> unitCollection = new List<RecordESKD>();
        List<RecordESKD> distinctCollection = componentCollection.Distinct(this.comparer).ToList();
        for (int elementCount = 0; elementCount < distinctCollection.Count; elementCount++)
        {
            IEnumerable<RecordESKD> currentDistinctCollection = componentCollection.Where(element => this.comparer.Equals(element, distinctCollection[elementCount]));
            string[] designators = currentDistinctCollection.Select(item => item.Designator).ToArray();
            distinctCollection[elementCount].Designator = designators.Length == 1 ? designators.First() : this.GetDesignatorUnionString(designators);
            distinctCollection[elementCount].Количество = designators.Length;
            distinctCollection[elementCount].CadID = string.Join("$", currentDistinctCollection.Select(item => item.CadID));
        }
        return distinctCollection.ToArray();
    }

    public UnitRecordRazdelSpCollection(string razdelSp, RecordESKD[] componentCollection, IEqualityComparer<RecordESKD> comparer)
    {
        this.razdelSp = razdelSp;
        this.componentCollection = componentCollection;
        this.comparer = comparer;
        this.Initialization();
    }
}