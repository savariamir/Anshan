using System.Collections;
using System.Collections.Generic;
using Anshan.File.Abstractions;

namespace Anshan.File.Internal.Registry
{
    public class ExtractorRegistry : IExtractorRegistry
    {
        private readonly HashSet<IFileTypeExtractor> _extractors = new();

        public IEnumerator<IFileTypeExtractor> GetEnumerator() => _extractors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _extractors.Count;

        public void Register(IFileTypeExtractor fileTypeExtractor) => _extractors.Add(fileTypeExtractor);
    }
}