using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(IEvent @event, bool publishInternal = false);
    }
}
