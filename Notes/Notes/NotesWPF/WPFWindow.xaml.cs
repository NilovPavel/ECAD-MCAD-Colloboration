using INotes;
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

namespace NotesWPF
{
    /// <summary>
    /// Логика взаимодействия для WPFWindow.xaml
    /// </summary>
    public partial class WPFWindow : Altium.Controls.Wpf.Window, INotesWindow
    {
        public WPFWindow()
        {
            InitializeComponent();
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {

        }

        private void UpRow(object sender, RoutedEventArgs e)
        {

        }

        private void DownRow(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveRow(object sender, RoutedEventArgs e)
        {

        }

        private void WriteNotes(object sender, RoutedEventArgs e)
        {

        }

        void INotesWindow.ShowDialog()
        {
            this.ShowDialog();
        }
    }
}
