using Hoff.Api.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hoff.Api.Infrastructure
{
    public static class RegisterOptions
    {
        public static void RegisterApiOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeometryOptions>(opt => configuration.GetSection(nameof(GeometryOptions)).Bind(opt));
            services.Configure<DailyInfoWebServiceOptions>(opt => configuration.GetSection(nameof(DailyInfoWebServiceOptions)).Bind(opt));
            services.Configure<ApiOptions>(opt => configuration.GetSection(nameof(ApiOptions)).Bind(opt));
        }
    }
}
