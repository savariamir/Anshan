using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Anshan.File.Abstractions
{
    public interface IFileTypeExtractor
    {
        bool CanExtract(string mimeType);

        Task<IFileInfo> ExtractAsync(Stream stream, string mimeType, CancellationToken cancellationToken = default);
    }

    public interface IFileTypeExtractor<TOutputInfo> : IFileTypeExtractor where TOutputInfo : IFileInfo
    {
        async Task<IFileInfo> IFileTypeExtractor.ExtractAsync(Stream stream, string mimeType,
                                                              CancellationToken cancellationToken = default) =>
            await ExtractAsync(stream, mimeType, cancellationToken);

        new Task<TOutputInfo> ExtractAsync(Stream stream, string mimeType,
                                           CancellationToken cancellationToken = default);
    }
}