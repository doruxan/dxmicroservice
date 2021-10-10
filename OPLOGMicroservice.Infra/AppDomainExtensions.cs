using System;
using System.Linq;
using System.Reflection;

namespace OPLOGMicroservice.Infra
{
    public static class AppDomainExtensions
    {
        public static Assembly GetAssemblyByName(this AppDomain domain, string assemblyName)
        {
            var asss = domain.GetAssemblies();
            return domain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyName);
        }
    }
}
