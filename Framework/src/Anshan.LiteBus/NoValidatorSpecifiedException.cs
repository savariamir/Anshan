using System;

namespace Anshan.LiteBus
{
    [Serializable]
    public class NoValidatorSpecifiedException : Exception
    {
        public NoValidatorSpecifiedException() : base("No validator has been found.")
        {
        }
    }
}