using System;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;

namespace AddInGost
{
	// Token: 0x02000009 RID: 9
	internal class OpenAssemblyXMLDialog
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002FFC File Offset: 0x000011FC
		public string FileName
		{
			get
			{
				return this.GetFileName();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003004 File Offset: 0x00001204
		private void Initialization()
		{
			this.openFileDialog = new OpenFileDialog();
			this.openFileDialog.Filter = "xml files (*.xml)|*.xml";
			this.openFileDialog.Title = "Открыть BOM";
			this.openFileDialog.FilterIndex = 1;
			this.openFileDialog.RestoreDirectory = true;
			this.openFileDialog.InitialDirectory = (this.modelDoc.GetPathName() ?? string.Empty);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003074 File Offset: 0x00001274
		private string GetFileName()
		{
			string result = string.Empty;
			if (this.openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					result = this.openFileDialog.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030C4 File Offset: 0x000012C4
		public OpenAssemblyXMLDialog(SolidWorks.Interop.sldworks.ModelDoc2 modelDoc)
		{
			this.modelDoc = modelDoc;
			this.Initialization();
		}

		// Token: 0x04000017 RID: 23
		private OpenFileDialog openFileDialog;

		// Token: 0x04000018 RID: 24
		private SolidWorks.Interop.sldworks.ModelDoc2 modelDoc;
	}
}
