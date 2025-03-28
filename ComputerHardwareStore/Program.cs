using AspNetCoreRateLimit;
using ComputerHardwareStore.Extensions;
using ComputerHardwareStore.Presentation.ActionFilters;
using ComputerHardwareStore.Utility;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Service;
using Shared.DataTransferObjects;

namespace ComputerHardwareStore
{
    public class Program
    {
        // Local function to workaround applying NewtonsoftJson
        // configuration to ALL JSON formatters
        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            return new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
                        .Services.BuildServiceProvider()
                        .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
                        .OfType<NewtonsoftJsonPatchInputFormatter>().First();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();

            // Action Filter
            builder.Services.AddScoped<BindingValidationFilterAttribute>();

            // Data Shaper
            builder.Services.AddScoped<IDataShaper<GraphicsCardBenchmarkDto>, DataShaper<GraphicsCardBenchmarkDto>>();

            // Media Types
            builder.Services.AddScoped<ValidateMediaTypeAttribute>();

            // Links for HATEOAS
            builder.Services.AddScoped<IGraphicsCardBenchmarkLinks, GraphicsCardBenchmarkLinks>();

            // Cache
            builder.Services.ConfigureResponseCaching();
            builder.Services.ConfigureHttpCacheHeaders();

            // Rate Limiting
            builder.Services.AddMemoryCache();
            builder.Services.ConfigureRateLimitingOptions();
            builder.Services.AddHttpContextAccessor();

            // Identity
            builder.Services.AddAuthentication();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.AddJwtConfiguration(builder.Configuration);

            // Configure to accept headers from 
            // HTTP request and adding XML formatter
            builder.Services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                config.CacheProfiles.Add("120secondsDuration", new CacheProfile { Duration = 120 });
            }).AddXmlDataContractSerializerFormatters()
              .AddCustomCSVFormatter()
              .AddApplicationPart(typeof(ComputerHardwareStore.Presentation.AssemblyReference).Assembly);

            builder.Services.AddCustomMediaTypes();

            builder.Services.ConfigureVersioning();

            // Suprresing filters of ApiController attribute
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Swagger Service
            builder.Services.ConfigureSwagger();

            // FluentValidation Service
            builder.Services.ConfigureFluentValidation();

            // Automapper service
            builder.Services.AddAutoMapper(typeof(Program));

            // Logging (obsolete for some reason, but in the book
            // the configuration is loaded like this)
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Added services with extensions methods
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureSqlContext(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Add logger to use it in our exception handling middleware
            var logger = app.Services.GetRequiredService<ILoggerManager>();
            app.ConfigureExceptionHandler(logger);

            // If we're not in Staging or Production
            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "ComputerHardwareStore API v1");
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "ComputerHardwareStore API v2");
            });

            app.UseHttpsRedirection();

            // Code from the book. Added middleware for CORS/IIS
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseIpRateLimiting();

            app.UseCors("CorsPolicy");

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
