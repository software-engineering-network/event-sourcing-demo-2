using System;

namespace EventSourcingDemo.Combat
{
    public record EntityStreamId : StreamId
    {
        #region Creation

        public EntityStreamId(string category, Guid id)
        {
            Category = category;
            EntityId = id;
        }

        #endregion
    }

    public record CategoryStreamId : StreamId
    {
        #region Creation

        public CategoryStreamId(string category)
        {
            Category = category;
        }

        #endregion
    }

    public abstract record StreamId
    {
        #region Creation

        public void Deconstruct(
            out string Category,
            out Guid? EntityId,
            out bool IsCommand
        )
        {
            Category = this.Category;
            EntityId = this.EntityId;
            IsCommand = this.IsCommand;
        }

        #endregion

        #region Implementation

        public Guid? EntityId { get; init; }
        public string Category { get; init; }
        public bool IsCommand { get; init; }

        #endregion
    }
}
