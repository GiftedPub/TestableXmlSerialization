using System.IO;
using System.IO.Abstractions;
using System.Xml.Serialization;

namespace TestableXmlSerialization
{
    /// <summary>
    /// Manages the yucky stuff of xml editing.
    /// </summary>
    public class XmlContext : IXmlContext
    {
        protected readonly IFileSystem FileSystem;

        public XmlContext(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        /// <summary>
        /// Opens the file provided, then passes it to a preprocessor, deserializes it,
        /// then calls your command. Once the command finishes executing the file is saved.
        /// </summary>
        /// <typeparam name="T">Type of deserialized data</typeparam>
        /// <param name="filePath">Path to xml file</param>
        /// <param name="command">Command to pass the deserialized data to</param>
        public void Execute<T>(string filePath, ICommand<T> command) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            T eventXml;

            using (var fileStream = FileSystem.File.Open(filePath, FileMode.Open))
            {
                var modifiedStream = PreProcessStream(fileStream);
                eventXml = (T)xmlSerializer.Deserialize(modifiedStream);
            }

            command.Update(eventXml);

            using (var stream = FileSystem.File.Create(filePath))
            {
                xmlSerializer.Serialize(stream, eventXml);
            }
        }

        /// <summary>
        /// This is called after the file is opened, but before it is deserialized.
        /// You can perform any sort of stream manipulation you need here.
        /// </summary>
        /// <param name="fileStream">The raw stream of the file's contents</param>
        /// <returns>Same or modified stream</returns>
        protected virtual Stream PreProcessStream(Stream fileStream)
        {
            return fileStream;
        }
    }
}
