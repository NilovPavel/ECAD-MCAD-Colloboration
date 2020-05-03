// File:    Polygon.cs
// Author:  Павел
// Created: 29 марта 2020 г. 22:02:40
// Purpose: Definition of Class Polygon

using System;

public class Polygon
{
   private IPolygon iPolygon;
   
   public Contour contour;
   public Hatch hatch;
   
   public Polygon()
   {
   }
   
   public Polygon(IPolygon iPolygon)
   {
       this.iPolygon = iPolygon;
   }

}