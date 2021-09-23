using System;
using Anshan.File.Abstractions;

namespace Anshan.File.Internal.Models
{
    internal class AudioInfo : FileInfo, IAudioInfo
    {
        public TimeSpan Duration { get; set; }
    }
}