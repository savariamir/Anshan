using System.Collections.Generic;

namespace Anshan.Test.Faker
{
    public class ContextData
    {
        private readonly Dictionary<string, object> _data = new();

        public T Get<T>(string name)
        {
            return (T) _data[name];
        }

        public void Set<T>(string name, T data)
        {
            _data[name] = data;
        }
    }
}