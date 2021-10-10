using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IEventHandler<T> where T : IEvent
    {
        void Handle(T @event, CancellationToken cancellationToken);
    }

    public interface IAsyncEventHandler<T> : IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T @event, CancellationToken cancellationToken);
    }
}
