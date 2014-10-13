using System.IO.Abstractions;
using System.Xml;
using System.Xml.Serialization;

namespace TestableXmlSerialization
{
    public interface IFileSerializer
    {
        T Deserialize<T>(string filename);
        void Serialize<T>(string filename, T config);
    }

    public class FileSerializer : IFileSerializer
    {
        readonly IFileSystem _fileSystem;
        readonly IXmlSerializer _xmlSerializer;

        public FileSerializer(IFileSystem fileSystem, IXmlSerializer xmlSerializer)
        {
            _fileSystem = fileSystem;
            _xmlSerializer = xmlSerializer;
        }

        public T Deserialize<T>(string filename)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(filename))
            {
                result = (T)ser.Deserialize(reader);
            }

            return result;
        }

        public void Serialize<T>(string filename, T obj)
        {
            using (var stream = _fileSystem.File.Create(filename))
            {
                _xmlSerializer.Serialize(stream, obj);
            }
        }
    }
}