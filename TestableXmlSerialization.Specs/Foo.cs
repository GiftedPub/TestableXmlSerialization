using System.Xml.Serialization;

namespace TestableXmlSerialization.Specs
{
    [XmlRoot(ElementName = "msg", Namespace = "")]
    public class Foo 
    {
        [XmlAttribute("req")]
        public string Request { get { return "UPDATE"; } }

        [XmlAttribute("path")]
        public string Path { get; set; }

        [XmlAttribute("file")]
        public string File { get; set; }
    }  
}
