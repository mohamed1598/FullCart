using FullCart.Core.Interfaces;
using FullCart.Infrastructure.Data;
using FullCart.Infrastructure.Repositories;
using FullCart.Infrastructure.Services;

namespace FullCart.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
