namespace EventSourcingDemo.Combat
{
    public record Command : Message
    {
        #region Implementation

        public StreamId GetEntityStreamId() => new(Category, EntityId);

        #endregion

        public interface IHandler<in T>
        {
            public Result Handle(T command);
        }
    }
}
