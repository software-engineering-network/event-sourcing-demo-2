using EventSourcingDemo.Combat;
using EventSourcingDemo.Combat.Items;

namespace EventSourcingDemo.CombatSpec.Items
{
    internal class ObjectProvider
    {
        #region Static Interface

        public static ItemManager CreateService() => CreateService(out var store);
        public static ItemManager CreateService(out IEventStore store) => new(store = new MockEventStore());

        #endregion
    }
}
