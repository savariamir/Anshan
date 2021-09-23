using System.IO;
using SixLabors.ImageSharp.PixelFormats;

namespace Anshan.Image
{
    public interface IBlurHashGenerator
    {
        string Generate(Stream imageData);
    }

    internal class BlurHashGenerator : IBlurHashGenerator
    {
        public string Generate(Stream imageData)
        {
            var encoder = new ImageSharpEncoder();
            
            var image = SixLabors.ImageSharp.Image.Load<Rgb24>(imageData);

            // x and y components = 3, higher will give better detail, but longer hashes
            return encoder.Encode(image, 3, 3);
        }
    }
}