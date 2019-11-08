// File:    MaterialSort.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:28:17
// Purpose: Definition of Class MaterialSort

using System;

public class MaterialSort : ISort
{
   public RecordESKD[] Sort(RecordESKD[] componentCollection)
   {
        /*for (int elementCount = 0; elementCount < componentCollection.Length; elementCount++)
        {
            MaterialElement currentElement = new MaterialElement(componentCollection[elementCount].Наименование);
            this.AddSortedStandartElements(currentElement);
        }
        this.sortedStandartElements.Sort(new StandartElementComparer());
        RecordESKD[] elements = (from elementStandart in this.sortedStandartElements
                                 join element in componentCollection
                                 on elementStandart.FullName equals element.Наименование
                                 select element).Distinct().ToArray();*/
        return componentCollection;
    }

}