

using MassTransit;

namespace ExampleMasstransit.Worker.Workers;

public class QueueClientUpdatedConsumerDefinition : ConsumerDefinition<QueueClientUpdatedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueClientUpdatedConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
