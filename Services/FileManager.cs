using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Services
{
    internal interface IFileManager // interface för två metoder

    {
        public void Save(string filePath, string content);

        public string Read(string filePath);


    }
    internal class FileManager : IFileManager
    {
        public string Read(string filePath)
        {
            using var sr = new StreamReader(filePath);
            return sr.ReadToEnd();

        }


        public void Save(string filePath, string content)
        {

            using var sw = new StreamWriter(filePath);  //StreamWriter hämtar och sparar till vår filepath 
            sw.WriteLine(content); //sparar texten 


        }
    }
}
