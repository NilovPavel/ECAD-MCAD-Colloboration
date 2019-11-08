// File:    ICable.cs
// Author:  nilov_pg
// Created: 26 августа 2019 г. 15:54:04
// Purpose: Definition of Interface ICable

using System;

public interface ICable : INet
{
   IWire[] GetIWires();

}