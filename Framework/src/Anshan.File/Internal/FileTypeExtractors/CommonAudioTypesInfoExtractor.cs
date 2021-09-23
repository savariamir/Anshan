using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Anshan.File.Abstractions;
using Anshan.File.Internal.Models;
using Anshan.File.Internal.TagLibExtensions;
using TagLib;

namespace Anshan.File.Internal.FileTypeExtractors
{
    public class CommonAudioTypesInfoExtractor : IFileTypeExtractor<IAudioInfo>
    {
        public bool CanExtract(string mimeType)
        {
            return mimeType.Equals(MimeTypes.Audio.Mp3, StringComparison.OrdinalIgnoreCase);
        }

        public Task<IAudioInfo> ExtractAsync(Stream stream,
                                             string mimeType,
                                             CancellationToken cancellationToken)
        {
            using var audioFile = TagLib.File.Create(new StreamFile(stream), mimeType, ReadStyle.Average);
            
            IAudioInfo audioInfo = new AudioInfo
            {
                Duration = audioFile.Properties.Duration,
            };

            return Task.FromResult(audioInfo);
        }
    }
}