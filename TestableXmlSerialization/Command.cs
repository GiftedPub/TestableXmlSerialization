using System;

namespace TestableXmlSerialization
{
    public class Command<T> : ICommand<T>
    {
        protected Action<T> ContextQuery;

        public void Update(T payload)
        {
            ContextQuery(payload);
        }
    }
}