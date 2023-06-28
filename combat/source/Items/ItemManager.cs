namespace EventSourcingDemo.Combat.Items
{
    public partial class ItemManager
    {
        public const string Category = "ItemManagement";
        private readonly IEventStore _store;

        #region Creation

        public ItemManager(IEventStore store)
        {
            _store = store;
        }

        #endregion
    }
}
