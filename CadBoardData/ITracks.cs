// File:    ITracks.cs
// Author:  Павел
// Created: 8 апреля 2020 г. 19:29:52
// Purpose: Definition of Interface ITracks

using System;

public interface ITracks
{
   int GetShapeEnds();
   
   double GetWidth();
   
   IContour GetIContour();

}