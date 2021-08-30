using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Anshan.Mongo.Abstractions;

namespace Anshan.Mongo.Internal.Configuration
{
    /// <summary>
    ///     Registers the derived types of <see cref="IMongoConfiguration" />
    /// </summary>
    public class ConfigurationController
    {
        internal static ConfigurationController Instance => new();

        private ConfigurationController()
        {
        }

        private static readonly ThreadSafeSingleShotGuard Guard = new();

        private readonly ConcurrentQueue<IMongoConfiguration> _configurations = new();

        /// <summary>
        ///     Applies every registered type of <see cref="IMongoConfiguration" />
        /// </summary>
        internal void Apply()
        {
            if (Guard.CheckAndSetFirstCall)
            {
                while (_configurations.Count > 0)
                {
                    if (_configurations.TryDequeue(out IMongoConfiguration mongoConfiguration))
                    {
                        mongoConfiguration.Configure();
                    }
                }
            }
        }


        public ConfigurationController Register<TMongoConfiguration>()
            where TMongoConfiguration : IMongoConfiguration, new()
        {
            _configurations.Enqueue(new TMongoConfiguration());

            return this;
        }

        public ConfigurationController Register(IMongoConfiguration mongoConfiguration)
        {
            _configurations.Enqueue(mongoConfiguration);

            return this;
        }

        public ConfigurationController RegisterFrom(Assembly assembly)
        {
            var foundTypes = assembly.DefinedTypes
                                     .Where(t => t.IsAssignableTo(typeof(IMongoConfiguration)));

            foreach (var foundType in foundTypes)
            {
                var mongoConfiguration = Activator.CreateInstance(foundType) as IMongoConfiguration;

                Register(mongoConfiguration);
            }

            return this;
        }
    }
}