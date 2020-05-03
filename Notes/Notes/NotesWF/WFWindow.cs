using INotes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesWF
{
    public partial class WFWindow : Form, INotesWindow
    {
        private string[] configurationNames;
        private ProjectProperties projectProperties;
        private Notes notes;
        private AbstractTable iTable;

        private void Initialization()
        {
            this.Text = this.notes.RazdelName;
            this.iTable = new WFTable(this.configurationNames, this.notes, this.table);
        }

        public WFWindow(string[] configurationNames, ProjectProperties projectProperties, Notes notes)
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

        private void table_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);

            if (e.ColumnIndex >=5)
            { return; }

            DataGridViewComboBoxCell comboboxCell = (DataGridViewComboBoxCell)this.table.Rows[e.RowIndex].Cells[e.ColumnIndex];
            comboboxCell.DataSource = ((WFTable)this.iTable).DefaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
        }
    }
}
