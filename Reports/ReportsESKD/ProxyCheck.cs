// File:    ProxyCheck.cs
// Author:  nilov_pg
// Created: 18 июля 2019 г. 10:59:44
// Purpose: Definition of Class ProxyCheck

using System;

public class ProxyCheck : ICheck
{
   private ICheck iCheck;
   
   private void Initialization()
   {
      this.iCheck = new SQLCheck();
   }
   
   public RecordESKD GetCheckingRecordESKD(RecordESKD recordESKD)
   {
      return this.iCheck.GetCheckingRecordESKD(recordESKD);
   }
   
   public ProxyCheck()
   {
      this.Initialization();
   }

}