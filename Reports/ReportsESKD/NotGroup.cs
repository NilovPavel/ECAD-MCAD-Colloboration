// File:    NotGroup.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:22:39
// Purpose: Definition of Class NotGroup

using System;
using System.Collections.Generic;

public class NotGroup : IGroup
{
   public Spisok[] Group(RecordESKD[] groupCollection)
   {
      List<Spisok> razdels = new List<Spisok>();
      return razdels.ToArray();
   }

}