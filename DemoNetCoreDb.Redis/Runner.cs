using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace DemoNetCoreDb.Redis
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        private readonly IDistributedCache _cache;
        public Runner(ILogger<Runner> logger,
            IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
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
            await _cache.SetStringAsync("key", "value");
            var value = await _cache.GetStringAsync("key");
            await _cache.RemoveAsync("key");
            _logger.LogInformation("DoAction End");
        }
    }
}
