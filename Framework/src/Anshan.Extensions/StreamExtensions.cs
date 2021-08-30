using System.IO;
using System.Threading.Tasks;

namespace Anshan.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ToBytesAsync(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            
            var ms = new MemoryStream();

            await stream.CopyToAsync(ms);

            return ms.ToArray();
        }
    }
}