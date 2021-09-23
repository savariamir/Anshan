using System;
using System.Reflection;

namespace Anshan.OutboxProcessor.Types
{
    public interface IEventTypeResolver
    {
        void AddTypesFromAssembly(Assembly assembly);

        Type GetType(string typeName);
    }
}