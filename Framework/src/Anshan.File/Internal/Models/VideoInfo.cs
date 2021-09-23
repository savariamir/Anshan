using System;
using Anshan.File.Abstractions;

namespace Anshan.File.Internal.Models
{
    internal class VideoInfo : FileInfo, IVideoInfo
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public TimeSpan Duration { get; set; }
    }
}