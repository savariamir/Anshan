using System;

namespace Anshan.Domain.Exceptions
{
    [Serializable]
    public class SlugIsNotInEnglishException : Exception
    {
        public SlugIsNotInEnglishException() :
            base("The slug can only contain english characters, numbers and dash character")
        {
        }
    }
}