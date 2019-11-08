// File:    IDataESKD.cs
// Author:  nilov_pg
// Created: 21 января 2019 г. 17:28:27
// Purpose: Definition of Interface IDataESKD

using System;

public interface IDataESKD
{
   string GetFormat();
   
   string GetObozn();
   
   string GetName();
   
   string GetPartNumber();
   
   string GetRazdelSP();
   
   string GetPrimechanie();
   
   EskdSpecificationType GetSpecificationType();
   
   void SetSpecificationType(EskdSpecificationType eskdSpecificationType);

}