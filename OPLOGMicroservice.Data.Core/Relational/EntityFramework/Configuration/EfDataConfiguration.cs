using System.Diagnostics.CodeAnalysis;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Configuration
{
    [ExcludeFromCodeCoverage]
    public class EfDataConfiguration
    {
        public EfConnection Connection { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class EfConnection
    {
        public string Server { get; set; }

        public string DB { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
