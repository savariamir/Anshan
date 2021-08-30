using System.IO;

namespace Anshan.Extensions
{
    public static class ByteArrayExtensions
    {
        public static MemoryStream ToMemoryStream(this byte[] source)
        {
            return new(source);
        }
    }
}