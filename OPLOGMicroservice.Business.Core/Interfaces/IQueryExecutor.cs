using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Interfaces
{
    public interface IQueryExecutor<TQuery, TResult> where TQuery : class, IQuery<TResult> where TResult : class
    {
        /// <summary>
        /// If called 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        TResult Execute(TQuery query, CancellationToken cancellationToken);
    }

    public interface IAsyncQueryExecutor<TQuery, TResult> : IQueryExecutor<TQuery, TResult> where TQuery : class, IQuery<TResult> where TResult : class
    {
        Task<TResult> ExecuteAsync(TQuery query, CancellationToken cancellationToken);
    }
}
