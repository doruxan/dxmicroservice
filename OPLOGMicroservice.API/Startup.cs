using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OPLOGMicroservice.API.Configuration;
using OPLOGMicroservice.Business;
using OPLOGMicroservice.Business.CQRS.Commands;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Contexts;
using OPLOGMicroservice.Data.Data;
using OPLOGMicroservice.Infra.Swagger;
using OPLOGMicroservice.Logging;
using System.Collections.Generic;
using System.Reflection;

namespace OPLOGMicroservice.API
{
    public class Startup
    {
        // This is the required API_VERSION for swagger endpoint to generate api version
        public const string ApiVersion = "v1";
        public const string ApiTitle = "OPLOGMicroservice Web API";
        public const string OpenApiTitle = "OPLOGMicroservice OpenAPI";
        private string ApiName => _env.IsProduction()
        ? OpenApiTitle
        : ApiTitle;

        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private List<string> _clientOriginHostNames;

        public ConfigurationOptions OPLOGMicroserviceConfiguration { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _logger = logger;
            _env = env;
            OPLOGMicroserviceConfiguration = _configuration.GetSection("AppSettings").Get<ConfigurationOptions>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(OPLOGMicroserviceConfiguration);
            services.AddSingleton(OPLOGMicroserviceConfiguration.Auth0Options);

            services.AddOPLOGSwagger(ApiVersion, ApiName, OPLOGMicroserviceConfiguration.Auth0Options, _env);


            string dbConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = OPLOGMicroserviceConfiguration.EfDataConfiguration.Connection.Server,
                UserID = OPLOGMicroserviceConfiguration.EfDataConfiguration.Connection.UserName,
                Password = OPLOGMicroserviceConfiguration.EfDataConfiguration.Connection.Password,
                InitialCatalog = OPLOGMicroserviceConfiguration.EfDataConfiguration.Connection.DB
            }.ConnectionString;

            services.AddDbContext<OPLOGMicroserviceReadDbContext>(opts =>
            {
                opts.UseSqlServer(dbConnectionString, sqlOptions =>
                {
                    sqlOptions.CommandTimeout(120);
                });
            });

            services.AddDbContext<OPLOGMicroserviceWriteDbContext>(opts =>
            {
                opts.UseSqlServer(dbConnectionString, sqlOptions =>
                {
                    sqlOptions.CommandTimeout(120);
                });
            });

            services.AddScoped<IReadDbContext, OPLOGMicroserviceReadDbContext>();
            services.AddScoped<IWriteDbContext, OPLOGMicroserviceWriteDbContext>();

            services.AddMediatR(typeof(CreateOPLOGMicroservice).GetTypeInfo().Assembly);


            services.AddControllers(options =>
            {
                options.AddPerformanceTracking();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _clientOriginHostNames = OPLOGMicroserviceConfiguration.ClientOriginHostNames;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseOPLOGSwaggerUI(ApiTitle, OPLOGMicroserviceConfiguration.Auth0Options);

            app.UseCors(options =>
            {
                options.WithOrigins(_clientOriginHostNames.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseOPLOGExceptionHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
