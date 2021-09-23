using Anshan.File.Abstractions;
using Anshan.File.Internal.FileTypeExtractors;

namespace Anshan.File.Internal.Registry
{
    public static class ExtractorRegistryAccessor
    {
        public static readonly IExtractorRegistry Registry;

        static ExtractorRegistryAccessor()
        {
            Registry = new ExtractorRegistry();
            Registry.Register(new CommonImageExtractor());
            Registry.Register(new CommonAudioTypesInfoExtractor());
            Registry.Register(new CommonVideoTypesInfoExtractor());
            Registry.Register(new PdfExtractor());
        }
    }
}