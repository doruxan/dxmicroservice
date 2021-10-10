using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OPLOGMicroservice.Business.Core.Azure.ServiceBus.Interface;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus
{
    public class AzureServiceBusQueueReceiver : IQueueReceiver
    {
        private IQueueClient client;
        protected readonly IQueueReceivedMessageProcessor receivedMessageProcessor;
        protected readonly IConfiguration configuration;

        public AzureServiceBusQueueReceiver(IQueueReceivedMessageProcessor receivedMessageProcessor)
        {
            this.receivedMessageProcessor = receivedMessageProcessor;
        }

        public async Task InitializeAsync()
        {
            var connectionString = configuration.GetSection("AzureServiceBus").GetSection("ConnectionString").Value;
            var queueName = configuration.GetSection("AzureServiceBus").GetSection("QueueName").Value;
            var retryPolicy = new RetryExponential(TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(5), 10);
            client = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock, retryPolicy);

            var options = new MessageHandlerOptions(ExceptionReceived)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 10,
                MaxAutoRenewDuration = TimeSpan.FromSeconds(60)
            };

            client.RegisterMessageHandler(OnMessageReceivedCallback, options);
        }

        private Task ExceptionReceived(ExceptionReceivedEventArgs e)
        {
            //Check
            //Logging.Application.Log(e.Exception, Microsoft.Extensions.Logging.LogLevel.Error, null, "AzureServiceBusQueueReceiver - Options_ExceptionReceived", null);
            return Task.CompletedTask;
        }

        private async Task OnMessageReceivedCallback(Message message, CancellationToken token)
        {
            QueueMessage bodyObject = null;
            var renewCancellationTokenSource = new CancellationTokenSource();

            try
            {
                if (message.ContentType != null && message.ContentType.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
                {
                    var body = message.Body;

                    bodyObject = JsonConvert.DeserializeObject<QueueMessage>(Encoding.UTF8.GetString(body));

                    await receivedMessageProcessor.ProcessAsync(bodyObject).ConfigureAwait(false);

                    await client.CompleteAsync(message.SystemProperties.LockToken).ConfigureAwait(false);
                }
                else
                {
                    await client.DeadLetterAsync("ProcessingError", "Message could not be processed.").ConfigureAwait(false);

                    //Logging.Application.Log("AzureServiceQueueBusReceiver - OnMessageAsync - ProcessingError", JsonConvert.SerializeObject(bodyObject), Microsoft.Extensions.Logging.LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                //Logging.Application.Log(ex, Microsoft.Extensions.Logging.LogLevel.Error, null, "AzureServiceBusQueueReceiver - OnMessageAsync", JsonConvert.SerializeObject(bodyObject));

                await client.AbandonAsync(message.SystemProperties.LockToken);
            }
            finally
            {
                renewCancellationTokenSource.Cancel();
            }
        }

        public async Task CloseAsync()
        {
            await client.CloseAsync().ConfigureAwait(false);
        }
    }
}
