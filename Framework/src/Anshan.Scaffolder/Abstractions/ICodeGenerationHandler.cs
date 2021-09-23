using System.Collections.Generic;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Abstractions
{
    internal interface ICodeGenerationHandler
    {
        IEnumerable<OutputFile> Handle(DomainContext context);
    }
}