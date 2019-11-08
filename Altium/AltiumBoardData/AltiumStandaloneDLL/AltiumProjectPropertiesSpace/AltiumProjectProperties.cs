using EDP;
using System.Collections.Generic;

namespace IProjectProperties
{
    class AltiumProjectProperties : IProjectPropertiesCAD
    {
        private IProject project;
        private Dictionary<string, string> parameters;

        public string[] GetPropertieNames()
        {
            return new string[]  {"Вид_документа",
                                                "Вид_документа_PCB",
                                                "Вид_документа_SCH",
                                                "Дата_изменения",
                                                "Дата_изменения_ВП",
                                                "Дата_изменения_ПЭ3",
                                                "Дата_изменения_СП",
                                                "Код_документа",
                                                "Код_документа_PCB",
                                                "Код_документа_SCH",
                                                "Литера",
                                                "Литера2",
                                                "Литера3",
                                                "Наименование",
                                                "Наименование1",
                                                "Наименование2",
                                                "Наименование3",
                                                "Наименование_PCB",
                                                "Номер_документа_изменение",
                                                "Номер_документа_изменение_ВП",
                                                "Номер_документа_изменение_ПЭ3",
                                                "Номер_документа_изменение_СП",
                                                "Обозначение",
                                                "Обозначение_PCB",
                                                "Обозначение_Код",
                                                "Перв.Примен.",
                                                "ПервПримен",
                                                "Порядковый_номер_изменения",
                                                "Порядковый_номер_изменения_ВП",
                                                "Порядковый_номер_изменения_ПЭ3",
                                                "Порядковый_номер_изменения_СП",
                                                "Проектвходимость ",
                                                "Раздел",
                                                "Раздел_PCB",
                                                "Раздел_SCH",
                                                "Указания_изменение",
                                                "Указания_изменение_ВП",
                                                "Указания_изменение_ПЭ3",
                                                "Указания_изменение_СП",
                                                "Формат_PCB",
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

        private void Initialization()
        {
            this.parameters = new Dictionary<string, string>();
        }

        void IProjectPropertiesCAD.ReadProperties()
        {
            for (int i = 0; i < this.project.DM_ParameterCount(); i++)
                if (!this.parameters.ContainsKey(project.DM_Parameters(i).DM_Name()))
                    this.parameters.Add(project.DM_Parameters(i).DM_Name(), project.DM_Parameters(i).DM_Value());
        }

        string IProjectPropertiesCAD.GetParameterValue(string parameterName)
        {
            //Косяк при получении, если не существует имя параметра - исправить!!!
            if (!this.parameters.ContainsKey(parameterName))
                return string.Empty;
            return parameters[parameterName];
        }

        void IProjectPropertiesCAD.WriteProperties()
        {
            //throw new NotImplementedException();
        }

        void IProjectPropertiesCAD.WriteValue(string parameterName, string parameterValue)
        {
            //throw new NotImplementedException();
        }

        public AltiumProjectProperties(IProject project)
        {
            this.project = project;
            this.Initialization();
        }
    }
}
