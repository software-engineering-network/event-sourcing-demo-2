namespace EventSourcingDemo.Combat
{
    public abstract record Event : Message
    {
        #region Public Interface

        public Event Apply(Metadata metadata, StreamId streamId, Version version)
        {
            var (category, entityId, _) = streamId;

            return this with
            {
                Category = category,
                EntityId = entityId!.Value,
                Metadata = metadata,
                Version = version
            };
        }

        public bool IsInCategory(StreamId streamId) => Category == streamId.Category;

        public bool IsInEntity(StreamId streamId) => Category == streamId.Category && EntityId == streamId.EntityId;

        #endregion
    }
}
