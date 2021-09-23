using System;
using System.IO;
using System.Threading.Tasks;

namespace Anshan.File.Abstractions
{
    public interface IVideoInfoExtractor
    {
        Task<IVideoInfo> ExtractAsync(string mimeType, Stream stream);
    }
    
    public interface IVideoInfo : IFileInfo
    {
        public int Height { get; }

        public int Width { get; }

        public TimeSpan Duration { get; }
    }
}