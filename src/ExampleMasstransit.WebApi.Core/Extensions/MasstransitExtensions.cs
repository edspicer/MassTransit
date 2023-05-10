using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleMasstransit.WebApi.Core.Extensions;

public static class MasstransitExtensions
{
    public static void AddMasstransitExtension(this IServiceCollection services, IConfiguration configuration)
        => services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
                cfg.UseDelayedMessageScheduler();
                cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                cfg.UseMessageRetry(retry=> retry.Interval(3, TimeSpan.FromSeconds(5)));

            });

        });
}
