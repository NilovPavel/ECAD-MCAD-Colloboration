// File:    Eri.cs
// Author:  nilov_pg
// Created: 5 июля 2019 г. 16:47:16
// Purpose: Definition of Class Eri

using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class Eri
{
   private string fullEriName;
   private string edIzm;
   private string eriName;
   private double nominal;
   private double admission;
   private double voltage;
   private string body;
   private string standart;
   private string other;
   
   private void CalculateParameters()
   {
      this.eriName = this.GetEriName();
      string nominalWithEdIzm = this.GetNominalWithEdIzm();
      this.nominal = this.GetNominal(nominalWithEdIzm);
      
      string voltageString = this.GetVoltageString();
      this.voltage = this.GetDoubleFromString(voltageString);
      
      string admissionString = this.GetAdmissionString();
      this.admission = this.GetDoubleFromString(admissionString);
      
      this.standart = this.GetStandart();
      
      this.body = this.GetBody();
      
      this.other = this.GetOther();
   }
   
   private string GetEriName()
   {
      Regex regex = new Regex("^\\w+-*\\w+");
      Match match = regex.Match(this.fullEriName);
      return match.Value;
   }
   
   private double GetNominal(string nominalWithEdIzm)
   {
      Regex regex = new Regex("[\\d]+[,]?[\\d]*");
      Match match = regex.Match(nominalWithEdIzm);
   
      string nominalWithoutEdIzm = match.Success ? match.Value : "0";
      nominalWithoutEdIzm = nominalWithoutEdIzm.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator).Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
   
      string siWithEdIzm = nominalWithEdIzm.Replace(nominalWithoutEdIzm, string.Empty);
      int stepen = this.GetSiStepen(siWithEdIzm);
   
      return double.Parse(nominalWithoutEdIzm) * Math.Pow(10, stepen);
   }
   
   private string GetNominalWithEdIzm()
   {
      Regex regex = new Regex("[\\d]+[,]?[\\d]*" + "[\\D]*[" + this.edIzm + "]");
      Match match = regex.Match(this.fullEriName);
      string nominalWithEdIzm = match.Success ? match.Value : string.Empty;
      return nominalWithEdIzm;
   }
   
   private int GetSiStepen(string siWithEdIzm)
   {
      TableSI tableSi = new TableSI();
      string si = siWithEdIzm.Replace(this.edIzm, string.Empty);
      return tableSi.GetStepen(si.Trim());
   }
   
   private double GetVoltage(string voltageString)
   {
      Regex regex = new Regex("[\\d]+[,]?[\\d]*");
      Match match = regex.Match(voltageString);
   
      string voltageWithoutVolts = match.Success ? match.Value : string.Empty;
      voltageWithoutVolts = voltageWithoutVolts.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator).Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
      return string.IsNullOrEmpty(voltageWithoutVolts) ? 0 : double.Parse(voltageWithoutVolts);
   }
   
   private string GetVoltageString()
   {
      Regex regex = new Regex("[\\W]?[\\d]+[,]?[\\d]*[\\s]*[\\D]*[В][\\W]?");
      Match match = regex.Match(this.fullEriName);
      string voltageWithVolts = match.Success ? match.Value : string.Empty;
      return voltageWithVolts;
   }
   
   private int GetAdmission(string admissionString)
   {
      Regex regex = new Regex("[\\D]");
      string admission = regex.Replace(admissionString, string.Empty);
      return string.IsNullOrEmpty(admission) ? 0 : int.Parse(admission);
   }
   
   private string GetAdmissionString()
   {
      Regex regex = new Regex("[(±|+)][\\d]+[%]*");
      Match match = regex.Match(this.fullEriName);
   
      string admissionString = match.Success ? match.Value : string.Empty;
      return admissionString;
   }
   
   private string GetBody()
   {
      Regex regex = new Regex("[\\s]+[\\d]+[\\s]*");
      Match match = regex.Match(this.eriName);
   
      string stringStandart = match.Success ? match.Value : string.Empty;
      return stringStandart.Trim();
   }
   
   private string GetOther()
   {
      string[] splitString = Regex.Split(this.eriName, "\\s+");
      return splitString.Length > 0 ? splitString[splitString.Length - 1] : string.Empty;
   }
   
   private string GetStandart()
   {
      Regex regex = new Regex("[\\S]+[\\s]*ТУ+");
      Match match = regex.Match(this.fullEriName);
   
      string stringStandart = match.Success ? match.Value : string.Empty;
      return stringStandart;
   }
   
   private double GetDoubleFromString(string doubleValueInString)
   {
      Regex regex = new Regex("[\\d]+[,]?[\\d]*");
      Match match = regex.Match(doubleValueInString);
   
      string voltageWithoutVolts = match.Success ? match.Value : string.Empty;
      voltageWithoutVolts = voltageWithoutVolts.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator).Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
      return string.IsNullOrEmpty(voltageWithoutVolts) ? 0 : double.Parse(voltageWithoutVolts);
   }
   
   public string FullEriName
   {
      get
      {
         return fullEriName;
      }
   }
   
   public string EdIzm
   {
      get
      {
         return edIzm;
      }
   }
   
   public string EriName
   {
      get
      {
         return eriName;
      }
   }
   
   public double Nominal
   {
      get
      {
         return nominal;
      }
   }
   
   public double Admission
   {
      get
      {
         return admission;
      }
   }
   
   public double Voltage
   {
      get
      {
         return voltage;
      }
   }
   
   public string Body
   {
      get
      {
         return body;
      }
   }
   
   public string Other
   {
      get
      {
         return other;
      }
   }
   
   public string Standart
   {
      get
      {
         return standart;
      }
   }
   
   public Eri(string fullEriName, string edIzm)
   {
      this.fullEriName = fullEriName;
      this.edIzm = edIzm;
      this.CalculateParameters();
   }

}