using Newtonsoft.Json;
using OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface;
using OPLOGMicroservice.Business.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus
{
    public class QueueReceivedMessageProcessor : IQueueReceivedMessageProcessor
    {
        protected readonly IEventProcessor eventProcessor;

        public QueueReceivedMessageProcessor(IEventProcessor eventProcessor)
        {
            this.eventProcessor = eventProcessor;
        }

        public async Task ProcessAsync(QueueMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Type))
            {
                return;
            }

            var type = GetType(message.Type);

            if (typeof(IEvent).IsAssignableFrom(type))
            {
                var obj = JsonConvert.DeserializeObject(message.BodyString, type);
                var eventObject = obj as IEvent;
                await eventProcessor.ProcessAsync(eventObject);
            }
        }

        public Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);

            if (type != null)
                return type;

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);

                if (type != null)
                    return type;
            }

            return null;
        }
    }
}
