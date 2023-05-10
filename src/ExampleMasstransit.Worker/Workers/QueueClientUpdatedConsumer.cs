using ExampleMasstransit.WebApi.Core.Events;
using MassTransit;
using MassTransit.Metadata;

namespace ExampleMasstransit.Worker.Workers;

public class QueueClientUpdatedConsumer : IConsumer<ClientUpdatedEvent>
{
    private readonly ILogger<QueueClientUpdatedConsumer> _logger;

    public QueueClientUpdatedConsumer(ILogger<QueueClientUpdatedConsumer> logger)
        => _logger = logger;
    public async Task Consume(ConsumeContext<ClientUpdatedEvent> context)
    {
        if (context.Message.Name == "test")
        {
            throw new ArgumentException("Invalid");
        }

        var id = context.Message.ClientId;
        var name = context.Message.Name;

        

        _logger.LogInformation($"Receive client: {id} - {name}");

        await Task.CompletedTask;
    }
}
