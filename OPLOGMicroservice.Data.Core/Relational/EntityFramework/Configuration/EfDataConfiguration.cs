using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
