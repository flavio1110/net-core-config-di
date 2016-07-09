using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class CustomService : ICustomService
    {
        private CustomServiceConfig _clientConfig;
        private ILogger _logger;

        public CustomService(IOptions<CustomServiceConfig> optionsAcessor, ILogger logger)
        {
            _logger = logger;
            _clientConfig = optionsAcessor.Value;
        }

        public void Authorize()
        {
            _logger.LogInformation("Authorize called");
            System.Console.WriteLine(_clientConfig.ToString());
        }

        public void Capture()
        {
            _logger.LogInformation("Authorize called");
            System.Console.WriteLine(_clientConfig.ToString());
        }

        public void LogEverything()
        {
            _logger.LogTrace("Trace");            
            _logger.LogDebug("Debug");
            _logger.LogInformation("Info");
            _logger.LogWarning("warning");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");
        }

    }
}
