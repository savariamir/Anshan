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
    public class CommonVideoTypesInfoExtractor : IFileTypeExtractor<IVideoInfo>
    {
        public bool CanExtract(string mimeType)
        {
            return mimeType.Equals(MimeTypes.Video.Mp4, StringComparison.OrdinalIgnoreCase);
        }

        Task<IVideoInfo> IFileTypeExtractor<IVideoInfo>.ExtractAsync(Stream stream,
                                                                     string mimeType,
                                                                     CancellationToken cancellationToken)
        {
            using var videoFile = TagLib.File.Create(new StreamFile(stream), mimeType, ReadStyle.Average);
            
            IVideoInfo result = new VideoInfo
            {
                Duration = videoFile.Properties.Duration,
                Height = videoFile.Properties.VideoHeight,
                Width = videoFile.Properties.VideoWidth,
            };

            return Task.FromResult(result);
        }
    }
}