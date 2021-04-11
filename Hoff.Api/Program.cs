using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Hoff.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((host, builder) => 
                {
                    //builder.
                    //builder.AddFile("");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
