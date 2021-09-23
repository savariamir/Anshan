using System.IO;

namespace Anshan.File.Abstractions
{
    public interface IStreamableFile : IFile
    {
        Stream Stream { get; }
    }
}