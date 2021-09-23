using System.Threading.Tasks;
using Anshan.Domain;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Anshan.OutboxProcessor.EventBus.MassTransit
{
    public class MassTransitBusAdapter : IEventBus
    {
        private readonly MassTransitConfig _massTransitConfig;
        private IBusControl _bus;
        private bool _isStarted;

        public MassTransitBusAdapter(IOptions<MassTransitConfig> config)
        {
            _massTransitConfig = config.Value;
            _isStarted = false;
        }

        public Task Publish(IDomainEvent @event)
        {
            if (!_isStarted) throw new BusNotStartedException();
            return _bus.Publish((dynamic) @event);
        }

        public async Task Start()
        {
            if (!_isStarted)
            {
                _bus = Bus.Factory.CreateUsingRabbitMq(
                                                       sbc =>
                                                       {
                                                           sbc.Host(_massTransitConfig.RabbitMqConnectionString);
                                                       });
                await _bus.StartAsync();
                _isStarted = true;
            }
        }
    }
}