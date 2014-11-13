namespace TestableXmlSerialization
{
    public interface ICommand<in T>
    {
        void Update(T payload);
    }
}