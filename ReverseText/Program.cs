using System;
using static System.Console;
namespace ReverseText
{
    // IMPORTANT: make sure you read the instructions in README.md

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string filePath = "TestTextFile.txt";
                FileTextReverser textReverser = new FileTextReverser(new FileService());
                string text = textReverser.ReverseFileContents(filePath);
                WriteLine(text);
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
    }
}
