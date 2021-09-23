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
    public class CommonImageExtractor : IFileTypeExtractor<IImageInfo>
    {
        public bool CanExtract(string mimeType)
        {
            return mimeType.Equals(MimeTypes.Image.Jpeg, StringComparison.OrdinalIgnoreCase) ||
                   mimeType.Equals(MimeTypes.Image.Png, StringComparison.OrdinalIgnoreCase) ||
                   mimeType.Equals(MimeTypes.Image.Bmp, StringComparison.OrdinalIgnoreCase) ||
                   mimeType.Equals(MimeTypes.Image.Gif, StringComparison.OrdinalIgnoreCase) ||
                   mimeType.Equals(MimeTypes.Image.Tga, StringComparison.OrdinalIgnoreCase);
        }

        public Task<IImageInfo> ExtractAsync(Stream stream,
                                             string mimeType,
                                             CancellationToken cancellationToken)
        {
            var imageFile = TagLib.File.Create(new StreamFile(stream), mimeType, ReadStyle.Average);
            var imageTag = imageFile.Tag as TagLib.Image.CombinedImageTag;
            
            var imageInfo = new ImageInfo
            {
                Height = imageFile.Properties.PhotoHeight,
                Width = imageFile.Properties.PhotoWidth,
            };

            if (imageTag is not null)
            {
                imageInfo.CameraMaker = imageTag.Make;
                imageInfo.CameraModel = imageTag.Model;
                imageInfo.Copyright = imageTag.Copyright;
                imageInfo.IsoSpeed = imageTag.ISOSpeedRatings;
            }
            
            return Task.FromResult((IImageInfo) imageInfo);
        }
    }
}