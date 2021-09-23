using Anshan.File.Abstractions;

namespace Anshan.File.Internal.Models
{
    internal class ImageInfo : FileInfo, IImageInfo
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public string CameraMaker { get; set; }

        public string CameraModel { get; set; }

        public string Copyright { get; set; }

        public uint? IsoSpeed { get; set; }
    }
}