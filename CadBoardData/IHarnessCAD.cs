// File:    IHarnessCAD.cs
// Author:  nilov_pg
// Created: 26 августа 2019 г. 15:49:41
// Purpose: Definition of Interface IHarnessCAD

using System;

public interface IHarnessCAD
{
   IWire[] GetIWires();
   
   ICable[] GetICables();

}