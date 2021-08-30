using Anshan.Mongo.Internal.Configuration;
using Anshan.Mongo.Internal.Configuration.CommonConfigurations;

namespace Anshan.Mongo.Extensions
{
    public static class ConfigurationControllerExtensions
    {
        public static ConfigurationController RegisterCommonValueObjectSerializer(this ConfigurationController configurationController)
        {
            configurationController.Register<CommonValueObjectConfiguration>();

            return configurationController;
        }
    }
}