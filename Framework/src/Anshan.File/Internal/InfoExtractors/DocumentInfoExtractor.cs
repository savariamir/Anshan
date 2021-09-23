using Anshan.File.Abstractions;

namespace Anshan.File.Internal.InfoExtractors
{
    internal class DocumentInfoExtractor : InfoExtractorBase<IDocumentInfo>, IDocumentInfoExtractor
    {
        public DocumentInfoExtractor(IExtractorRegistry typeExtractorRegistry) : base(typeExtractorRegistry)
        {
        }
    }
}