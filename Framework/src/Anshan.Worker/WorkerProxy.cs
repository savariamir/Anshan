using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Anshan.Worker
{
    public class WorkerProxy : IWorkerProxy
    {
        private Stopwatch _stopwatch;
        private readonly MetricReporter _reporter;

        public WorkerProxy(MetricReporter reporter)
        {
            _reporter = reporter;
        }

        protected virtual void Before()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public async Task Execute(string name, Func<Task> func)
        {
            Before();
            await func();
            After(name);
        }

        protected virtual void After(string name)
        {
            _stopwatch.Stop();
            _reporter.Report(name, _stopwatch.Elapsed.Milliseconds);
        }
    }
}