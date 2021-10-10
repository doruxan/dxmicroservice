using OPLOGMicroservice.Business.Core.Interfaces;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core
{
    public class EventPublisher : IEventPublisher
    {
        protected IEventProcessor eventProcessor;
        protected IExternalEventPublisher externalEventPublisher;

        public EventPublisher(IEventProcessor eventProcessor, IExternalEventPublisher externalEventPublisher)
        {
            this.eventProcessor = eventProcessor;
            this.externalEventPublisher = externalEventPublisher;
        }

        public async Task PublishAsync(IEvent @event, bool publishInternal = false)
        {
            if (publishInternal)
            {
                await eventProcessor.ProcessAsync(@event);
                return;
            }

            await externalEventPublisher.PublishAsync(@event);
        }
    }
}
