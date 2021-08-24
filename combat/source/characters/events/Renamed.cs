namespace EventSourcingDemo.Combat
{
    public class Renamed : IEvent
    {
        #region Creation

        public Renamed(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public string Name { get; }

        #endregion
    }
}
