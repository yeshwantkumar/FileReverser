using System;
using System.IO;
using System.Text;

namespace ReverseText
{
    public class FileTextReverser
    {
        private readonly IFileService _fileService;
        public FileTextReverser(IFileService fileService)
        {
            _fileService = fileService;
        }
        public string ReverseFileContents(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException();

            try
            {
                string text = _fileService.ReadFile(filePath);
                if(string.IsNullOrWhiteSpace(text))
                    return "Empty File";

                string reverseText = ReverseText(text);
                _fileService.WriteToFile(filePath, reverseText);
                return reverseText;
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string ReverseText(string text)
        {
            StringBuilder reverseText = new StringBuilder();
            for (int i = text.Length - 1; i >= 0; i--)
            {
                reverseText.Append(text[i]);
            }
            return Convert.ToString(reverseText);
        }
    }
}
