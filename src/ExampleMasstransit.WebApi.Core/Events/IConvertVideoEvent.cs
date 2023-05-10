

namespace ExampleMasstransit.WebApi.Core.Events;

public interface IConvertVideoEvent
{
    string GroupId { get; }
    int Index { get; }
    int Count { get; }
    string Path { get; }
}
