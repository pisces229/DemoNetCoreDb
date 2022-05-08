﻿using DemoNetCoreDb.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine(args);

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Information);
    builder.AddConsole();
});
services.AddStackExchangeRedisCache(options =>
{
    //options.ConfigurationOptions
    options.Configuration = "localhost:6379,defaultDatabase=0";
    //options.InstanceName = "";
});
services.AddSingleton<Runner>();

var serviceProvider = services.BuildServiceProvider();
try
{
    serviceProvider.GetRequiredService<Runner>().Run();
}
finally
{
    serviceProvider.Dispose();
}
