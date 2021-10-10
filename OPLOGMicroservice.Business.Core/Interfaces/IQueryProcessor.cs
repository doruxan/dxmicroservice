using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IQueryProcessor
    {
        /// <summary>
        /// This method could cause deadlocks. If you have to use it, please be careful. Consider using ProcessAsync with IAsyncQuery objects. 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        TResult Process<TResult>(IQuery<TResult> query, CancellationToken cancellationToken) where TResult : class;

        Task<TResult> ProcessAsync<TResult>(IAsyncQuery<TResult> query, CancellationToken cancellationToken) where TResult : class;
    }
}
