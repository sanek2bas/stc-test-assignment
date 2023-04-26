using BuildUtility.Parser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildUtility.UnitTests
{
    [TestFixture]
    public class FileReaderTests
    {
        private FileReader _fileReader;

        [SetUp]
        public void Setup()
        {
            _fileReader = new FileReader();
        }

        [Test]
        public void Start_Read_Nonexistent_File()
        {
            _fileReader.Start(string.Empty);
            Assert.Throws(Is.TypeOf<InvalidOperationException>()
                .And.Message.EqualTo("Cannot read temperature before initializing."),
              () => sut.ReadCurrentTemperature());
        }
    }
}
