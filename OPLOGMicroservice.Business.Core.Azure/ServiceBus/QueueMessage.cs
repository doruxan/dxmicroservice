using System.Text.Json.Serialization;

namespace OPLOGMicroservice.Business.Core.Azure.ServiceBus
{
    public class QueueMessage
    {
        [JsonIgnore]
        public object Body { get; set; }
        public string BodyString { get; set; }
        public string Type { get; set; }
    }
}
