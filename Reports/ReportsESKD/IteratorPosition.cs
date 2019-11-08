// File:    IteratorPosition.cs
// Author:  nilov_pg
// Created: 9 августа 2019 г. 12:15:55
// Purpose: Definition of Class IteratorPosition

using System;

public class IteratorPosition : IIteratorAction
{
   private int currentPositionNumber;
   
   public void ElementAction(RecordESKD recordESKD)
   {
        switch (recordESKD.РазделСп)
        {
            case "Документация":
            case "Комплекты":
                return;
        }
      recordESKD.Позиция = ++this.currentPositionNumber;
   }
   
   public void RazdelAction(Spisok spisok)
   {
      return;
   }
   
   public IteratorPosition()
   {
      this.currentPositionNumber = 0;
   }

}