using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync(string topic, IEvent @event, CancellationToken cancellationToken);
    }
}
