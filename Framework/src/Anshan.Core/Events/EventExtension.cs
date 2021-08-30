using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Core.Events
{
    public static class EventExtension
    {
        public static void AddEventAggregator(this IServiceCollection services)
        {
            var eventAggregator = new EventAggregator();
            services.AddSingleton<IEventListener>(eventAggregator);
            services.AddSingleton<IEventPublisher>(eventAggregator);
        }
    }
}