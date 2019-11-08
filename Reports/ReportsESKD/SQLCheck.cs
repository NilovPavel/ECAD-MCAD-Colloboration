// File:    SQLCheck.cs
// Author:  nilov_pg
// Created: 18 июля 2019 г. 11:01:45
// Purpose: Definition of Class SQLCheck

using System;
using System.Data;
using System.Data.SqlClient;

public class SQLCheck : ICheck
{
   private string serverName;
   private int port;
   private string dataBaseName;
   private string tableName;
   private string userName;
   private string password;
   private DataTable dataTable;
   
   private void Initialization()
   {
      this.serverName = "e-pdm";
      this.port = 1433;
      this.userName = "Base";
      this.password = "base";
      this.dataBaseName = "AltiumDesigner";
      this.tableName = "AltiumDesigner";
      string urlSqlConnection = "server=tcp:" + this.serverName
               + ", " + this.port
               + ";Database=" + this.dataBaseName
               + ";User ID=" + this.userName
               + ";Password=" + this.password
               + ";" + "Connect Timeout=1";
      this.dataTable = new DataTable();
      this.ReadDataTable(urlSqlConnection);
   }
   
   private void ReadDataTable(string urlSqlConnection)
   {
      using (SqlConnection sqlConnection = new SqlConnection(urlSqlConnection))
      {
         sqlConnection.Open();
         SqlCommand command = new SqlCommand();
         command.CommandText = "SELECT * FROM " + this.tableName;
         command.CommandType = CommandType.Text;
         command.Connection = sqlConnection;
         SqlDataReader dataReader = command.ExecuteReader();
         this.dataTable.Load(dataReader);
      }
   }
   
   public RecordESKD GetCheckingRecordESKD(RecordESKD recordESKD)
   {
      DataRow[] dataRows = this.dataTable.Select("[Part Number]=" + "'" + recordESKD.PartNumber + "'");
      if (dataRows.Length != 0)
      {
         DataRow dataRow = dataRows[0];
         recordESKD.Наименование = dataRow.ItemArray[this.dataTable.Columns["Обозначение элемента"].Ordinal].ToString();
            recordESKD.Status = dataRow.ItemArray[this.dataTable.Columns["Status"].Ordinal].ToString();
         recordESKD.Наименование = string.IsNullOrEmpty(recordESKD.Наименование) ? recordESKD.PartNumber : recordESKD.Наименование;
      }
      return recordESKD;
   }
   
   public SQLCheck()
   {
      this.Initialization();
   }

}