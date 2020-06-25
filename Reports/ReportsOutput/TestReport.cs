using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ReportsOutput
{
    public class TestReport
    {
        private string filePath;
        private XmlSerializer formatter;
        private AbstractReport report;

        /*private void OneReportIteration(string currentPath)
        {
            Assembly assembly;
            using (FileStream fs = new FileStream(currentPath, FileMode.Open, FileAccess.Read))
            {
                assembly = (Assembly)formatter.Deserialize(fs);
            }
            this.report = new SpecificationReport(assembly);
            AssemblyPositionXML assemblyPosition = new AssemblyPositionXML((SpecificationReport)this.report);
            string newPath = Path.GetDirectoryName(currentPath) + @"\" + Path.GetFileNameWithoutExtension(currentPath) + "_pos" + ".xml";
            assemblyPosition.WritePosition(newPath);
            if (this.report.Spisok.Count == 0)
                return;

            IDocument iDocuemnt = new SpecificationADocumentXLS(assembly.projectProperties);
            new SpecificationA(currentPath, this.report, iDocuemnt);
        }*/

        private void OneReportIteration(string currentPath)
        {
            Assembly assembly;
            using (FileStream fs = new FileStream(currentPath, FileMode.Open, FileAccess.Read))
            {
                assembly = (Assembly)formatter.Deserialize(fs);
            }
            this.report = new PerechenReport(assembly);
            //string newPath = Path.GetDirectoryName(currentPath) + @"\" + Path.GetFileNameWithoutExtension(currentPath) + "_pos" + ".xml";

            //IDocument iDocuemnt = new SpecificationADocumentXLS(assembly.projectProperties);
            //new PerechenA(currentPath, this.report, iDocuemnt);
        }

        private void RunReport()
        {
            /*string[] lines = System.IO.File.ReadAllLines(this.filePath, Encoding.Default);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
                this.OneReportIteration(line);
            }*/

            //this.OneReportIteration(@"D:\Test\BoardData\ReportData\ИЮТВ.467479.005СБ Модуль вычислителя (МВ-2).xml");
            //this.OneReportIteration(@"D:\Test\BoardData\Altium\MiniPC.xml");
            this.OneReportIteration(@"D:\Test\BoardData\Altium\Hierarchy_PRJ.xml");
        }

        private void Initialization()
        {
            this.filePath = @"D:\Test\BoardData\ReportData\ReportESKD.txt";
            this.formatter = new XmlSerializer(typeof(Assembly));
        }

        public TestReport()
        {
            this.Initialization();
            this.RunReport();
        }
    }
}
