using AddressProcessing.Abstract;
using AddressProcessing.Common;
using AddressProcessing.CSV;
using System.IO;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        private const string TestReadFile = @"test_data\contacts.csv";       
        private const string TestWriteFile = @"test_data\writecontacts.csv";

        private StreamReader _readerStream = null;
        private StreamWriter _writerStream = null;

        private CSVReader _reader;
        private CSVWriter _writer;

        /// <summary>
        /// Test to read the content from contacts.csv file using CSVReader.
        /// </summary>
        [Test]
        public void CSVReaderTest()
        {
            string column1;
            string column2;
            _readerStream = new StreamReader(TestReadFile);
            _reader = new CSVReader(_readerStream);
            _reader.Read(out column1, out column2);
            Assert.AreEqual("Shelby Macias", column1);
            Assert.AreEqual("3027 Lorem St.|Kokomo|Hertfordshire|L9T 3D5|England", column2);
        }

        /// <summary>
        /// Test to open the file in read mode using CSVHandler.
        /// </summary>
        [Test]
        public void CSVOpenReadTest()
        {
            ICSVHandler handler = new CSVHandler(_readerStream, _writerStream);
            handler.Open(TestReadFile, Enums.Mode.Read);
            //if we are passing valid file for reading then reader must have valid reference and should not be null
            var reader = handler.Reader;
            var writer = handler.Writer;
            Assert.IsNotNull(reader);
            Assert.IsNull(writer);
            handler.Close();
        }

        /// <summary>
        /// Test to open the file in write mode using CSVHandler.
        /// </summary>
        [Test]
        public void CSVOpenWriteTest()
        {
            ICSVHandler handler = new CSVHandler(_readerStream, _writerStream);
            handler.Open(TestWriteFile, Enums.Mode.Write);
            //if we are passing valid file for writing then writer must have valid reference and should not be null
            var reader = handler.Reader;
            var writer = handler.Writer;          
            Assert.IsNotNull(writer);
            Assert.IsNull(reader);
            handler.Close();
        }

        /// <summary>
        /// Test to open invalid file using CSVHandler open method .
        /// </summary>
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void CSVOpenInvalidFileExceptionTest()
        {
            ICSVHandler handler = new CSVHandler(_readerStream, _writerStream);
            handler.Open("TestForUnavailableFile.csv", Enums.Mode.Read);
            handler.Close();
        }

        /// <summary>
        /// Test to write the content in writecontacts.csv file using CSVWriter write method :
        /// To see the output of in file please the debug folder of the solution.
        /// </summary>
        [Test]
        public void CSVWriterTest()
        {
            string[] items = { "TrainLine", "Test" };
            _writerStream = new StreamWriter(TestWriteFile);
            _writer = new CSVWriter(_writerStream);
            _writer.Write(items);
            _writerStream.Close();
            _readerStream = new StreamReader(TestWriteFile);
            _reader = new CSVReader(_readerStream);
            string column1, column2;
            _reader.Read(out column1, out column2);
            Assert.AreEqual("TrainLine", column1);
            Assert.AreEqual("Test", column2);
        }
    }
}
