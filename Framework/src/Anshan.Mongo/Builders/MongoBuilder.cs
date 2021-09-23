using System;
using Anshan.Mongo.Internal.Configuration;
using Anshan.Mongo.Options;

namespace Anshan.Mongo.Builders
{
    public class MongoBuilder
    {
        private readonly ConfigurationController _configurationController;
        private readonly MongoRepositoryBuilder _mongoRepositoryBuilder;

        public MongoConnectionOptions Connection { get; }
        
        public MongoBuilder(MongoConnectionOptions connectionOptions,
                            ConfigurationController configurationController,
                            MongoRepositoryBuilder mongoRepositoryBuilder)
        {
            _configurationController = configurationController;
            _mongoRepositoryBuilder = mongoRepositoryBuilder;
            Connection = connectionOptions;
        }

        public MongoBuilder WithConfiguration(Action<ConfigurationController> config)
        {
            config(_configurationController);
            return this;
        }

        public MongoBuilder WithRepositories(Action<MongoRepositoryBuilder> config)
        {
            config(_mongoRepositoryBuilder);
            
            return this;
        }
    }
}