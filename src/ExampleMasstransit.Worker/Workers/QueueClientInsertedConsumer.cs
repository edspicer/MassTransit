using System.Diagnostics;
using MassTransit;
using MassTransit.Metadata;
using ExampleMasstransit.WebApi.Core.Events;

namespace ExampleMasstransit.Worker.Workers;

public class QueueClientInsertedConsumer : IConsumer<ClientInsertedEvent>
{
    private readonly ILogger<QueueClientInsertedConsumer> _logger;

    public QueueClientInsertedConsumer(ILogger<QueueClientInsertedConsumer> logger)
        => _logger = logger;

    public async Task Consume(ConsumeContext<ClientInsertedEvent> context)
    {
        var timer = Stopwatch.StartNew();

        try
        {
            var id = context.Message.ClientId;
            var name = context.Message.Name;
            var email = context.Message.Email;

            await context.Publish(new SendEmailEvent { Email= email});

            _logger.LogInformation($"Receive client: {id} - {name}");

            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<ClientInsertedEvent>.ShortName);

        }
        catch (Exception ex)
        {

            await context.NotifyFaulted(timer.Elapsed, TypeMetadataCache<ClientInsertedEvent>.ShortName, ex);
        }
    }
}
