using System;

namespace Anshan.Domain.Exceptions
{
    [Serializable]
    public class SlugContainInvalidWordsException : Exception
    {
        public SlugContainInvalidWordsException(string invalidWord) :
            base($"The slug contains the invalid word '{invalidWord}'")
        {
        }
    }
}