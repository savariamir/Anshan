using Coravel;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Worker.Coravel
{
    public static class WorkerExtension
    {
        public static IServiceCollection AddCoravel(this IServiceCollection services)
        {
            services.AddScheduler();
            services.AddQueue();
            services.AddSingleton<MetricReporter>();
            services.AddSingleton<IWorkerProxy, WorkerProxy>();
            
            return services;
        }
    }
}