
using MassTransit;

namespace ExampleMasstransit.Worker.Workers;

public class QueueClientConsumerDefinition : ConsumerDefinition<QueueClientInsertedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueClientInsertedConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry=> retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}

