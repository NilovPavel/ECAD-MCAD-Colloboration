using System;
using INotes;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Linq;
using System.Windows.Media;

namespace NotesWPF
{
    internal class WPFNetDataGridNameColumn : DataGridTemplateColumn, IColumn
    {
        private DefaultNotesReader defaultNotesReader;

        private void Initialization()
        {
            this.Header = this.ColumnName;
            //this.ElementStyle = new Style(typeof(ComboBox));
            //this.SelectedValuePath = this.ColumnName;
            /*this.SortMemberPath = this.ColumnName;
            this.SelectedValueBinding = new Binding()
            {
                Path = new PropertyPath(this.ColumnName),
                ElementName = this.ColumnName,
                Mode = BindingMode.TwoWay
            };*/
            //this.DisplayMemberPath = this.ColumnName;
            //this.Binding = new Binding(this.ColumnName);
            //this.DisplayMemberPath = this.ColumnName;
            //this.ItemsSource = this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
        }

        public WPFNetDataGridNameColumn(DefaultNotesReader defaultNotesReader)
        {
            this.defaultNotesReader = defaultNotesReader;
            this.Initialization();
        }

        public string ColumnName
        {
            get
            {
                return "Наименование";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /*protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.ItemsSource = this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
            comboBox.IsEditable = false;

            return comboBox;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            ComboBox comboBox = new ComboBox();
            //comboBox.SelectionChanged += this.СhangeName;
            comboBox.ItemsSource = this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
            comboBox.IsEditable = false;

            Binding binding = new Binding();
            string value = (string)dataItem.GetType().GetProperty(this.ColumnName).GetValue(dataItem);
            comboBox.Text = value;

            return comboBox;
        }*/

        /*private void СhangeName(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string codeDoc = this.defaultNotesReader.SectionPair.Where(item => item.Name.Equals(comboBox.Text)).Select(item => item.Value).FirstOrDefault();
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.ItemsSource = this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
            comboBox.IsEditable = false;

            return comboBox;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            throw new NotImplementedException();
        }*/
    }
}