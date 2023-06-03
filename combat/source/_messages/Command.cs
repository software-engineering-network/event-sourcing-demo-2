namespace EventSourcingDemo.Combat
{
    public record Command : Message
    {
        #region Public Interface

        public StreamId GetEntityStreamId() => new(Category, EntityId);

        #endregion

        public interface IHandler<in T>
        {
            public Result Handle(T command);
        }
    }
}
