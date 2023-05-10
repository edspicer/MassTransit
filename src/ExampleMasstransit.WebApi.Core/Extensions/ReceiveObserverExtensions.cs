using MassTransit;

namespace ExampleMasstransit.WebApi.Core.Extensions;

public class ReceiveObserverExtensions : IReceiveObserver
{
    public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostReceive(ReceiveContext context)
    {
        throw new NotImplementedException();
    }

    public Task PreReceive(ReceiveContext context)
    {
        return Task.CompletedTask;
    }

    public Task ReceiveFault(ReceiveContext context, Exception exception)
    {
        return Task.CompletedTask;
    }
}
