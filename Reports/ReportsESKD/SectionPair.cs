// File:    SectionPair.cs
// Author:  nilov_pg
// Created: 12 июля 2019 г. 17:23:33
// Purpose: Definition of Class SectionPair

using System;

public class SectionPair
{
   private string name;
   private string value;
   
   public string Name
   {
      get
      {
         return name;
      }
      set
      {
         this.name = value;
      }
   }
   
   public string Value
   {
      get
      {
         return value;
      }
      set
      {
         this.value = value;
      }
   }
   
   public SectionPair(string name, string value)
   {
      this.name = name;
      this.value = value;
   }

}