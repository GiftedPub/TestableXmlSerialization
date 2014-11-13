using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;

namespace TestableXmlSerialization.Specs
{
    class describe_xml_context : WithSubject<XmlContext>
    {
        Establish context = () =>
        {
            _mockFileSystem = new MockFileSystem();
            Configure<IFileSystem>(_mockFileSystem);
            _path = @"C:\temp\test.xml";
            _mockFileSystem.AddFile(_path, new MockFileData(Xml));
        };

        Because of = () => Subject.Execute(_path, new FooCommand());

        It should_update_file = () => XDocument.Load(_mockFileSystem.File.OpenRead(_path))
            .Should().HaveRoot("msg").Which.Should().HaveAttribute("file", "test2.xml");

        const string Xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<msg path=""C:\foo"" file=""test.xml""></msg>";
        static string _path;
        static MockFileSystem _mockFileSystem;
    }

    class FooCommand : ICommand<Foo>
    {
        public void Update(Foo payload)
        {
            payload.File = "test2.xml";
        }
    }
}
