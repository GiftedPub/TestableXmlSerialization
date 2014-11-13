namespace TestableXmlSerialization
{
    public interface IXmlContext
    {
        void Execute<T>(string filePath, ICommand<T> command) where T : class;
    }
}