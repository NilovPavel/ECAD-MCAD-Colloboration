using SearchESKD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardBuilder.KompasSearch
{
    public class FileSystemKompasSearch : ISearchESKD
    {
        private string directory;
        private List<string> files;

        private void Initialization()
        {
            this.directory = @"E:\Работа\Sources\CAD\Altium\AltiumBoardData\Parts";
            this.files = new List<string>();
        }

        public FileSystemKompasSearch()
        {
            this.Initialization();
            this.DirSearch(this.directory);
        }

        void DirSearch(string sourceDirectory)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(sourceDirectory))
                    this.files.Add(fileName);
                foreach (string currentDirectory in Directory.GetDirectories(sourceDirectory))
                    this.DirSearch(currentDirectory);
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        string ISearchESKD.GetPath(DataESKD eskdData)
        {
            return this.files.Where(fileItem => Path.GetFileNameWithoutExtension(fileItem).Equals(eskdData.PartNumber)).FirstOrDefault();
        }
    }
}
