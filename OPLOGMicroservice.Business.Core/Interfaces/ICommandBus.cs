using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface ICommandBus
    {
        void Send<K, T>(K command, CancellationToken cancellationToken) where K : ICommand<T>;
        Task SendAsync<K, T>(K command, CancellationToken cancellationToken) where K : IAsyncCommand<T>;
    }
}
