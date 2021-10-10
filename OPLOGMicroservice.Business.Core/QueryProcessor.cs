using OPLOGMicroservice.Business.Core.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core
{
    public class QueryProcessor : IQueryProcessor
    {
        protected readonly IServiceProvider serviceProvider;

        public QueryProcessor(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [DebuggerStepThrough]
        public TResult Process<TResult>(IQuery<TResult> query, CancellationToken cancelliationToken) where TResult : class
        {
            var handlerType = typeof(IQueryExecutor<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var executor = serviceProvider.GetService(handlerType);

            if (executor == null)
                throw new Exception(string.Format("Query executor not found for query type {0}", query.GetType().Name));

            return (executor as dynamic).Execute(query as dynamic, cancelliationToken);

        }

        [DebuggerStepThrough]
        public async Task<TResult> ProcessAsync<TResult>(IAsyncQuery<TResult> query, CancellationToken cancelliationToken) where TResult : class
        {
            var handlerType = typeof(IAsyncQueryExecutor<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var executor = serviceProvider.GetService(handlerType);

            if (executor == null)
                throw new Exception(string.Format("Query executor not found for query type {0}", query.GetType().Name));

            return await (executor as dynamic).ExecuteAsync(query as dynamic, cancelliationToken);
        }
    }
}
