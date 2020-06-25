// File:    IteratorVariablePositions.cs
// Author:  nilov_pg
// Created: 9 августа 2019 г. 12:09:33
// Purpose: Definition of Class IteratorVariablePositions

using System;
using System.Collections.Generic;

public class IteratorVariablePositions : IIteratorAction
{
    private RecordESKD[] recordsESKD;
    private IEqualityComparer<RecordESKD> iEqualityComparer;

    public void ElementAction(RecordESKD recordESKD)
    {
        RecordESKD recordWithPosition = Array.Find(this.recordsESKD, item => this.iEqualityComparer.Equals(recordESKD, item));
        if (recordWithPosition == null)
        {
            recordESKD.Позиция = 0;
            return;
        }
        recordESKD.Позиция = recordWithPosition.Позиция;
    }

    public void RazdelAction(Spisok spisok)
    {
        return;
    }

    public IteratorVariablePositions(RecordESKD[] recordsESKD)
    {
        this.recordsESKD = recordsESKD;
        this.iEqualityComparer = (IEqualityComparer<RecordESKD>)new RecordESKDPositionEqualityComparer();
    }

}