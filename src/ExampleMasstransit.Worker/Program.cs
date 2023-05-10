using ExampleMasstransit.WebApi.Core.Events;
using ExampleMasstransit.WebApi.Core;
using ExampleMasstransit.Worker.Workers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using ExampleMasstransit.WebApi.Core.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog("Worker MassTransit");
    Log.Information("Starting Worker");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog(Log.Logger)
        .ConfigureServices((context, collection) =>
        {
            var appSettings = new AppSettings();
            context.Configuration.Bind(appSettings);
            collection.AddOpenTelemetry(appSettings);
            collection.AddHttpContextAccessor();
            collection.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.AddConsumer<TimerVideoConsumer>(typeof(TimerVideoConsumerDefinition));
                x.AddConsumer<QueueClientInsertedConsumer>(typeof(QueueClientConsumerDefinition));
                x.AddConsumer<QueueClientUpdatedConsumer>(typeof(QueueClientUpdatedConsumerDefinition));
                x.AddConsumer<QueueSendEmailConsumer>(typeof(QueueSendEmailConsumerDefinition));
                x.AddRequestClient<IConvertVideoEvent>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(context.Configuration.GetConnectionString("RabbitMq"));
                    cfg.UseDelayedMessageScheduler();
                    //cfg.ConnectReceiveObserver(new ReceiveObserverExtensions());
                    cfg.ServiceInstance(instance =>
                    {
                        instance.ConfigureJobServiceEndpoints();
                        instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                    });
                });
            });
        }).Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}