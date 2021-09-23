using Anshan.File.Abstractions;

namespace Anshan.File.Internal.InfoExtractors
{
    internal class VideoInfoExtractor : InfoExtractorBase<IVideoInfo>, IVideoInfoExtractor
    {
        public VideoInfoExtractor(IExtractorRegistry typeExtractorRegistry) : base(typeExtractorRegistry)
        {
        }
    }
}