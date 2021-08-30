using System.IO;
using System.Threading.Tasks;
using Anshan.File;
using Anshan.File.Internal.InfoExtractors;
using Anshan.File.Internal.Registry;
using FluentAssertions;
using Xunit;

namespace Anshan.Decoder.UnitTests
{
    public class ImageInfoExtractorTests
    {
        [Fact(Skip = "Bmp is not supported yet")]
        public async Task Bmp_should_be_decoded_correctly()
        {
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "bmp-sample-512w-512h.bmp"));

            var extractor = new ImageInfoExtractor(ExtractorRegistryAccessor.Registry);

            var result = await extractor.ExtractAsync(MimeTypes.Image.Bmp, stream);

            result.Should().NotBeNull();
            result.Height.Should().Be(512);
            result.Width.Should().Be(512);
        }

        [Fact]
        public async Task Jpeg_should_be_decoded_correctly()
        {
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "jpg-sample-313w-234h.jpg"));

            var extractor = new ImageInfoExtractor(ExtractorRegistryAccessor.Registry);

            var result = await extractor.ExtractAsync(MimeTypes.Image.Jpeg, stream);

            result.Should().NotBeNull();
            result.Width.Should().Be(313);
            result.Height.Should().Be(234);
        }
        
        [Fact]
        public async Task Jpeg_should_be_decoded_correctly_2()
        {
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "jpg-sample-1920w-1280h.jpg"));

            var extractor = new ImageInfoExtractor(ExtractorRegistryAccessor.Registry);

            var result = await extractor.ExtractAsync(MimeTypes.Image.Jpeg, stream);

            result.Should().NotBeNull();
            result.Width.Should().Be(1920);
            result.Height.Should().Be(1280);
        }

        [Fact]
        public async Task Png_should_be_decoded_correctly()
        {
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "png-sample-300w-300h.png"));

            var extractor = new ImageInfoExtractor(ExtractorRegistryAccessor.Registry);

            var result = await extractor.ExtractAsync(MimeTypes.Image.Png, stream);

            result.Should().NotBeNull();
            result.Width.Should().Be(300);
            result.Height.Should().Be(300);
        }

        [Fact(Skip = "Webp not supported yet")]
        public async Task Webp_should_be_decoded_correctly()
        {
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "webp-sample-550w-404h.webp"));

            var extractor = new ImageInfoExtractor(ExtractorRegistryAccessor.Registry);

            var result = await extractor.ExtractAsync(MimeTypes.Image.Webp, stream);

            result.Should().NotBeNull();
            result.Width.Should().Be(550);
            result.Height.Should().Be(404);
        }
    }
}