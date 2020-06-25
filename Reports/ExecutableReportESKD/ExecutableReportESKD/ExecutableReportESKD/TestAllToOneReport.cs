using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ExecutableReportESKD
{
    public class TestAllToOneReport
    {
        private string filePath;
        private XmlSerializer formatter;
        private AbstractReport report;
        private IIteratorAction iteratorAction;
        private IteratorSpisok iteratorSpisok;

        private void RunReport()
        {
            string[] lines = System.IO.File.ReadAllLines(this.filePath, Encoding.Default);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
                Assembly assembly;
                using (FileStream fs = new FileStream(line, FileMode.Open, FileAccess.Read))
                {
                    assembly = (Assembly)formatter.Deserialize(fs);
                }
                this.report = new SpecificationReport(assembly);
                if (this.report.Spisok.Count == 0)
                    continue;
                this.iteratorSpisok = new IteratorSpisok(this.report.Spisok.First());
                this.iteratorSpisok.SetIteratorAction(this.iteratorAction);
                this.iteratorSpisok.Iteration();
            }
            ((ExcelIteratorAction)this.iteratorAction).Close();
        }

        private void Initialization()
        {
            this.filePath = @"D:\Test\BoardData\ReportData\ReportESKD.txt";
            this.formatter = new XmlSerializer(typeof(Assembly));
            this.iteratorAction = new ExcelIteratorAction();
        }

        public TestAllToOneReport()
        {
            this.Initialization();
            this.RunReport();
        }
    }
}