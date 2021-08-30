using System.IO;
using System.Threading.Tasks;
using Anshan.File;
using Anshan.File.Internal.InfoExtractors;
using Anshan.File.Internal.Registry;
using FluentAssertions;
using Xunit;

namespace Anshan.Decoder.UnitTests
{
    public class VideoInfoExtractorTests
    {
        [Fact]
        public async Task mp4_info_should_be_extracted_correctly()
        {
            // arrange
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "mp4-sample.mp4"));
            VideoInfoExtractor extractor = new VideoInfoExtractor(ExtractorRegistryAccessor.Registry);

            // act
            var videoInfo = await extractor.ExtractAsync(MimeTypes.Video.Mp4, stream);

            // assert
            videoInfo.Width.Should().Be(480);
            videoInfo.Height.Should().Be(270);
            videoInfo.Duration.Seconds.Should().Be(30);
        }

        [Fact]
        public async Task mp4_info_should_be_extracted_correctly_2()
        {
            // arrange
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "mp4-sample-2.mp4"));
            VideoInfoExtractor extractor = new VideoInfoExtractor(ExtractorRegistryAccessor.Registry);

            // act
            var videoInfo  = await extractor.ExtractAsync(MimeTypes.Video.Mp4, stream);

            // assert
            videoInfo.Width.Should().Be(190);
            videoInfo.Height.Should().Be(240);
            videoInfo.Duration.Seconds.Should().Be(4);
        }
    }
}