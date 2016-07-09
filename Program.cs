using System;
using Microsoft.Extensions.Configuration;

namespace ConsoleApplication
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            Application.Startup();            
            
            Console.WriteLine(Application.Configuration["somekey"]);

            Console.WriteLine(Application.Configuration.GetConnectionString("DefaultConnection"));
            
            ICustomService customService = Application.GetService<ICustomService>();

            customService.Authorize();
            customService.Capture();
            customService.LogEverything();
        }       
    }
}
