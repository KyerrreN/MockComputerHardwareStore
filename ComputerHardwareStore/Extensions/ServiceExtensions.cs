namespace ComputerHardwareStore.Extensions
{
    public static class ServiceExtensions
    {
        // Configuring CORS
        // Basic for now, will be more restrictive later
        // as i learn whatever the fuck CORS is and how it works
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
        }

        // Configuring IIS
        // Fine for now, might modify options in the future
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
    }
}
