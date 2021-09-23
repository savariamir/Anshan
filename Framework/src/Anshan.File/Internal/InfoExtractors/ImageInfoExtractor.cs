using Anshan.File.Abstractions;

namespace Anshan.File.Internal.InfoExtractors
{
    internal class ImageInfoExtractor : InfoExtractorBase<IImageInfo>, IImageInfoExtractor 
    {
        public ImageInfoExtractor(IExtractorRegistry typeExtractorRegistry) : base(typeExtractorRegistry)
        {
        }
    }
}