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
        protected readonly IFileSystem FileSystem;

        public XmlSerializerWrapper(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public virtual void Serialize(string path, object o)
        {
            FileSystem.File.SafeAction(path, tempFile =>
            {
                var ser = new XmlSerializer(o.GetType());
                using (var fileStream = FileSystem.File.Create(tempFile))
                {
                    ser.Serialize(fileStream, o);
                }
            });
        }

        public virtual void Serialize(Stream stream, object o)
        {
            var ser = new XmlSerializer(o.GetType());
            ser.Serialize(stream, o);
        }

        public virtual void Serialize(TextWriter textWriter, object o)
        {
            var ser = new XmlSerializer(o.GetType());
            ser.Serialize(textWriter, o);
        }

        public virtual T Deserialize<T>(string path)
        {
            using (var fs = FileSystem.File.OpenRead(path))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(fs);
            }
        }

        public virtual T Deserialize<T>(TextReader stringReader)
        {
            var ser = new XmlSerializer(typeof(T));
            return (T) ser.Deserialize(stringReader);
        }

        public virtual T Deserialize<T>(Stream stream)
        {
            var ser = new XmlSerializer(typeof(T));
            return (T) ser.Deserialize(stream);
        }
    }
}
