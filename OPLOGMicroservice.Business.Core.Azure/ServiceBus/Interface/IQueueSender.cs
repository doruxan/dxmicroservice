using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface
{
    public interface IQueueSender
    {
        Task InitializeAsync();
        Task SendAsync(QueueMessage message);
        Task CloseAsync();
    }
}
