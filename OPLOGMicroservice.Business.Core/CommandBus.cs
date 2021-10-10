using Microsoft.Extensions.DependencyInjection;
using OPLOGMicroservice.Business.Core.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core
{
    public class CommandBus : ICommandBus
    {
        protected readonly IServiceProvider serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [DebuggerStepThrough]
        public void Send<K, T>(K command, CancellationToken cancelliationToken) where K : ICommand<T>
        {
            var handler = serviceProvider.GetService<ICommandHandler<K, T>>();

            if (handler == null)
                throw new Exception(string.Format("Command handler not found for command type {0}", nameof(command)));

            command.Result = true;

            try
            {
                handler.Handle(command, cancelliationToken);
            }
            catch (Exception)
            {
                command.Result = false;
                throw;
            }
        }

        [DebuggerStepThrough]
        public async Task SendAsync<K, T>(K command, CancellationToken cancelliationToken) where K : IAsyncCommand<T>
        {
            var handler = serviceProvider.GetService<ICommandHandler<K, T>>();

            if (handler == null)
                throw new Exception(string.Format("Command handler not found for command type {0}", nameof(command)));

            if (!(handler is IAsyncCommandHandler<K, T>))
                throw new Exception(string.Format("Command handler {0} does not support async", nameof(handler)));

            command.Result = true;

            try
            {
                await (handler as IAsyncCommandHandler<K, T>).HandleAsync(command, cancelliationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                command.Result = false;
                throw;
            }
        }
    }
}
