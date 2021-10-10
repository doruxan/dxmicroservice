using OPLOGMicroservice.Business.Core.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core
{
    public class EventProcessor : IEventProcessor
    {
        protected readonly IServiceProvider serviceProvider;

        public EventProcessor(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [DebuggerStepThrough]
        public async Task ProcessAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlerGeneric = typeof(IEventHandler<>);
            Type[] typeArgs = { @event.GetType() };
            var handlerType = handlerGeneric.MakeGenericType(typeArgs);

            var handler = serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new Exception(string.Format("Event handler not found for event type {0}", nameof(@event)));

            await ((dynamic)handler).HandleAsync(@event as dynamic);
        }
    }
}
