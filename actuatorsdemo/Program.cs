using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pivotal.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Logging;

namespace ActuatorsDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                    //TODO: check how can both UseCloudFoundryHosting() and AddConfigServer()
                    //Error CS0121  The call is ambiguous between the following methods or properties: 
                    //'Pivotal.Extensions.Configuration.ConfigServer.ConfigServerHostBuilderExtensions.UseCloudFoundryHosting(Microsoft.AspNetCore.Hosting.IWebHostBuilder, int?)' 
                    //and 'Steeltoe.Extensions.Configuration.CloudFoundry.CloudFoundryHostBuilderExtensions.UseCloudFoundryHosting(Microsoft.AspNetCore.Hosting.IWebHostBuilder, int?)'	

                    //.UseCloudFoundryHosting()
                    .AddCloudFoundry()
                    .AddConfigServer()

                    .ConfigureLogging((builderContext, loggingBuilder) =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                        loggingBuilder.AddDynamicConsole();
                    })
            
                    .UseStartup<Startup>();
    }
}
