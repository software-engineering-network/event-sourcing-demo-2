using EventSourcingDemo.Combat;
using EventSourcingDemo.Combat.Items;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Items.Item;
using static EventSourcingDemo.CombatSpec.Items.ObjectProvider;

namespace EventSourcingDemo.CombatSpec.Items
{
    public class WhenRegisteringAnItem
    {
        #region Setup

        private readonly RegisterItem _command;
        private readonly ItemRegistered _event;

        public WhenRegisteringAnItem()
        {
            var service = CreateService(out var store);
            service.Handle(_command = new RegisterItem("Mushroom"));

            var stream = store.Find(new StreamId(ItemManager.Category, _command.EntityId)).Value;

            _event = (ItemRegistered) stream[0];
        }

        #endregion

        #region Requirements

        [Fact]
        public void ThenCategoryIsItemRegistration() => _event.Category.Should().Be(ItemManager.Category);

        [Fact]
        public void ThenEntityIdMatchesCommandEntityId() => _event.EntityId.Should().Be(_command.EntityId);

        #endregion

        public class GivenAConflictingItem
        {
            #region Requirements

            [Fact]
            public void ThenReturnItemAlreadyExistsError()
            {
                const string name = "Mushroom";

                var service = CreateService();
                service.Handle(new RegisterItem(name));

                var error = service.Handle(new RegisterItem(name)).Error;

                error.Should().Be(ItemAlreadyExists());
            }

            #endregion
        }
    }
}
