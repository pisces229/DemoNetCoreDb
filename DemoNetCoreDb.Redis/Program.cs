using DemoNetCoreDb.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

Console.WriteLine(args);

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddConsole();
});
services.AddStackExchangeRedisCache(options =>
{
    //options.ConfigurationOptions
    options.Configuration = "localhost:6379,defaultDatabase=0";
    //options.InstanceName = "";

    //var connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379,defaultDatabase=0");
    //connectionMultiplexer.Configure(new RedisWriterLogger());
    //options.ConnectionMultiplexerFactory = async () =>  await Task.FromResult(connectionMultiplexer);
});
services.AddSingleton<Runner>();
services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect("localhost:6379,defaultDatabase=0"));

var serviceProvider = services.BuildServiceProvider();
try
{
    serviceProvider.GetRequiredService<Runner>().Run();
}
finally
{
    serviceProvider.Dispose();
}