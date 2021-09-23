using System.Collections.Generic;

namespace Anshan.File.Abstractions
{
    public interface IExtractorRegistry : IReadOnlyCollection<IFileTypeExtractor>
    {
        void Register(IFileTypeExtractor fileTypeExtractor);
    }
}