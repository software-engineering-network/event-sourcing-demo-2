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
        public static readonly CategoryStreamId StreamId = new(Category);

        #region Implementation

        private Result CheckForDuplicates(string name, CategoryStream stream) =>
            Project(stream).Any(x => x.Name == name)
                ? ItemAlreadyExists()
                : Success();

        #endregion

        #region IHandler<RegisterItem> Implementation

        public Result Handle(RegisterItem command) =>
            _store.Find(StreamId)
                .Bind(stream => CheckForDuplicates(command.Name, stream))
                .Bind(() => _store.Push(new ItemRegistered(command)));

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
