using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IEventProcessor
    {
        Task ProcessAsync<T>(T @event) where T : IEvent;
    }
}
