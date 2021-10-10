using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IExternalEventPublisher
    {
        Task PublishAsync(IEvent @event);
    }
}
