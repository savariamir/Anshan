using System.Threading.Tasks;
using Anshan.Domain;

namespace Anshan.OutboxProcessor.EventBus
{
    public interface IEventBus
    {
        Task Publish(IDomainEvent @event);

        Task Start();
    }
}