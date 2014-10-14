using System.IO;
using System.IO.Abstractions;
using System.Xml.Serialization;

namespace TestableXmlSerialization
{
    public interface IXmlSerializer
    {
        void Serialize(Stream stream, object o);
        T Deserialize<T>(string path);
    }

    public class XmlSerializerWrapper : IXmlSerializer
    {
        XmlSerializer _xmlSerializer;
        readonly IFileSystem _fileSystem;

        public XmlSerializerWrapper(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Serialize(Stream stream, object o)
        {
            _xmlSerializer = new XmlSerializer(o.GetType());
            _xmlSerializer.Serialize(stream, o);
        }

        public T Deserialize<T>(string path)
        {
            using (var fs = _fileSystem.File.OpenRead(path))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(fs);
            }
        }
    }
}
