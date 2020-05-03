// File:    INotesRazdelESKD.cs
// Author:  nilov_pg
// Created: 15 июля 2019 г. 15:54:26
// Purpose: Definition of Interface INotesRazdelESKD

using System;

public interface INotesRazdel
{
   string[] GetColumnNames();
   
   string GetDefaultValue();

}