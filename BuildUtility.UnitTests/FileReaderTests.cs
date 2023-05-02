using BuildUtility.Parser;
using NUnit.Framework;

namespace BuildUtility.UnitTests
{
    [TestFixture]
    public class FileReaderTests
    {
        private readonly string fileName = "text.txt";
        private FileReader fileReader;


        [SetUp]
        public void Setup()
        {
            fileReader = new FileReader();
            deleteFile();
        }

        [Test]
        public void Start_Nonexistent_File()
        {
            Assert.Throws<FileNotFoundException>(() => 
                fileReader.Start(fileName));
        }

        [Test]
        public void ReadLine_Without_Start_FileReader()
        {
            createFile();
            var line = fileReader.ReadLine();
            Assert.IsNull(line);
        }

        [Test]
        public void ReadLine_After_Stop_FileReader()
        {
           createFile();
            fileReader.Stop();
            var line = fileReader.ReadLine();
            Assert.IsNull(line);
        }

        [Test]
        public void Check_Data_Into_File()
        {
            createFile();
            var data = addDataToFile();
            fileReader.Start(fileName);
            var resultList = new List<string>();
            string? line;
            while((line = fileReader.ReadLine()) is not null)
                resultList.Add(line);
            fileReader.Stop();
            Assert.That(resultList, Is.EqualTo(data));
        }


        private void createFile()
        {
            File.Create(fileName).Close();
        }

        private void deleteFile()
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        private List<string> addDataToFile()
        {
            var data = new List<string>();
            using (var sw = new StreamWriter(fileName))
            {
                for(int i = 0; i < 10; i++)
                {
                    sw.WriteLine(i);
                    data.Add(i.ToString());
                }
            }
            return data;
        }
    }
}
