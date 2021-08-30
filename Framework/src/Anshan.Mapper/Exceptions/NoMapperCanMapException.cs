using System;

namespace Anshan.Mapper.Exceptions
{
    [Serializable]
    public class NoMapperCanMapException : Exception
    {
        public NoMapperCanMapException(Type inputType) :
            base($"No mapper could handle the input type of '{inputType}'")
        {
        }
    }
}