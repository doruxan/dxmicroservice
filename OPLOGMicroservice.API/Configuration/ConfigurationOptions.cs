using OPLOGMicroservice.Auth;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Configuration;
using System.Collections.Generic;

namespace OPLOGMicroservice.API.Configuration
{
    public class ConfigurationOptions
    {

        public List<string> ClientOriginHostNames { get; set; }
        public Auth0Options Auth0Options { get; set; }

        public EfDataConfiguration EfDataConfiguration { get; set; }
    }
}
