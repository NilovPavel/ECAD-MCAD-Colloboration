using System;
using System.Linq;
using BoardSpace;
using EDP;
using IProjectProperties;
using PCB;
using System.Windows.Forms;

namespace IAssembly
{
    class AltiumBoardESKD : IDataESKD
    {
        private IProject project;
        private IProjectPropertiesCAD altiumProjectProperties;
        private IBoardCAD iBoard;

        public AltiumBoardESKD(IProject project, IBoardCAD iBoard)
        {
            this.project = project;
            this.iBoard = iBoard;
            this.Initialization();
        }

        private void Initialization()
        {
            this.altiumProjectProperties = new AltiumProjectProperties(this.project);
            this.altiumProjectProperties.ReadProperties();
        }

        string IDataESKD.GetFormat()
        {
            return this.altiumProjectProperties.GetParameterValue("Формат_PCB");
        }

        string IDataESKD.GetName()
        {
            return this.altiumProjectProperties.GetParameterValue("Наименование_PCB");
        }

        string IDataESKD.GetObozn()
        {
            return this.altiumProjectProperties.GetParameterValue("Обозначение_PCB");
        }

        string IDataESKD.GetPartNumber()
        {
            return string.Empty;
        }

        string IDataESKD.GetPrimechanie()
        {
            return string.Empty;
        }

        string IDataESKD.GetRazdelSP()
        {
            AltiumBoard altiumBoard = (AltiumBoard)this.iBoard;
            int electricalLayersCount = altiumBoard.AltiumLayerStackManager.Layers.Count(layer => layer.IsElectrical);
            return electricalLayersCount > 2 ? "Сборочные единицы" : "Детали";
        }

        EskdSpecificationType IDataESKD.GetSpecificationType()
        {
            return EskdSpecificationType.general;
        }

        void IDataESKD.SetSpecificationType(EskdSpecificationType eskdSpecificationType)
        {
            return;
        }
    }
}