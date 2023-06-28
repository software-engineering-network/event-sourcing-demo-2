using System;
using System.Linq;
using static EventSourcingDemo.Combat.Command;
using static EventSourcingDemo.Combat.Items.Item;
using static EventSourcingDemo.Combat.Result;

namespace EventSourcingDemo.Combat.Items
{
    public record RegisterItem : Command
    {
        #region Creation

        public RegisterItem(string name)
        {
            Category = ItemManager.Category;
            EntityId = Guid.NewGuid();
            Name = name;
        }

        #endregion

        #region Implementation

        public string Name { get; init; }

        #endregion
    }

    public record ItemRegistered : Event
    {
        #region Creation

        public ItemRegistered(RegisterItem command)
        {
            Category = command.Category;
            EntityId = command.EntityId;
            Name = command.Name;
        }

        #endregion

        #region Implementation

        public string Name { get; init; }

        #endregion
    }

    public partial class ItemManager : IHandler<RegisterItem>
    {
        #region IHandler<RegisterItem> Implementation

        public Result Handle(RegisterItem command) =>
            _store.Find(new(Category))
                .Bind(categoryStream => CheckForDuplicates(command.Name, categoryStream))
                .Bind(() => _store.Push(new ItemRegistered(command)));

        #endregion

        #region Static Interface

        private static Result CheckForDuplicates(string name, Event[] categoryStream)
        {
            var entityStreamIds = categoryStream.Select(x => new StreamId(Category, x.EntityId)).Distinct();

            var items = entityStreamIds.Select(
                id => Rehydrate(categoryStream.Where(x => x.IsInEntity(id)).ToArray()).Value
            );

            return items.Any(x => x.Name == name)
                ? ItemAlreadyExists()
                : Success();
        }

        #endregion
    }

    public partial class Item
    {
        #region Creation

        public Item(ItemRegistered @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }

        #endregion

        #region Static Interface

        public static Item Apply(Item target, ItemRegistered @event) => new(@event);
        public static Error ItemAlreadyExists() => new($"{nameof(Item)}.{nameof(ItemAlreadyExists)}");

        #endregion
    }
}
