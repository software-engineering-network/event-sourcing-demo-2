using System;

namespace EventSourcingDemo.Combat
{
    public interface IEventStore
    {
        IEvent[] GetStream(Guid streamId);
        Result Push(Guid streamId, IEvent @event);
    }
}
