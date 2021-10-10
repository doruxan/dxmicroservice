using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface
{
    public interface IQueueReceiver
    {
        Task InitializeAsync();
        Task CloseAsync();
    }
}
