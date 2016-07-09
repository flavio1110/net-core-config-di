using Microsoft.Extensions.Options;

namespace ConsoleApplication
{
    public class CustomService : ICustomService
    {
        private CustomServiceConfig _clientConfig;

        public CustomService(IOptions<CustomServiceConfig> optionsAcessor)
        {
            _clientConfig = optionsAcessor.Value;
        }

        public void Authorize()
        {
            System.Console.WriteLine("Authorize called");
            System.Console.WriteLine(_clientConfig.ToString());
        }

        public void Capture()
        {
            System.Console.WriteLine("Capture called");
            System.Console.WriteLine(_clientConfig.ToString());
        }
    }
}
