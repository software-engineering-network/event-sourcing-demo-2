using System;
using static EventSourcingDemo.Combat.Command;

namespace EventSourcingDemo.Combat.Items
{
    public partial class ItemManager : IHandler<RegisterItem>
    {
        #region IHandler<RegisterItem> Implementation

        public Result Handle(RegisterItem command)
        {
            var itemRegistered = new ItemRegistered(command);

            return _store.Push(itemRegistered);
        }

        #endregion
    }

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
}
