namespace EventSourcingDemo.Combat
{
    public interface IEventStore
    {
        Result<Event[]> Find(StreamId streamId);
        Result<CategoryStream> Find(CategoryStreamId id);
        Result<EntityStream> Find(EntityStreamId id);
        Result Push(Event @event);
    }
}
