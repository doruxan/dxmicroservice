using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data
{
    [ExcludeFromCodeCoverage]
    public class DataConfiguration
    {
        public Connection Connection { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Connection
    {
        public string Server { get; set; }

        public string DB { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
