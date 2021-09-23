using System;
using System.IO;
using System.Threading.Tasks;
using Anshan.File.Abstractions;

namespace Anshan.File.Internal.InfoExtractors
{
    public abstract class InfoExtractorBase<TOutputInfo> where TOutputInfo : class, IFileInfo
    {
        private readonly IExtractorRegistry _typeExtractorRegistry;
        
        public InfoExtractorBase(IExtractorRegistry typeExtractorRegistry)
        {
            _typeExtractorRegistry = typeExtractorRegistry;
        }

        public virtual async Task<TOutputInfo> ExtractAsync(string mimeType, Stream stream)
        {
            foreach (var fileExtractor in _typeExtractorRegistry)
            {
                if (fileExtractor.CanExtract(mimeType))
                {
                    return await fileExtractor.ExtractAsync(stream, mimeType) as TOutputInfo;
                }
            }

            throw new NotSupportedException("This type of file is not supported!");
        }
    }
}