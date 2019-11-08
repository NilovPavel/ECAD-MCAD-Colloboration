using FlexBoardSolidworks;
using System.IO;
using System.Resources;

namespace SolidworksBoardSpace
{
    public class SolidworksMaterial
    {
        private ResourceManager resource_manager;

        public string FileName
        { get; private set; }

        private void Initialization()
        {
            this.resource_manager = new ResourceManager("FlexBoardSolidworks.AddInData", typeof(AddInData).Assembly);
            string folderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.FileName = folderPath + @"\" + "MaterialsPCB.sldmat";
        }

        private void WriteFile()
        {
            if(!File.Exists(this.FileName))
            {
                byte[] binary = (byte[])this.resource_manager.GetObject("MaterialsPCB");
                try
                {
                    Stream filestream = new FileStream(this.FileName, FileMode.CreateNew);
                    filestream.Write(binary, 0, binary.Length);
                    filestream.Close();
                }
                finally { }
            }
        }

        public SolidworksMaterial()
        {
            this.FileName = FileName;
            this.Initialization();
            this.WriteFile();
        }
    }
}