using PaintImage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Paint
{
    class Program
    {
        static void Main()
        {
            string xmlFilePath = @"D:\PDM\Проект\MiniPC MB GOST\MiniPC MB GOST.xml";
            
            Assembly assembly = default(Assembly);
            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));
            
            using (FileStream fs = new FileStream(xmlFilePath, FileMode.OpenOrCreate))
            {
                assembly = (Assembly)formatter.Deserialize(fs);
            }

            string fileImagePath = Path.GetDirectoryName(xmlFilePath) + @"\" + Path.GetFileNameWithoutExtension(xmlFilePath) + "TOP.jpg";
            int[] array = new int[] { 33, 1 };
            ImageBuilder imageBuilder = new ImageBuilder(assembly, array, fileImagePath);
            Process.Start(fileImagePath);
            
            array = new int[] {34, 32};
            fileImagePath = Path.GetDirectoryName(xmlFilePath) + @"\" + Path.GetFileNameWithoutExtension(xmlFilePath) + "BOTTOM.jpg";
            imageBuilder = new ImageBuilder(assembly, array, fileImagePath);
            Process.Start(fileImagePath);
        }
    }
}
