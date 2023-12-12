using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Application.Services;
using NeoSOFT.Common.Helpers;
using NeoSOFT.Infrastructure.Context;
using NeoSOFT.Infrastructure.Contracts;
using NeoSOFT.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
//using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Asp.Versioning;
using NeoSOFT.Infrastrcture.Contracts;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using ServiceLifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime;

namespace NeoSOFTWebAPI.Extentions
{
    public static class ServiceExtensions
    {
        public static readonly ILoggerFactory _efLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                   builder => builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            });
        }
        public static void ConfigureDBConnection(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<EmployeeManagementContext>(options => options
                                                     .UseLazyLoadingProxies()
                                                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                                                    .UseLoggerFactory(_efLoggerFactory)
                                                    , ServiceLifetime.Transient);
        }
        public static void ConfigureHttpContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
        }
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {

            var VersionedApiExplorer = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                // Use whatever reader you want
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                new HeaderApiVersionReader("x-api-version"),
                                                new MediaTypeApiVersionReader("x-api-version"));
            });
            VersionedApiExplorer.AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
               
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{

                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //{
                //     new OpenApiSecurityScheme
                //     {
                //       Reference = new OpenApiReference
                //       {
                //         Type = ReferenceType.SecurityScheme,
                //         Id = "Bearer"
                //       }
                //      },
                //      new string[] { }
                //    } });
            });
        }
    }
}
