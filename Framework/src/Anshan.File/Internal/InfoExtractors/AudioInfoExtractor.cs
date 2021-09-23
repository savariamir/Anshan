using Anshan.File.Abstractions;

namespace Anshan.File.Internal.InfoExtractors
{
    internal class AudioInfoExtractor : InfoExtractorBase<IAudioInfo>, IAudioInfoExtractor
    {
        public AudioInfoExtractor(IExtractorRegistry typeExtractorRegistry) : base(typeExtractorRegistry)
        {
        }
    }
}