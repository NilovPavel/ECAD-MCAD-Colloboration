using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddInGost
{
    class OpenAssemblyXMLDialog
    {
        private OpenFileDialog openFileDialog;
        private ModelDoc2 modelDoc;

        public string FileName
        { get { return this.GetFileName(); } }

        private void Initialization()
        {
            this.openFileDialog = new OpenFileDialog();
            this.openFileDialog.Filter = "xml files (*.xml)|*.xml";
            this.openFileDialog.Title = "Открыть BOM";
            this.openFileDialog.FilterIndex = 1;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.InitialDirectory = this.modelDoc.GetPathName() ?? string.Empty;
        }

        private string GetFileName()
        {
            string path = string.Empty;
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    path = this.openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            return path;
        }

        public OpenAssemblyXMLDialog(ModelDoc2 modelDoc)
        {
            this.modelDoc = modelDoc;
            this.Initialization();
        }
    }
}
