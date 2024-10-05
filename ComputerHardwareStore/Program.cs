using NLog;
using ComputerHardwareStore.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Contracts;

namespace ComputerHardwareStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(ComputerHardwareStore.Presentation.AssemblyReference).Assembly);
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

            app.UseHttpsRedirection();

            // Code from the book. Added middleware for CORS/IIS
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
