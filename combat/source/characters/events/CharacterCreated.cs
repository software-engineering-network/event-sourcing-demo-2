namespace EventSourcingDemo.Combat
{
    public class CharacterCreated : Event
    {
        #region Creation

        public CharacterCreated(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public string Name { get; }

        #endregion
    }
}
