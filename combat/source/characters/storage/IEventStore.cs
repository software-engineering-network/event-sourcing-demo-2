namespace EventSourcingDemo.Combat
{
    public interface IEventStore
    {
        Result<IEvent[]> GetStream(string streamId);
        Result Push(string streamId, IEvent @event);
    }
}
