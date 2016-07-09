using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleApplication
{
    public class Program
    {
        static IConfigurationRoot Configuration { get; set; }
        static IServiceProvider Provider {get;set;}
        public static void Main(string[] args)
        {
            Setup();

            Console.WriteLine(Configuration["somekey"]);
            Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
            
            var dummy = Provider.GetService(typeof(IDummyInterface)) as IDummyInterface;

            if(dummy != null)
                System.Console.WriteLine(dummy.GetOptions());
        }

        private static void Setup()
        {
            var builder = new ConfigurationBuilder();
            builder
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddInMemoryCollection()
               .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            Configuration["somekey"] = "somevalue";
            ConfigServices(Configuration);

        }

        private static void ConfigServices(IConfigurationRoot configuration)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
             var section = configuration.GetSection("MyOptions");
            // var config = Configuration["MyOptions:Option1"];
            services.Configure<MyOptions>(section);

            services.AddTransient(typeof(IDummyInterface), typeof(DummyClass));

            Provider = services.BuildServiceProvider();
            //var options = provider.GetService(typeof(MyOptions));

            //var dummyClass = new DummyClass(provider.get));

            //services.Add(new ServiceDescriptor(typeof(DummyClass), new DummyClass(provider.GetService)));
        }
    }

    public interface IDummyInterface
    {
        string GetOptions();
    }
    public class DummyClass : IDummyInterface
    {
        private IOptions<MyOptions> _optionsAccessor;

        public DummyClass(IOptions<MyOptions> optionsAcessor)
        {
            _optionsAccessor = optionsAcessor;
        }

        public string GetOptions()
        {
            MyOptions options = _optionsAccessor.Value;
            
            return $"{options.Option1} {options.Option2}";
        }
        
    }
    public class MyOptions
    {
        public string Option1 { get; set; }
        public int Option2 { get; set; }
    }
}
