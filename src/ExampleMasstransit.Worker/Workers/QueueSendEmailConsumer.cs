using ExampleMasstransit.WebApi.Core.Events;
using MassTransit;

namespace ExampleMasstransit.Worker.Workers;

public class QueueSendEmailConsumer : IConsumer<SendEmailEvent>
{
    private readonly ILogger<QueueSendEmailConsumer> _logger;

    public QueueSendEmailConsumer(ILogger<QueueSendEmailConsumer> logger)
        => _logger = logger;
    public async Task Consume(ConsumeContext<SendEmailEvent> context)
    {
        _logger.LogInformation($"Email successfully sent: {context.Message.Email}");

       await Task.CompletedTask;
    }
}
