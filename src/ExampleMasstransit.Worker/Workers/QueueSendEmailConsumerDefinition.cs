

using MassTransit;

namespace ExampleMasstransit.Worker.Workers;

public class QueueSendEmailConsumerDefinition : ConsumerDefinition<QueueSendEmailConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueSendEmailConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
