﻿using INotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotesWPF.Net
{
    /// <summary>
    /// Логика взаимодействия для WPFNetWindow.xaml
    /// </summary>
    public partial class WPFNetWindow : Window, INotesWindow
    {
        private string[] configurationNames;
        private ProjectProperties projectProperties;
        private Notes notes;
        private AbstractTable iTable;


        private void Initialization()
        {
            this.Title = this.notes.RazdelName;
            this.iTable = new WPFNetTable(this.configurationNames, this.notes, this.table);
        }

        public WPFNetWindow(string[] configurationNames, ProjectProperties projectProperties, Notes notes)
        {
            this.configurationNames = configurationNames;
            this.projectProperties = projectProperties;
            this.notes = notes;
            InitializeComponent();
            this.Initialization();
        }
        
        void INotesWindow.ShowDialog()
        {
            this.ShowDialog();
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveRow(object sender, RoutedEventArgs e)
        {

        }

        private void WriteNotes(object sender, RoutedEventArgs e)
        {

        }

        private void UpRow(object sender, RoutedEventArgs e)
        {

        }

        private void DownRow(object sender, RoutedEventArgs e)
        {

        }
    }
}
