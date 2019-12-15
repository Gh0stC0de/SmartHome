using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartHome.Core.Services.Abstractions;
using SmartHome.Service.Helpers;
using SmartHome.Infrastructure;
using SmartHome.Infrastructure.DbContexts.Abstractions;
using SmartHome.Infrastructure.DbContexts.Implementations;
using SmartHome.Infrastructure.Services.Implementations;

namespace SmartHome.Service
{
    /// <summary>
    ///     Manages application startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class with a configuration.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Configures services.
        /// </summary>
        /// <param name="services">Service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.TokenSecret);
            ConfigureAuthentication(services, key);

            ConfigureSwagger(services);

            ConfigureSmartHomeDbContext(services, appSettingsSection);

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, Infrastructure.Services.Implementations.DeviceService>();
        }

        /// <summary>
        ///     Configures the authentication.
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="secret">Issuer signing key secret</param>
        private static void ConfigureAuthentication(IServiceCollection services, byte[] secret)
        {
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = true;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = Guid.Parse(context.Principal.Identity.Name);
                            var user = userService.GetUser(userId);
                            if (user == null) context.Fail("401 Unauthorized");
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddScoped<IJwtService, JwtService>();
        }

        /// <summary>
        ///     Configures swagger.
        /// </summary>
        /// <param name="services">Services</param>
        private static void ConfigureSwagger(IServiceCollection services)
        {
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

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                g.IncludeXmlComments(xmlPath);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                g.AddSecurityDefinition("Bearer", securityScheme);
                g.AddSecurityRequirement(new OpenApiSecurityRequirement {{securityScheme, new[] {"Bearer"}}});
            });
        }

        /// <summary>
        ///     Configures smart home db context.
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="appSettingsSection">Application settings configuration section</param>
        private void ConfigureSmartHomeDbContext(IServiceCollection services, IConfiguration appSettingsSection)
        {
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SmartHome"))
            {
                Password = appSettingsSection["DbPassword"]
            };
            var connection = builder.ConnectionString;
            services.AddDbContext<SmartHomeDbContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("SmartHome.Infrastructure")));
            services.AddScoped<ISmartHomeDbContext>(provider => provider.GetService<SmartHomeDbContext>());
        }

        /// <summary>
        ///     Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Web host environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetService<SmartHomeDbContext>().Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Home API V1");
                c.RoutePrefix = string.Empty;
            });

            MappingHelper.ConfigureMappings();
        }
    }
}