using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ConsoleApplication
{
    public static class Application
    {
        public static IConfigurationRoot Configuration { get; private set; }
        static IServiceProvider ServiceProvider { get; set; }

        public static void Startup()
        {
            var builder = new ConfigurationBuilder();
                        builder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddInMemoryCollection()
                        .AddJsonFile("appsettings.json");
            
            Configuration = builder.Build();
            Configuration["somekey"] = "somevalue";
            ServiceProvider = ConfigServices(Configuration);
        }

        private static IServiceProvider ConfigServices(IConfigurationRoot configuration)
        {
            Func<string, LogLevel, bool> func = (category, loglevel) => loglevel >= LogLevel.Trace;

            ILogger logger = new ConsoleLogger("teste", func, false);

            IServiceCollection services = new ServiceCollection();
            
            services.AddOptions()
                .Configure<CustomServiceConfig>(configuration.GetSection("CustomServiceConfig"))
                .AddTransient(typeof(ICustomService), typeof(CustomService))
                .AddTransient(typeof(ILogger), typeof(CustomService))
                .Add(new ServiceDescriptor(typeof(ILogger), logger));

            return services.BuildServiceProvider();           
        }

        public static T GetService<T>() where T : class
        {
            if(ServiceProvider == null)
                throw new NullReferenceException("Provider not defined. Call Application.Startup");
            
            return ServiceProvider.GetService<T>() as T;
        }
    }
}
