using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class SpecificationRazdelCombiner
{
    protected IEqualityComparer<RecordESKD> iEqualityComparer;
    protected RecordESKD[] sourceComponentCollection;

    public RecordESKD[] ComponentCollection
    {
        get
        {
            return this.GetComparableCollection();
        }
    }

    private int GetIndexFromDesignator(string designator)
    {
        Regex regEx = new Regex("[^\\d+]");
        designator = regEx.Replace(designator, string.Empty);
        if (string.IsNullOrEmpty(designator))
            return 0;
        return int.Parse(designator);
    }

    private string GetBaseFromDesignators(string[] designators)
    {
        string designator = string.Empty;
        string firstDesignator = designators.FirstOrDefault();
        Regex regEx = new Regex("\\d+");
        designator = regEx.Replace(firstDesignator, string.Empty);
        return designator;
    }

    private string GetDesignatorUnionString(string[] designators)
    {
        string baseString = this.GetBaseFromDesignators(designators);
        int[] designatorIndexes = designators.Where(item => !string.IsNullOrEmpty(item)).Select(item => this.GetIndexFromDesignator(item)).OrderBy(item => item).ToArray();
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

    protected virtual RecordESKD GetRecordESKD(RecordESKD recordESKD, IEnumerable<RecordESKD> records)
    {
        recordESKD.Количество = records.Count();
        return recordESKD;
    }

    private RecordESKD[] GetComparableCollection()
    {
        List<RecordESKD> unitCollection = new List<RecordESKD>();
        List<RecordESKD> distinctCollection = this.sourceComponentCollection.Distinct(this.iEqualityComparer).ToList();
        for (int elementCount = 0; elementCount < distinctCollection.Count; elementCount++)
        {
            IEnumerable<RecordESKD> currentDistinctCollection = this.sourceComponentCollection.Where(element => this.iEqualityComparer.Equals(element, distinctCollection[elementCount]));
            string[] designators = currentDistinctCollection.Select(item => item.Designator).ToArray();
            distinctCollection[elementCount].Designator = designators.Length == 1 ? designators.First() : this.GetDesignatorUnionString(designators);
            distinctCollection[elementCount] = this.GetRecordESKD(distinctCollection[elementCount], currentDistinctCollection);
            distinctCollection[elementCount].CadID = string.Join("$", currentDistinctCollection.Select(item => item.CadID));
        }
        return distinctCollection.ToArray();
    }

    public SpecificationRazdelCombiner(RecordESKD[] sourceComponentCollection)
    {
        this.sourceComponentCollection = sourceComponentCollection;
    }
}