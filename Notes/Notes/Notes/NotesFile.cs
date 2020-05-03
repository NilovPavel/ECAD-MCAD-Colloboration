using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWindow
{
    class NotesFile
    {
        private string folderPath;

        public string[] GetLines(string razdelName)
        {
            string fileName = this.folderPath + @"\" + razdelName + ".txt";

            if(!File.Exists(fileName))
            {
                File.WriteAllLines(fileName, this.GetCollectionFromRazdel(razdelName));
            }

            string[] lines = File.ReadAllLines(fileName);
            return lines;
        }

        public void WriteLines(string razdelName, string[] lines)
        {
            string fileName = this.folderPath + @"\" + razdelName + ".txt";
            File.WriteAllLines(fileName, lines);
        }

        private string[] GetCollectionFromRazdel(string razdelName)
        {
            switch (razdelName)
            {
                case "Документация":
                    return new string[] { "А3#ИЮТВ.111111.222#Ведомость технического проекта#ТП##1#1" }; 
                case "Комплекты":
                    return new string[] { "А1#ИЮТВ.111111.222#Комплект запасных частей#КЗЧ##1#1" };
                case "Материалы":
                    return new string[] { "Медные листы#324#м2##0#1" };
            }
            return new string[] { "" };
        }

        private void Initialization()
        {
            this.folderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ;
        }

        public NotesFile()
        {
            this.Initialization();
        }
    }
}
