using ExampleMasstransit.WebApi.Core.Events;
using MassTransit;


namespace ExampleMasstransit.Worker.Workers;

public class TimerVideoConsumer : IJobConsumer<IConvertVideoEvent>
{
    public async Task Run(JobContext<IConvertVideoEvent> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
    }
}
