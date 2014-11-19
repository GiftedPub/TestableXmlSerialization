using System.IO;
using System.IO.Abstractions;
using System.Xml.Serialization;

namespace TestableXmlSerialization
{
    public interface IXmlSerializer
    {
        void Serialize(Stream stream, object o);
        void Serialize(TextWriter textWriter, object o);
        T Deserialize<T>(string path);
        T Deserialize<T>(TextReader stringReader);
        T Deserialize<T>(Stream stream);
    }

    public class XmlSerializerWrapper : IXmlSerializer
    {
        readonly IFileSystem _fileSystem;

        public XmlSerializerWrapper(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Serialize(Stream stream, object o)
        {
            var ser = new XmlSerializer(o.GetType());
            ser.Serialize(stream, o);
        }

        public void Serialize(TextWriter textWriter, object o)
        {
            var ser = new XmlSerializer(o.GetType());
            ser.Serialize(textWriter, o);
        }

        public T Deserialize<T>(string path)
        {
            using (var fs = _fileSystem.File.OpenRead(path))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(fs);
            }
        }

        public T Deserialize<T>(TextReader stringReader)
        {
            var ser = new XmlSerializer(typeof(T));
            return (T) ser.Deserialize(stringReader);
        }

        public T Deserialize<T>(Stream stream)
        {
            var ser = new XmlSerializer(typeof(T));
            return (T) ser.Deserialize(stream);
        }
    }
}
