using BuildUtility.Exceptions;
using BuildUtility.Parser;
using NUnit.Framework;

namespace BuildUtility.UnitTests
{
    [TestFixture]
    public class FileParserTests
    {
        private const string TARGET_1 = "Target1";
        private const string TARGET_2 = "Target2";
        private const string TARGET_3 = "Target3";
        private const string TAB = "\t";
        private const string SPACE = " ";
        private const string EXECUTE = "execute";
        private const string UPDATE = "update";
        private const string SORT = "sort";
        private const string READ = "read";

        private readonly string fileName = "text.txt";
        private FileParser parser;

        [OneTimeSetUp]
        public void Setup()
        {
            parser = new FileParser();
        }

        [TearDown]
        public void TearDown()
        {
            parser.Close();
            deleteFile();
        }

        [Test]
        public void Start_Nonexistent_File()
        {
            var result = parser.Parse(fileName);
            Assert.Throws<FileNotFoundException>(() =>
                result.ToList());
        }

        [Test]
        public void Parse_Empty_File()
        {
            createFile();
            var result = parser.Parse(fileName);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Parse_File_With_Correct_Data()
        {
            createFile();
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"{TARGET_1}: {TARGET_2}   {TARGET_3}");
                sw.WriteLine($" {EXECUTE}");
                sw.WriteLine($" {UPDATE}");
                sw.WriteLine($"{TARGET_2}:        {TARGET_3}");
                sw.WriteLine($"  {SORT}");
                sw.WriteLine($"{TARGET_3}");
                sw.WriteLine($"  {READ}");
            }

            var builds = parser.Parse(fileName).ToList();
            
            var target1 = builds[0];
            Assert.That(target1.Name, Is.EqualTo(TARGET_1));
            Assert.That(target1.Dependencies[0], Is.EqualTo(TARGET_2));
            Assert.That(target1.Dependencies[1], Is.EqualTo(TARGET_3));
            Assert.That(target1.Actions[0], Is.EqualTo(EXECUTE));
            Assert.That(target1.Actions[1], Is.EqualTo(UPDATE));

            var target2 = builds[1];
            Assert.That(target2.Name, Is.EqualTo(TARGET_2));
            Assert.That(target2.Dependencies[0], Is.EqualTo(TARGET_3));
            Assert.That(target2.Actions[0], Is.EqualTo(SORT));

            var target3 = builds[2];
            Assert.That(target3.Name, Is.EqualTo(TARGET_3));
            Assert.That(target3.Dependencies.Count, Is.EqualTo(0));
            Assert.That(target3.Actions[0], Is.EqualTo(READ));
        }

        [Test]
        public void Parse_File_With_Uncorrect_Data_Space_At_The_Beginning_Of_The_Line()
        {
            createFile();
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"{SPACE}{TARGET_1}: {TARGET_2}   {TARGET_3}");
            }
            var result = parser.Parse(fileName);
            Assert.Throws<InvalidFileDataException>(() =>
                result.ToList());
        }

        [Test]
        public void Parse_File_With_Uncorrect_Data_Invalid_In_Line_With_Build_Name()
        {
            createFile();
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"{TARGET_1} {TARGET_2} {TARGET_3}");
            }
            var result = parser.Parse(fileName);
            Assert.Throws<InvalidFileDataException>(() =>
                result.ToList());
        }


        [Test]
        public void Parse_File_With_Uncorrect_Data_Build_Without_Action()
        {
            createFile();
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"{TARGET_3}");
                sw.WriteLine($"{SPACE}");
            }
            var result = parser.Parse(fileName);
            Assert.Throws<InvalidFileDataException>(() =>
                result.ToList());
        }

        [Test]
        public void Parse_File_With_Uncorrect_Data_Empty_Line()
        {
            createFile();
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine($"{TARGET_3}");
                sw.WriteLine(string.Empty);
            }
            var result = parser.Parse(fileName);
            Assert.Throws<InvalidFileDataException>(() =>
                result.ToList());
        }

        private void deleteFile()
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        private void createFile()
        {
            File.Create(fileName).Close();
        }

    }
}
