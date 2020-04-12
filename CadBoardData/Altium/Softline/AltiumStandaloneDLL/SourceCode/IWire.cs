// File:    IWire.cs
// Author:  nilov_pg
// Created: 26 августа 2019 г. 15:51:34
// Purpose: Definition of Interface IWire

using System;




public interface IWire : INet
{
   string GetSourceConnectorDesignator();
   
   string GetSourceConnectorPin();
   
   string GetDestinationConnectorDesignator();
   
   string GetDestinationConnectorPin();

}
