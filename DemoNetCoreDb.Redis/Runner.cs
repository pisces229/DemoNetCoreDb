using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace DemoNetCoreDb.Redis
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public Runner(ILogger<Runner> logger,
            IDistributedCache cache,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _cache = cache;
            _connectionMultiplexer = connectionMultiplexer;
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
        private async Task DoAction()
        {
            await DoCRUD();
            //DoSubscriber();
        }
        private async Task DoCRUD()
        {
            _logger.LogInformation("start");
            await _cache.SetStringAsync("key", "value");
            var value = await _cache.GetStringAsync("key");
            _logger.LogInformation($"{value}");
            await _cache.RemoveAsync("key");
            _logger.LogInformation("end");
        }
        private void DoSubscriber()
        {
            _connectionMultiplexer.GetSubscriber().Subscribe("MessageFirst").OnMessage(handler =>
            {
                _logger.LogInformation($"SubscribeMessageFirst:{handler.Message}");
                Thread.Sleep(10000);
            });
            //_connectionMultiplexer.GetSubscriber().Subscribe("MessageFirst").OnMessage(handler =>
            //{
            //    _logger.LogInformation($"SubscribeMessageFirst 2:{handler.Message}");
            //});
            //_connectionMultiplexer.GetSubscriber().Subscribe("MessageSecond").OnMessage(handler =>
            //{
            //    _logger.LogInformation($"SubscribeMessageSecond 0:{handler.Message}");
            //});
            while (true)
            {
                _connectionMultiplexer.GetSubscriber().Publish("MessageFirst", $"PublishMessageFirst[{DateTime.Now}]", CommandFlags.FireAndForget);
                //_connectionMultiplexer.GetSubscriber().Publish("MessageSecond", $"PublishMessageSecond[{DateTime.Now}]", CommandFlags.FireAndForget);
                Thread.Sleep(3000);
            }
        }
    }
}
