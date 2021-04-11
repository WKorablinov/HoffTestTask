using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Hoff.Api.Infrastructure;
using Hoff.Api.Middleware;

using Microsoft.Extensions.Logging;

namespace Hoff.Api
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hoff.Api", Version = "v1" });
            });
            services.AddMediatR(typeof(Startup));
            services.RegisterApiOptions(_configuration);
            services.RegisterApiLogic(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hoff.Api v1"));
            app.UseRouting();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            loggerFactory.AddFile("Logs/Hoff.Api-{Date}.txt");
        }
    }
}
