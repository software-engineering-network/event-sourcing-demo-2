namespace EventSourcingDemo.Combat
{
    public record Command : Message
    {
        #region Implementation

        public EntityStreamId CreateEntityStreamId() => new(Category, EntityId);

        #endregion

        public interface IHandler<in T>
        {
            public Result Handle(T command);
        }
    }
}
