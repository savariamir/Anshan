using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Anshan.File.Extensions
{
    public static class MimeTypeExtensions
    {
        public static bool IsMimeType(this string input)
        {
            return Regex.IsMatch(input, @"^[-\w.]+\/[-\w.+]+$");
        }

        public static bool IsImageMimeType(this string input)
        {
            return MimeTypes.Image.All.Any(m => m.Equals(input, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsVideoMimeType(this string input)
        {
            return MimeTypes.Video.All.Any(m => m.Equals(input, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsAudioMimeType(this string input)
        {
            return MimeTypes.Audio.All.Any(m => m.Equals(input, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsDocumentMimeType(this string input)
        {
            return input.Equals(MimeTypes.Application.Pdf, StringComparison.OrdinalIgnoreCase) ||
                   input.Equals(MimeTypes.Application.MicrosoftWordOpenXml, StringComparison.OrdinalIgnoreCase) ||
                   input.Equals(MimeTypes.Application.MicrosoftWord, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsNotCommonMimeType(this string input)
        {
            return !(input.IsAudioMimeType() ||
                     input.IsImageMimeType() ||
                     input.IsAudioMimeType() ||
                     input.IsVideoMimeType());
        }
    }
}