using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface ICommandHandler<K, T> where K : ICommand<T>
    {
        void Handle(K command, CancellationToken cancellationToken);
    }

    public interface IAsyncCommandHandler<K, T> : ICommandHandler<K, T> where K : ICommand<T>
    {
        Task HandleAsync(K command, CancellationToken cancellationToken);
    }
}
