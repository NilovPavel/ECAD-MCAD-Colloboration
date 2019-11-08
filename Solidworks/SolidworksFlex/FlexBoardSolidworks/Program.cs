using SolidWorks.Interop.sldworks;
using SolidWorksAssemblySpace;
using SolidworksBoardData;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FlexBoardSolidworks
{
    class Program
    {
        static void buildBoards()
        {
            string[] lines = System.IO.File.ReadAllLines(@"D:\TEST\BoardData\Solidworks\SolidworksProjects.txt", Encoding.Default);

            foreach (string str in lines)
            {
                new CloseOpenDocuments();
                //Console.WriteLine(str);
                IBuilderCAD builderAssembly = new SolidworksBuilderRoot(str);
                //Thread.Sleep(5000);
            }

            /*new CloseOpenDocuments(swApp);
            new SolidworksBuilderRoot(@"d:\Test\BoardData\Solidworks\L_MSV-2.xml");*/
        }

        static void OneAssembly(string path)
        {
                new CloseOpenDocuments();
                IAssemblyCAD assem_cad = new SolidworksAsseblyXML(path);
                
                Assembly board = new Assembly(assem_cad);

                XmlSerializer formatter = new XmlSerializer(typeof(Assembly));

                using (FileStream fs = new FileStream(@"d:\Test\BoardData\SolidworksAssemblyData\" + Path.GetFileNameWithoutExtension(path) + ".xml", FileMode.Create))
                {
                    formatter.Serialize(fs, board);
                }

                using (FileStream fs = new FileStream(@"d:\Test\BoardData\SolidworksAssemblyData\" + Path.GetFileNameWithoutExtension(path) + ".xml", FileMode.OpenOrCreate))
                {
                    board = (Assembly)formatter.Deserialize(fs);
                }
        }

        static void buildXMLFromSolidworks()
        {
            string[] lines = System.IO.File.ReadAllLines(@"D:\Test\BoardData\SolidworksAssemblyData\SolidworksProjects.txt", Encoding.Default).Distinct().ToArray();
            //lines = new string[] { @"D:\ELSY-PDM\Проект\ИЮТВ.468454.002 ЦАФС\Корпус новый\ИЮТВ.301265.022 Крышка-теплообменник\ИЮТВ.301265.022СБ Крышка-теплообменник.SLDASM" };
            //lines = new string[] { @"D:\ELSY-PDM\Проект\ИЮТВ.463436.023СРД-Б\ИЮТВ.468364.060БОР-Б\ИЮТВ.468364.060БОР-Б.SLDASM" };
            foreach (string str in lines)
            {
                try
                {
                    OneAssembly(str);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            /*new CloseOpenDocuments(swApp);
            new SolidworksBuilderRoot(@"d:\Test\BoardData\Solidworks\L_MSV-2.xml");*/
        }

        static void Main(string[] args)
        {

            try
            {
                buildXMLFromSolidworks();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
