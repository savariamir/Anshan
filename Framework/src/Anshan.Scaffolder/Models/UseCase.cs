using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Anshan.Scaffolder.Models
{
    public class UseCase
    {
        public UseCase(MethodInfo methodInfo)
        {
            Title = new MultiStyleText(methodInfo.Name);
            Arguments = methodInfo.GetParameters().Select(p => new Property(p));
        }

        public MultiStyleText Title { get; }

        public IEnumerable<Property> Arguments { get; }

        public string Signature { get; private set; }

        public string UnitTestSignature
        {
            get
            {
                var args = Arguments.Select(a => $"Arg.Any<{a.Type.Name}>()");

                return string.Join(", ", args);
            }
        }

        public void AddPostfix(string postfix)
        {
            foreach (var argument in Arguments) argument.AddPostfix(postfix);
        }


        public void SetSignature(string objectNameToPrefix)
        {
            var signature = Arguments.Select(a => objectNameToPrefix + "." + a.Title.InPascalCase);

            Signature = string.Join(',', signature);
        }
    }
}