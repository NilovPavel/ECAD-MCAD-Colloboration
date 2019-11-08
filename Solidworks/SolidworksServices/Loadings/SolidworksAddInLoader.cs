using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Loadings
{
    public class SolidworksAddInLoader
    {
        private SldWorks swApp;
        private string folderPath;

        private void Initialization()
        {
            this.swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            this.folderPath = this.swApp.GetExecutablePath() + @"\";
        }

        public bool LoadAddin(string addInFileName)
        {
            string fullAddinFileName = this.folderPath + addInFileName;

            if (!File.Exists(fullAddinFileName))
                return false;

            try
            {
                while (this.swApp.LoadAddIn(fullAddinFileName) != (int)swLoadAddinError_e.swAddinAlreadyLoaded);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SolidWorks Routing Library file sldrtadd.dll is not found!" + "\r\n" + ex.Message, "SolidWorks Routing Library is not installed!", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            return true;
        }

        public SolidworksAddInLoader()
        {
            this.Initialization();
        }
    }
}
