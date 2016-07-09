using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.Configure<CustomServiceConfig>(configuration.GetSection("CustomServiceConfig"));

            services.AddTransient(typeof(ICustomService), typeof(CustomService));

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
