// File:    RecordESKDFullEqualityComparer.cs
// Author:  nilov_pg
// Created: 28 июня 2019 г. 11:32:29
// Purpose: Definition of Class RecordESKDFullEqualityComparer

using System;
using System.Collections.Generic;

public class RecordESKDFullEqualityComparer : IEqualityComparer<RecordESKD>
{
   public bool Equals(RecordESKD x, RecordESKD y)
   {
      return x.Designator.Equals(y.Designator)
               && x.CadID.Equals(y.CadID)
               && x.Fitted == y.Fitted
               && x.PartNumber.Equals(y.PartNumber)
               && x.Наименование.Equals(y.Наименование)
               && x.Обозначение.Equals(y.Обозначение)
               && x.РазделСп.Equals(y.РазделСп);
   }
   
   public int GetHashCode(RecordESKD obj)
   {
      return 0;
   }

}