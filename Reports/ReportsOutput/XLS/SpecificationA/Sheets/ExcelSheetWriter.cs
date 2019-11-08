using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Linq;

namespace ReportsOutput
{
    internal class ExcelSheetWriter : IDocumentSheetPropertiesWriter
    {
        private IDocument iDocument;
        private ProjectProperties projectProperties;

        public void WriteSheetProperties()
        {
            List<AddressParameterCell> cellsAddresses = this.iDocument.CurrentSheet.GetAddressesParameters();
            foreach (AddressParameterCell addressParameterCell in cellsAddresses)
            {
                string value = projectProperties.GetPropertyByName(addressParameterCell.Parametr);
                this.iDocument.WriteCellValue(addressParameterCell.Column, addressParameterCell.Row, value);
            }
            AddressParameterCell cellSheetNumber = cellsAddresses.Where(item => item.Parametr.Equals("Лист")).FirstOrDefault();
            this.iDocument.WriteCellValue(cellSheetNumber.Column, cellSheetNumber.Row, this.iDocument.CurrentSheetIndex.ToString());
        }

        public ExcelSheetWriter(IDocument iDocument, ProjectProperties projectProperties)
        {
            this.projectProperties = projectProperties;
            this.iDocument = iDocument;
        }
    }
}