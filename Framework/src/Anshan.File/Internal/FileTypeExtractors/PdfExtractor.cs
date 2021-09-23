using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Anshan.File.Abstractions;
using Anshan.File.Internal.Models;

namespace Anshan.File.Internal.FileTypeExtractors
{
    public class PdfExtractor : IFileTypeExtractor<IDocumentInfo>
    {
        public bool CanExtract(string mimeType)
        {
            return mimeType.Equals(MimeTypes.Application.Pdf, StringComparison.OrdinalIgnoreCase);
        }

        public Task<IDocumentInfo> ExtractAsync(Stream stream,
                                                string mimeType,
                                                CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new DocumentInfo() as IDocumentInfo);
        }
    }
}