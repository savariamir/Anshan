using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Anshan.File.Abstractions
{
    //TODO: break down this service, it has too much responsibilities (upload, download, stream and query)
    public interface IFileService
    {
        Task UploadAsync(string id, string fileName, string mimeType, byte[] content);

        Task UploadAsync(string id, string fileName, string mimeType, Stream content);

        Task<IStreamableFile> StreamByNameAsync(string fileName, CancellationToken cancellationToken = default);

        Task<IStreamableFile> StreamAsync(string id, CancellationToken cancellationToken = default);

        IAsyncEnumerable<IFile> StreamFilesAsync();

        Task<bool> DeleteAsync(string nameOrId);

        Task<IFile> GetFileInfoAsync(string identifier);
    }
}