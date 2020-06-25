using Kompas6API5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KompasApplicationSpace
{
    public class KompasApplication
    {
        private KompasObject kompas;

        public KompasObject Kompas
        { get { return this.kompas; } }

        private void Initialization()
        {
            this.kompas = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");

            if(this.kompas == null)
                this.kompas = (KompasObject)Activator.CreateInstance(Type.GetTypeFromProgID("KOMPAS.Application.5"));

            this.kompas.Visible = true;
            this.kompas.ActivateControllerAPI();
        }
        public KompasApplication()
        {
            this.Initialization();
        }

        public void CloseOpenDocuments()
        {
            while (this.kompas.ActiveDocument3D() != null)
                ((ksDocument3D)this.kompas.ActiveDocument3D()).close();
        }
    }
}
