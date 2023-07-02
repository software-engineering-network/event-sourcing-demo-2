namespace EventSourcingDemo.Combat
{
    public interface IEventStore
    {
        Result<CategoryStream> Find(CategoryStreamId id);
        Result<EntityStream> Find(EntityStreamId id);
        Result Push(Event @event);
    }
}
