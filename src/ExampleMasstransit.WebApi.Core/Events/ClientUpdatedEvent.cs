
namespace ExampleMasstransit.WebApi.Core.Events;

public class ClientUpdatedEvent
{
    public string? ClientId { get; set; }
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
