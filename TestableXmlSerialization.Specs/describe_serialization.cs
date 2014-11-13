using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;

namespace TestableXmlSerialization.Specs
{
    class when_updating_configuration : WithSubject<FileSerializer>
    {
        Establish context = () =>
        {
            _fileSystem = new MockFileSystem();
            Configure(x=>x.For<IFileSystem>().Use(_fileSystem));
        };

        Because of = () => Subject.Serialize(_path, Subject);
        
        It should_serialize_the_file = () => _fileSystem.File.Exists(_path).Should().BeTrue();

        static string _path = @"c:\temp\test.xml";
        static IFileSystem _fileSystem;
    }
}
