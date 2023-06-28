using EventSourcingDemo.Combat;
using EventSourcingDemo.Combat.Items;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class WhenRegisteringAnItem
    {
        #region Setup

        private readonly RegisterItem _command;
        private readonly ItemRegistered _event;

        public WhenRegisteringAnItem()
        {
            var store = new MockEventStore();
            var service = new ItemManager(store);

            service.Handle(_command = new RegisterItem("Mushroom"));

            var stream = store.Find(new StreamId(ItemManager.Category, _command.EntityId)).Value;

            _event = (ItemRegistered) stream[0];
        }

        #endregion

        #region Requirements

        [Fact]
        public void ThenCategoryIsItemRegistration() => _event.Category.Should().Be(ItemManager.Category);

        #endregion
    }
}
