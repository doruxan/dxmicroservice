using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OPLOGMicroservice.API.Configuration
{
    internal static class ConfigurationBuilder
    {
        private const string EnvironmentKey = "ASPNETCORE_ENVIRONMENT";
        public static ConfigurationOptions Build()
        {
            string environment = GetEnvironmentVariable(EnvironmentKey);
            if (string.IsNullOrEmpty(environment))
            {
                throw new ArgumentNullException(environment);
            }

            ConfigurationOptions configurationOptions;
            var assembly = Assembly.GetExecutingAssembly();

            var resources = assembly.GetManifestResourceNames();
            // There should be an appsettings json for each enviroment
            var resourceName = $"{assembly.GetName().Name}.Settings.appsettings.{environment}.json";

            // Get the embedded resource to set the function configuration by enviroment
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                var configuration = reader.ReadToEnd();
                if (string.IsNullOrEmpty(configuration))
                {
                    throw new Exception($"Could not load configuration file. ({resourceName})");
                }

                configurationOptions = JsonConvert.DeserializeObject<ConfigurationOptions>(configuration);
            }

            if (configurationOptions == null) throw new ArgumentNullException(nameof(configurationOptions));

            return configurationOptions;
        }


        private static string GetEnvironmentVariable(string name, bool nullable = false)
        {
            string envVar = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
            if (!nullable && string.IsNullOrWhiteSpace(envVar))
            {
                throw new ArgumentNullException(name);
            }

            return envVar;
        }

        private static List<string> GenerateListFromString(string propName)
        {
            string value = GetEnvironmentVariable(propName);
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] values = value.Split(',');

                if (values == null || values.Length == 0)
                {
                    throw new ArgumentNullException($"{propName}: {values} is not configured correctly!");
                }

                return values.ToList();
            }

            return new List<string>();
        }
    }
}
