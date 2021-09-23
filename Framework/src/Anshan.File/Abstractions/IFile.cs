using System;

namespace Anshan.File.Abstractions
{
    public interface IFile
    {
        string Id { get; }

        string MimeType { get; }

        string FileName { get; }

        DateTime UploadedAt { get; }

        public string Md5 { get; }
    }
}