using KompasAPI7;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KompasApplicationSpace
{
    public class KompasApplication
    {
        private IApplication kompas;

        public IApplication Kompas
        { get { return this.kompas; } }

        private void Initialization()
        {
            this.kompas = (IApplication)Marshal.GetActiveObject("KOMPAS.Application.7");

            if(this.kompas == null)
                this.kompas = (IApplication)Activator.CreateInstance(Type.GetTypeFromProgID("KOMPAS.Application.7"));

            //this.kompas.Application.Visible = true;
            //this.kompas.ActivateControllerAPI();
        }
        public KompasApplication()
        {
            this.Initialization();
        }

        public void CloseOpenDocuments()
        {
            while(this.kompas.ActiveDocument != null)
                this.kompas.ActiveDocument.Close(Kompas6Constants.DocumentCloseOptions.kdDoNotSaveChanges);
            /*IEnumerator iEnumerator = this.kompas.Documents.GetEnumerator();
            for (IKompasDocument iKompasDocument = (IKompasDocument)iEnumerator.Current; iEnumerator.Current != null; iEnumerator.Reset())
                iKompasDocument.Close(Kompas6Constants.DocumentCloseOptions.kdSaveChanges);*/
            /*while (this.kompas.ActiveDocument != null)
                ((ksDocument3D)this.kompas.ActiveDocument3D()).close();*/
        }
    }
}
