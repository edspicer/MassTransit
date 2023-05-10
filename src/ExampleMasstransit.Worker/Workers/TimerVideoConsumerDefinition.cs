
using ExampleMasstransit.WebApi.Core.Events;
using MassTransit;

namespace ExampleMasstransit.Worker.Workers;

public class TimerVideoConsumerDefinition : ConsumerDefinition<TimerVideoConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TimerVideoConsumer> consumerConfigurator)
    {
        consumerConfigurator.Options<JobOptions<IConvertVideoEvent>>(options =>
            options.SetRetry(r => r.Interval(3, TimeSpan.FromSeconds(30))).SetJobTimeout(TimeSpan.FromMinutes(1)).SetConcurrentJobLimit(10));
    }
}
