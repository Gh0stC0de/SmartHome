using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartHome.Infrastructure.DbContexts;

namespace SmartHome.DeviceService
{
    /// <summary>
    ///     Manages application startup
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Configures services
        /// </summary>
        /// <param name="services">Service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SmartHome"))
            {
                Password = Configuration["DbPassword"]
            };
            var connection = builder.ConnectionString;

            services.AddDbContext<SmartHomeContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("SmartHome.Infrastructure")));

            services.AddSwaggerGen(g =>
            {
                g.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Smart home API",
                    Version = "v1",
                    Description = "Simple smart home ASP .NET Core API",
                    TermsOfService = null,
                    Contact = new OpenApiContact
                    {
                        Name = "Aaron Müller"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                g.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        ///     Configures the HTTP request pipeline
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Web host environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart home API V1");
                u.RoutePrefix = string.Empty;
            });
        }
    }
}