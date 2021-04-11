using Hoff.Api.Logic.Currency.Abstractions;
using Hoff.Api.Logic.Currency.Business;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hoff.Api.Infrastructure
{
    public static class RegisterLogic
    {
        public static void RegisterApiLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IGeometry, Circle>();
            services.AddTransient<ICurrencyExchangeRate, CurrencyExchangeRate>();
            services.AddTransient<ICartesianCoordinates, CartesianCoordinateSystem>();
        }
    }
}
