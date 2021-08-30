using System.IO;
using System.Threading.Tasks;
using Anshan.File;
using Anshan.File.Internal.InfoExtractors;
using Anshan.File.Internal.Registry;
using FluentAssertions;
using Xunit;

namespace Anshan.Decoder.UnitTests
{
    public class AudioInfoExtractorTests
    {
        [Fact]
        public async Task mp3_info_should_be_extracted_correctly()
        {
            // arrange
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "mp3-sample.mp3"));
            AudioInfoExtractor extractor = new AudioInfoExtractor(ExtractorRegistryAccessor.Registry);

            // act
            var audioInfo = await extractor.ExtractAsync(MimeTypes.Audio.Mp3, stream);

            // assert
            audioInfo.Duration.Seconds.Should().Be(27);
        }
        
        [Fact]
        public async Task mp3_info_should_be_extracted_correctly_2()
        {
            // arrange
            var stream = System.IO.File.OpenRead(Path.Combine("Files", "mp3-sample-2.mp3"));
            AudioInfoExtractor extractor = new AudioInfoExtractor(ExtractorRegistryAccessor.Registry);

            // act
            var audioInfo = await extractor.ExtractAsync(MimeTypes.Audio.Mp3, stream);

            // assert
            audioInfo.Duration.Seconds.Should().Be(54);
            audioInfo.Duration.Minutes.Should().Be(1);
        }
    }
}