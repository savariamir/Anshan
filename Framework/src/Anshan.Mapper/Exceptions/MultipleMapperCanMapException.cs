using System;

namespace Anshan.Mapper.Exceptions
{
    [Serializable]
    public class MultipleMapperCanMapException : Exception
    {
        public MultipleMapperCanMapException(Type inputType) :
            base($"Multiple mapper could handle the input type of '{inputType}'")
        {
        }
    }
}