using EPDM.Interop.epdm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchESKD
{
    public class SearchSolidworksPDM : ISearchESKD
    {
        private IEdmVault18 vault;
        private EdmViewInfo[] views;
        private IEdmSearch7 search;

        string ISearchESKD.GetPath(DataESKD eskdData)
        {
            string path = null;
            this.search.Clear();
            this.SetSearchParametersESKD(eskdData);
            //search.get_State()
            int count = 0;
            for (IEdmSearchResult5 searchResult = this.search.GetFirstResult();  searchResult != null; searchResult = this.search.GetNextResult())
            {
                count++;
                if (Path.GetExtension(searchResult.Path).ToLower().Equals(".sldprt") || Path.GetExtension(searchResult.Path).ToLower().Equals(".sldasm"))
                {
                    path = searchResult.Path;
                    break;
                }
            }
            return path;
        }

        private void SetSearchParametersESKD(DataESKD eskdData)
        {
            switch (eskdData.РазделСп)
            {
                case "Сборочные единицы":
                case "Детали":
                    this.search.AddVariable("Обозначение", eskdData.Обозначение);
                    this.search.AddVariable("Наименование", eskdData.Наименование);
                    break;
                case "Стандартные изделия":
                case "Материалы":
                    this.search.AddVariable("Наименование", eskdData.Наименование);
                    break;
                case "Прочие изделия":
                    this.search.AddVariable("elib_ERP_Part_Number", eskdData.PartNumber);
                    break;
            }
        }

        private void LogIn()
        {
            string message = "Не удалось подключиться к следующим хранилищам:\r\n";
            for (int i = 0; i < views.Length && !this.vault.IsLoggedIn; i++ )
            {
                EdmViewInfo currentViewInfo = views[i];
                try
                { this.vault.LoginAuto(currentViewInfo.mbsVaultName, 0); }
                catch (Exception ex)
                { message += currentViewInfo.mbsVaultName + "\r\n" + ex.Message + "\r\n"; }
            }

            if (!this.vault.IsLoggedIn)
                MessageBox.Show(message);
        }

        private void Initialization()
        {
            this.vault = (IEdmVault18)new EdmVault5();
            this.vault.GetVaultViews(out views, false);

            this.LogIn();
            if (!this.vault.IsLoggedIn)
                return;

            this.search = (IEdmSearch7)this.vault.CreateSearch();
            this.search.FindFiles = true;
            this.search.FindUnlockedFiles = true;

        }

        public SearchSolidworksPDM()
        {
            this.Initialization();
        }
    }
}
