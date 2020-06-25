// File:    PerechenConfigurationGroup.cs
// Author:  nilov_pg
// Created: 7 июня 2019 г. 16:23:18
// Purpose: Definition of Class PerechenConfigurationGroup

using ReportsESKD;
using System;
using System.Collections.Generic;
using System.Linq;

public class PerechenConfigurationGroup : IGroup
{
    private List<Spisok> razdels;
    private DeviceCatalog deviceCatalog;

    private void AddUnknownDevices(RecordESKD[] groupCollection)
    {
        foreach (RecordESKD element in groupCollection)
        {
            string tempGroupName = this.deviceCatalog.GetDeviceByDesinator(Designator.GetSymbolsFromDesignator(element.Designator));
            deviceCatalog.AddElement(tempGroupName, tempGroupName);
        }

        this.deviceCatalog.Sort();
    }

    public Spisok[] Group(RecordESKD[] groupCollection)
    {
        this.AddUnknownDevices(groupCollection);

        foreach (string designator in this.deviceCatalog.Designators)
        {
            string currentRazdelName = this.deviceCatalog.GetDeviceByDesinator(designator);
            List<RecordESKD> currentCollection = groupCollection.Where(item => Designator.GetSymbolsFromDesignator(item.Designator).Equals(designator)).ToList();//.FindAll(x => dc.GetDeviceDyDesinator(Constants.design_from_text(x[Array.IndexOf(XMLConstants.tags_attributes, "DM_PhysicalDesignator")])).Equals(current_design_razdel_name));

            //PerechenGroup perechenGroup = new PerechenGroup(this.deviceCatalog.GetDeviceByDesinator(currentRazdelName), currentCollection.ToArray());

            //currentCollection = new UnitRecordPerechenCollection(groupCollection).GetComparableCollection().ToList();

            if (currentCollection.Count > 0)
                razdels.Add(new PerechenGroup(this.deviceCatalog.GetDeviceByDesinator(currentRazdelName), currentCollection.ToArray()));
        }

        return this.razdels.ToArray();
    }

    public PerechenConfigurationGroup()
    {
        this.Initialization();
    }

    private void Initialization()
    {
        this.deviceCatalog = new DeviceCatalog();
        this.razdels = new List<Spisok>();
    }
}