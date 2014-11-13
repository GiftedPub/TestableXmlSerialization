TestableXmlSerialization
========================

Manages the yucky stuff of xml editing.

Example
-------

The code to execute:

```C#
var fileSystem = new FileSystem(); // System.IO.Abstractions
var context = new XmlContext(fileSystem);

context.Execute(@"C:\temp\foo.xml", new UpdateFoo());
```

Command code:

```C#
class UpdateFoo : ICommand<Foo>
{
    public void Update(Foo foo)
    {
        foo.Bar = "baz";
    }
}
```
