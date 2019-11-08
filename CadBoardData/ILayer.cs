// File:    ILayer.cs
// Author:  nilov_pg
// Created: 23 ноября 2018 г. 9:46:56
// Purpose: Definition of Interface ILayer

using System;

public interface ILayer
{
   double GetThickness();
   
   string LayerName();
   
   int LayerNumber();
   
   string GetTypeName();
   
   double GetOriginHeight();
   
   string GetUniqueID();

}