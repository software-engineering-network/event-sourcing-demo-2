using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat.Items
{
    public interface IProjector<T>
    {
        public HashSet<T> Project(CategoryStream stream);
        public T Project(EntityStream stream);
    }

    public partial class ItemManager : Microservice, IProjector<Item>
    {
        public const string Category = "ItemManagement";
        private readonly IEventStore _store;

        #region Creation

        public ItemManager(IEventStore store)
        {
            _store = store;
        }

        #endregion

        #region IProjector<Item> Implementation

        public HashSet<Item> Project(CategoryStream stream)
        {
            var items = new HashSet<Item>();

            using var events = stream.GetEnumerator();

            while (events.MoveNext())
            {
                var @event = events.Current;

                if (@event is ItemRegistered itemRegistered)
                {
                    items.Add(Item.Apply(null, itemRegistered));
                    continue;
                }

                var item = items.First(x => x.Id == @event.EntityId);

                items.Remove(item);
                items.Add(item.Apply(item, @event));
            }

            return items;
        }

        public Item Project(EntityStream stream) => throw new NotImplementedException();

        #endregion

        #region Static Interface

        public static EntityStreamId CreateEntityStreamId(Guid id) => new(Category, id);

        #endregion
    }
}
