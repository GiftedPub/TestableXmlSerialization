using System.IO;
using System.Xml.Serialization;

namespace TestableXmlSerialization
{
    public interface IXmlSerializer
    {
        void Serialize(Stream stream, object o);
    }

    public class XmlSerializerWrapper : IXmlSerializer
    {
        XmlSerializer _xmlSerializer;

        public void Serialize(Stream stream, object o)
        {
            _xmlSerializer = new XmlSerializer(o.GetType());
            _xmlSerializer.Serialize(stream, o);
        }
    }
}
