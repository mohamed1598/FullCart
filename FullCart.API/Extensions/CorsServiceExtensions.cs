using Microsoft.OpenApi.Models;

namespace FullCart.API.Extensions
{
    public static class CorsServiceExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }
}
