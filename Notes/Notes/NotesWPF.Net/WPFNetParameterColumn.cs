using System;
using INotes;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace NotesWPF
{
    internal class WPFNetParameterColumn : DataGridBoundColumn, IColumn
    {
        private DefaultNotesReader defaultNotesReader;

        public string ColumnName
        { get; set; }

        private FrameworkElement GetElement()
        {
            FrameworkElement element = new TextBox();
            switch (this.ColumnName)
            {
                case "Формат":
                    element = new ComboBox();
                    ((ComboBox)element).IsEditable = false;
                    ((ComboBox)element).ItemsSource = new string[] 
                    {"А4","А3","А2","А1","А4, А3","А4, А3, А2","А4, А3, А2, А1","А4, А3, А1","А4, А2","А4, А2, А1","А3, А2","А3, А2, А1","А3, А1","А2, А1"};
                    break;
                case "Обозначение":
                    break;
                case "Наименование":
                    element = new ComboBox();
                    ((ComboBox)element).IsEditable = false;
                    ((ComboBox)element).ItemsSource = this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
                    break;
                case "Примечание":
                    break;
                case "Едизм":
                    ((TextBox)element).IsEnabled = false;
                    this.IsReadOnly = true;
                    break;
                case "КодДокумента":
                    ((TextBox)element).IsEnabled = false;
                    this.IsReadOnly = true;
                    break;
            }
            return element;
        }

        private void Initialization()
        {
            this.Header = this.ColumnName;
            this.Binding = new Binding(this.ColumnName);
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement element = this.GetElement();
            return element;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            throw new NotImplementedException();
        }

        public WPFNetParameterColumn(string ColumnName, DefaultNotesReader defaultNotesReader)
        {
            this.ColumnName = ColumnName;
            this.defaultNotesReader = defaultNotesReader;
            this.Initialization();
        }
    }
}