// File:    IteratorSpisok.cs
// Author:  nilov_pg
// Created: 5 августа 2019 г. 12:09:05
// Purpose: Definition of Class IteratorSpisok

using System;

public class IteratorSpisok
{
    private Spisok spisok;

    private IIteratorAction iIteratorAction;

    private void IterateChildSpisok(Spisok razdel)
    {
        this.iIteratorAction.RazdelAction(razdel);
        if (razdel.Elements != null)
            this.IterateElements(razdel.Elements);
        foreach (Spisok childSpisok in razdel.Razdels)
            this.IterateChildSpisok(childSpisok);
    }

    private void IterateElements(RecordESKD[] elements)
    {
        foreach (RecordESKD recordESKD in elements)
            this.iIteratorAction.ElementAction(recordESKD);
    }

    public void SetIteratorAction(IIteratorAction iIteratorAction)
    {
        this.iIteratorAction = iIteratorAction;
    }

    public void Iteration()
    {
        this.IterateChildSpisok(spisok);
    }

    public IteratorSpisok(Spisok spisok)
    {
        this.spisok = spisok;
    }

}