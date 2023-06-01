using System;

namespace EventSourcingDemo.Combat
{
    public interface IEventStore
    {
        Result<Event[]> Find(StreamId streamId);
        Result Push(Event @event);
    }

    public record StreamId(
        string Category,
        Guid EntityId = default,
        bool IsCommand = false
    );
}
