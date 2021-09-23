using System.IO;
using System.Threading.Tasks;

namespace Anshan.File.Abstractions
{
    public interface IImageInfoExtractor
    {
        Task<IImageInfo> ExtractAsync(string mimeType, Stream stream);
    }

    public interface IImageInfo : IFileInfo
    {
        public int Height { get; }

        public int Width { get; }

        string CameraMaker { get; }

        string CameraModel { get; }

        string Copyright { get; }

        uint? IsoSpeed { get; }
    }
}