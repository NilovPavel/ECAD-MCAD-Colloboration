using System;

public class ConsoleIteratorAction : IIteratorAction
{
    void IIteratorAction.Close()
    {
        return;
    }

    void IIteratorAction.ElementAction(RecordESKD recordESKD)
    {
        Console.WriteLine(recordESKD.Designator
             //+ recordESKD.Fitted
             + "\t" + recordESKD.Обозначение
             + "\t" + recordESKD.Наименование
             );
    }

    void IIteratorAction.RazdelAction(Spisok spisok)
    {
        Console.WriteLine(spisok.Name);
    }
}