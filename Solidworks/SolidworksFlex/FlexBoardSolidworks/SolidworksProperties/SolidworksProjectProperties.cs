using System;
using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using FlexBoardSolidworks;

namespace IAssembly
{
    class SolidworksProjectProperties : IProjectPropertiesCAD
    {
        private ModelDoc2 modelDoc;
        private SolidworksModelProperties modelProperties;

        public SolidworksProjectProperties(ModelDoc2 modelDoc)
        {
            this.modelDoc = modelDoc;
            this.Initialization();
        }

        private void Initialization()
        {
            this.modelProperties = new SolidworksModelProperties(this.modelDoc);
        }

        string IProjectPropertiesCAD.GetParameterValue(string parameterName)
        {
            return this.modelProperties.GetPropertieValue(parameterName) ;
        }

        string[] IProjectPropertiesCAD.GetPropertieNames()
        {
            return new string[]  {"Вид_документа",
                                                "Дата_изменения",
                                                "Дата_изменения_ВП",
                                                "Дата_изменения_ПЭ3",
                                                "Дата_изменения_СП",
                                                "Код_документа",
                                                "Литера",
                                                "Литера2",
                                                "Литера3",
                                                "Наименование",
                                                "Номер_документа_изменение",
                                                "Номер_документа_изменение_ВП",
                                                "Номер_документа_изменение_ПЭ3",
                                                "Номер_документа_изменение_СП",
                                                "Обозначение",
                                                "Перв.Примен.",
                                                "ПервПримен",
                                                "Порядковый_номер_изменения",
                                                "Порядковый_номер_изменения_ВП",
                                                "Порядковый_номер_изменения_ПЭ3",
                                                "Порядковый_номер_изменения_СП",
                                                "Проектвходимость ",
                                                "Раздел",
                                                "Указания_изменение",
                                                "Указания_изменение_ВП",
                                                "Указания_изменение_ПЭ3",
                                                "Указания_изменение_СП",
                                                "Характер_работы",
                                                "п_Доп_графа",
                                                "п_Доп_графа_Р",
                                                "п_Н_контр",
                                                "п_Н_контр_Р",
                                                "п_Пров",
                                                "п_Пров_Р",
                                                "п_Разраб",
                                                "п_Разраб_Р",
                                                "п_Т_Контр",
                                                "п_Утв",
                                                "п_Утв_Р" };
        }

        void IProjectPropertiesCAD.ReadProperties()
        {
            throw new NotImplementedException();
        }

        void IProjectPropertiesCAD.WriteProperties()
        {
            throw new NotImplementedException();
        }

        void IProjectPropertiesCAD.WriteValue(string parameterName, string parameterValue)
        {
            this.modelProperties.AddPropertieValue(parameterName, parameterValue);
        }
    }
}