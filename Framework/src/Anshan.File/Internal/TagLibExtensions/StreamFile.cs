using System;
using System.IO;

namespace Anshan.File.Internal.TagLibExtensions
{
    public class StreamFile : TagLib.File.IFileAbstraction
    {
        private readonly Stream _stream;
        private readonly bool _dispose;

        public StreamFile(Stream stream, bool dispose = false)
        {
            Name = Guid.NewGuid().ToString();
            _stream = stream;
            _dispose = dispose;
            ReadStream = stream;
            WriteStream = stream;
        }

        public void CloseStream(Stream stream)
        {
            if (_dispose)
            {
                stream.Close();
            }
        }

        public string Name { get; }

        public Stream ReadStream { get; }

        public Stream WriteStream { get; }
    }
}