using System.Threading;

namespace Anshan.Mongo.Internal.Configuration
{
    public class ThreadSafeSingleShotGuard
    {
        private static int NOTCALLED = 0;
        private static int CALLED = 1;
        private int _state = NOTCALLED;

        /// <summary>Explicit call to check and set if this is the first call</summary>
        public bool CheckAndSetFirstCall => Interlocked.Exchange(ref _state, CALLED) == NOTCALLED;
    }
}