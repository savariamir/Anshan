using Anshan.File.Abstractions;
using Anshan.File.Internal.InfoExtractors;
using Anshan.File.Internal.Registry;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.File
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExtractors(this IServiceCollection services)
        {
            services.AddTransient<IVideoInfoExtractor, VideoInfoExtractor>();
            services.AddTransient<IAudioInfoExtractor, AudioInfoExtractor>();
            services.AddTransient<IDocumentInfoExtractor, DocumentInfoExtractor>();
            services.AddTransient<IImageInfoExtractor, ImageInfoExtractor>();
            services.AddSingleton<IExtractorRegistry>(ExtractorRegistryAccessor.Registry);
            
            return services;
        }
    }
}