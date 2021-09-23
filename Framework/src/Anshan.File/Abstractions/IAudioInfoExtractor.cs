using System;
using System.IO;
using System.Threading.Tasks;

namespace Anshan.File.Abstractions
{
    public interface IAudioInfoExtractor
    {
        Task<IAudioInfo> ExtractAsync(string mimeType, Stream stream);
    }
    
    public interface IAudioInfo : IFileInfo
    {
        TimeSpan Duration { get; }
    }
}