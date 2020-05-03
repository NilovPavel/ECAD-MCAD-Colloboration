using INotes;
using System.Windows.Controls;
using System;
using System.Windows.Data;
using System.Windows;


namespace NotesWPF
{
    public class WPFNetDataGridFormatColumn : DataGridBoundColumn, IColumn
    {
        private void Initialization()
        {
            this.Header = this.ColumnName;
            this.Binding = new Binding(this.ColumnName);
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            ComboBox comboBox = new ComboBox();
            string value = (string) dataItem.GetType().GetProperty(this.ColumnName).GetValue(dataItem);
            comboBox.IsEditable = false;
            comboBox.ItemsSource = new string[]
            {"А4","А3","А2","А1","А4, А3","А4, А3, А2","А4, А3, А2, А1","А4, А3, А1","А4, А2","А4, А2, А1","А3, А2","А3, А2, А1","А3, А1","А2, А1"};
            comboBox.Text = value;
            return comboBox;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return null;
        }

        public WPFNetDataGridFormatColumn()
        {
            this.Initialization();
        }

        public string ColumnName
        {
            get
            {
                return "Формат";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}