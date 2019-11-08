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
        public WFWindow()
        {
            InitializeComponent();
        }

        void INotesWindow.ShowDialog()
        {
            this.ShowDialog();
        }
    }
}
