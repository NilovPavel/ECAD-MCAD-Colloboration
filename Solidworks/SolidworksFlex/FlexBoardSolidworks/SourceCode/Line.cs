// File:    Line.cs
// Author:  nilov_pg
// Created: 12 сентября 2018 г. 15:07:12
// Purpose: Definition of Class Line

using System;
using System.Collections.Generic;

public class Line : IExtremum
{
   private Point point1;
   private Point point2;
   
   private ILine iLine;
   
   public Point Point1
   {
      get
      {
         return point1;
      }
      set
      {
         this.point1 = value;
      }
   }
   
   public Point Point2
   {
      get
      {
         return point2;
      }
      set
      {
         this.point2 = value;
      }
   }
   
   public Line()
   {
   }
   
   public Line(ILine iLine)
   {
      this.iLine = iLine;
      this.point1 = this.iLine.GetStartPoint();
      this.point2 = this.iLine.GetEndPoint();
   }
   
   public Point[] GetExtremums()
   {
      List<Point> points = new List<Point>();
           points.Add(this.point1);
           points.Add(this.point2);
           return points.ToArray();
   }

}