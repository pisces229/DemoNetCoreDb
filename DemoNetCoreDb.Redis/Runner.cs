using Microsoft.Extensions.Logging;

namespace DemoNetCoreDb.Redis
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }
        public void Run()
        {
            try
            {
                Task.Run(async () => await DoAction()).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public async Task DoAction()
        {
            _logger.LogInformation("DoAction Begin");
            
            _logger.LogInformation("DoAction End");
        }
    }
}
