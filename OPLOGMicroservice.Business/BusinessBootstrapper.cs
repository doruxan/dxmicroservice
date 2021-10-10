using Microsoft.Extensions.DependencyInjection;
using OPLOGMicroservice.Business.Core;
using OPLOGMicroservice.Business.Core.Interfaces;
using System.Linq;
using System.Reflection;

namespace OPLOGMicroservice.Business
{
    public static class BusinessBootstrapper
    {
        public static void RegisterBusinessDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICommandBus, CommandBus>();
            serviceCollection.AddTransient<IQueryProcessor, QueryProcessor>();
            RegisterQueryExecutors(serviceCollection);
            RegisterCommandHandlers(serviceCollection);
            RegisterEventHandlers(serviceCollection);
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            var handlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.GetInterfaces()
                .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))).ToList();

            foreach (var handler in handlers)
            {
                var interfaces = handler.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    serviceCollection.AddTransient(@interface, handler);
                }
            }
        }

        private static void RegisterQueryExecutors(IServiceCollection serviceCollection)
        {
            var executors = Assembly.GetExecutingAssembly().GetTypes()
              .Where(x => x.GetInterfaces()
                  .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IAsyncQueryExecutor<,>))).ToList();

            foreach (var executor in executors)
            {
                var interfaces = executor.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    serviceCollection.AddTransient(@interface, executor);
                }
            }
        }

        public static void RegisterEventHandlers(IServiceCollection serviceCollection)
        {
            var handlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.GetInterfaces()
                .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>))).ToList();

            foreach (var handler in handlers)
            {
                var interfaces = handler.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    serviceCollection.AddTransient(@interface, handler);
                }
            }
        }
    }
}
