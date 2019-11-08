using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ExecutableReportESKD
{
    public class TestReport
    {
        private string filePath;
        private XmlSerializer formatter; 

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
                new SpecificationReport(assembly);
            }
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