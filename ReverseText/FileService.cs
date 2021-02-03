using System;
using System.IO;

namespace ReverseText
{
    public class FileService : IFileService
    {
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void WriteToFile(string filePath, string text)
        {
            File.WriteAllLines(filePath, new string[] { text });
        }
    }
}