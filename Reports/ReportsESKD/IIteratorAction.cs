// File:    IIteratorAction.cs
// Author:  nilov_pg
// Created: 5 августа 2019 г. 12:10:09
// Purpose: Definition of Interface IIteratorAction

using System;

public interface IIteratorAction
{
   void ElementAction(RecordESKD recordESKD);
   
   void RazdelAction(Spisok spisok);
}