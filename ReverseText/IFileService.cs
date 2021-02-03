namespace ReverseText
{
    public interface IFileService
    {
        string ReadFile(string filePath);
        void WriteToFile(string filePath, string text);
    }
}