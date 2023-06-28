using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat.Items
{
    public partial class Item : Projection
    {
        private static readonly Dictionary<Type, Func<Item, Event, Item>> Handlers = new();

        #region Creation

        static Item()
        {
            Register<ItemRegistered>(Apply);
        }

        public static Result<Item> Rehydrate(params Event[] stream)
        {
            using var events = stream.ToList().GetEnumerator();

            Item item = null;

            while (events.MoveNext())
                item = Handlers[events.Current.GetType()](item, events.Current);

            return item;
        }

        #endregion

        #region Implementation

        public Guid Id { get; }
        public string Name { get; }

        #endregion

        #region Static Interface

        private static void Register<T>(Func<Item, T, Item> handler) where T : Event
        {
            Handlers.Add(
                typeof(T),
                (item, @event) => handler(item, (T) @event)
            );
        }

        #endregion
    }
}
