using System.IO;
using System.Threading.Tasks;

namespace Anshan.File.Abstractions
{
    public interface IDocumentInfoExtractor
    {
        Task<IDocumentInfo> ExtractAsync(string mimeType, Stream stream);
    }
    
    public interface IDocumentInfo : IFileInfo
    {
    }
}