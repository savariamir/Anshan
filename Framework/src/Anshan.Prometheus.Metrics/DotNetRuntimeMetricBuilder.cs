using System;
using Prometheus.DotNetRuntime;

namespace Anshan.Prometheus.Metrics
{
    public static class DotNetRuntimeMetricBuilder
    {
        public static IDisposable CreateCollector()
        {
            return DotNetRuntimeStatsBuilder.Customize()
                                            .WithContentionStats(CaptureLevel.Informational)
                                            .WithGcStats(CaptureLevel.Verbose)
                                            .WithThreadPoolStats(CaptureLevel.Informational)
                                            .WithExceptionStats(CaptureLevel.Errors)
                                            .WithJitStats()
                                            .RecycleCollectorsEvery(TimeSpan.FromDays(1))
                                            .StartCollecting();
        }
    }
}