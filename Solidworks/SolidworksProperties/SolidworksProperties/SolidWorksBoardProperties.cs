using System;
using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Linq;
using FlexBoardSolidworks;

namespace SolidworksBoardSpace
{
    public class SolidWorksBoardProperties : IProjectPropertiesCAD
    {
        private ModelDoc2 modelDoc;
        private SolidworksModelProperties modelProperties;
        private Dictionary<string, string> dictionary;

        string[] IProjectPropertiesCAD.GetPropertieNames()
        {
            return this.dictionary.Values.ToArray();
        }

        void IProjectPropertiesCAD.ReadProperties()
        {
            throw new NotImplementedException();
        }

        string IProjectPropertiesCAD.GetParameterValue(string parameterName)
        {
            throw new NotImplementedException();
        }

        void IProjectPropertiesCAD.WriteProperties()
        {
            throw new NotImplementedException();
        }

        string GetCorrectParameterName(string parameterName)
        {
            string newParameterName;
            this.dictionary.TryGetValue(parameterName, out newParameterName);
            return newParameterName;
        }

        void IProjectPropertiesCAD.WriteValue(string parameterName, string parameterValue)
        {
            string correctParameterName = this.GetCorrectParameterName(parameterName);
            this.modelProperties.AddPropertieValue(correctParameterName, parameterValue);
        }

        private void Initialization()
        {
            this.modelProperties = new SolidworksModelProperties(this.modelDoc);
            this.dictionary = new Dictionary<string, string>()
            {
                { "Перв.Примен.", "Перв.Примен."},
                { "Проектвходимость ","Проект" },
                { "Формат_PCB", "Формат" },
                { "Обозначение_PCB", "Обозначение"},
                { "Наименование_PCB", "Наименование"},
                { "Раздел_PCB", "Раздел"},
                { "п_Разраб", "п_Разраб"},
                { "п_Пров", "п_Пров"},
                { "п_Н_контр", "п_Н_контр"},
                { "п_Т_контр", "п_Т_контр"},
                { "п_Утв", "п_Утв"},
            };
        }

        public SolidWorksBoardProperties(ModelDoc2 modelDoc)
        {
            this.modelDoc = modelDoc;
            this.Initialization();
        }
    }
}