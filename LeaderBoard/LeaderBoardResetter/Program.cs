using LeaderBoard.Data;
using LeaderBoardResetter;
using StackExchange.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IConnectionMultiplexer>(service =>
        {
            return ConnectionMultiplexer.Connect("127.0.0.1:6379");
        });

        services.AddTransient<ILeaderboardManager, LeaderboardManager>();
    })
    .Build();



host.Run();
