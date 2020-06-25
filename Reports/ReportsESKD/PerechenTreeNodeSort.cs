using System;

internal class PerechenTreeNodeSort : ISort
{
    RecordESKD[] ISort.Sort(RecordESKD[] componentCollection)
    {
        Array.Sort(componentCollection, new RecordESKDDesignatorComparer());
        return componentCollection;
    }
}