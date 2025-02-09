﻿using System;

namespace EventSourcingDemo.Combat
{
    public interface IEventStore
    {
        Result<Event[]> Find(StreamId streamId);
        Result Push(Event @event);
    }

    public record StreamId
    {
        #region Creation

        public StreamId(
            string category,
            bool isCommand = false
        )
        {
            Category = category;
            IsCommand = isCommand;
        }

        public StreamId(
            string category,
            Guid entityId,
            bool isCommand = false
        )
        {
            Category = category;
            EntityId = entityId;
            IsCommand = isCommand;
        }

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
