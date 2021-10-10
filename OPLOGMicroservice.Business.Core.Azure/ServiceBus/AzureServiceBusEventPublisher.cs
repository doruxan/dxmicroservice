using OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface;
using OPLOGMicroservice.Business.Core.Interfaces;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus
{
    public class AzureServiceBusEventPublisher : IExternalEventPublisher
    {
        protected readonly IQueueSender queueSender;

        public AzureServiceBusEventPublisher(IQueueSender queueSender)
        {
            this.queueSender = queueSender;
        }

        public async Task PublishAsync(IEvent @event)
        {
            await queueSender.SendAsync(new QueueMessage { Body = @event });
        }
    }
}
