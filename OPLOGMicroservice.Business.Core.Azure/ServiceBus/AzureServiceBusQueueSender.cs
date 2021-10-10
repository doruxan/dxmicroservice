using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface;
using OPLOGMicroservice.Business.Core.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus
{
    public class AzureServiceBusQueueSender : IQueueSender
    {
        protected IQueueClient client;
        protected readonly IConfiguration configuration;

        public AzureServiceBusQueueSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            var connectionString = configuration.GetSection("AzureServiceBus").GetSection("ConnectionString").Value;
            var queueName = configuration.GetSection("AzureServiceBus").GetSection("QueueName").Value;
            var retryPolicy = new RetryExponential(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(5), 10);
            client = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock, retryPolicy);
        }

        public async Task SendAsync(QueueMessage queueMessage)
        {
            if (queueMessage == null)
                throw new Exception("Could not sent null message.");

            queueMessage.Type = queueMessage.Body.GetType().FullName;
            queueMessage.BodyString = JsonConvert.SerializeObject(queueMessage.Body);

            var @event = queueMessage.Body as IEvent;

            var messageBody = JsonConvert.SerializeObject(queueMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                ContentType = "application/json",
                Label = nameof(@event),
                PartitionKey = @event.GetType().Name,
                MessageId = Guid.NewGuid().ToString()
            };

            await client.SendAsync(message);
        }

        public async Task CloseAsync()
        {
            await client.CloseAsync().ConfigureAwait(false);
        }
    }
}
