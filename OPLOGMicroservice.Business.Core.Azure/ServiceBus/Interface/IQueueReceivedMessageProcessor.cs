using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface
{
    public interface IQueueReceivedMessageProcessor
    {
        Task ProcessAsync(QueueMessage message);
    }
}
