using ReverseText;
using NUnit.Framework;
using Moq;
using System;
using System.Diagnostics;
using System.Threading;

namespace ReverseText.Tests
{
    [TestFixture]
    public class FileTextReverser_Tests
    {
        private Mock<IFileService> mockFileService;
        private FileTextReverser fileTextReverser;
        private string filePath;

        [SetUp]
        public void Setup(){
            filePath = "File1.txt";
            mockFileService = new Mock<IFileService>();
            fileTextReverser = new FileTextReverser(mockFileService.Object);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void ReverseFileContents_FilePathNullOrEmpty_ThrowsArgumentNullException(string filePath)
        {
            Assert.That(()=>fileTextReverser.ReverseFileContents(filePath), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void ReverseFileContents_ReadEmptyTextFromFile_ReturnsEmptyFile(string emptyFile)
        {
            mockFileService.Setup(fs=>fs.ReadFile(It.IsAny<string>()))
                            .Returns(emptyFile);
            mockFileService.Setup(fs=>fs.WriteToFile(It.IsAny<string>(),It.IsAny<string>()));

            string result = fileTextReverser.ReverseFileContents(filePath);
            Assert.That(result, Does.Contain("empty file").IgnoreCase);
            mockFileService.Verify(fs=>fs.WriteToFile(It.IsAny<string>(),It.IsAny<string>()), Times.Never);
        }
        
        [Test]
        public void ReverseFileContents_ReadFile_ThrowsFileNotFoundException()
        {
            mockFileService.Setup(fs=>fs.ReadFile(It.IsAny<string>()))
                            .Throws<System.IO.FileNotFoundException>();
            mockFileService.Setup(fs=>fs.WriteToFile(It.IsAny<string>(),It.IsAny<string>()));

            Assert.That(()=> fileTextReverser.ReverseFileContents(filePath), Throws.Exception.TypeOf<System.IO.FileNotFoundException>());
            mockFileService.Verify(fs=>fs.WriteToFile(It.IsAny<string>(),It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReverseFileContents_ReadFile_ThrowsGenericException()
        {
            mockFileService.Setup(fs=>fs.ReadFile(It.IsAny<string>()))
                            .Throws<Exception>();
            mockFileService.Setup(fs=>fs.WriteToFile(It.IsAny<string>(),It.IsAny<string>()));

            Assert.That(()=> fileTextReverser.ReverseFileContents(filePath), Throws.Exception.TypeOf<Exception>());
            mockFileService.Verify(fs=>fs.WriteToFile(It.IsAny<string>(),It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReverseFileContents_ReadTextFromFile_ReverseTheText()
        {
            string fileTextReturn = "123";
            string expectedReversedText = "321";
            mockFileService.Setup(fs=>fs.ReadFile(It.IsAny<string>()))
                            .Returns(fileTextReturn);
            mockFileService.Setup(fs=>fs.WriteToFile(It.IsAny<string>(),expectedReversedText));

            string result = fileTextReverser.ReverseFileContents(filePath);
            Assert.That(result, Is.EqualTo(expectedReversedText));
            mockFileService.Verify(fs=>fs.WriteToFile(filePath, expectedReversedText));
        }
    }
}
