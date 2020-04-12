// File:    PaintNet.cs
// Author:  Павел
// Created: 29 марта 2020 г. 22:04:25
// Purpose: Definition of Class PaintNet

using System;

public class PaintNet
{
   private IPaintNet iPaintNet;
   
   public double width;
   
   public Contour contour;

    public PaintNet()
    { }

    public PaintNet(IPaintNet iPaintNet)
    {
        this.iPaintNet = iPaintNet;
        this.width = this.iPaintNet.GetWidth();
        this.contour = new Contour(this.iPaintNet.GetIContour());
    }
}