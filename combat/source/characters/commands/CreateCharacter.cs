namespace EventSourcingDemo.Combat
{
    public record CreateCharacter : ICommand
    {
        #region Creation

        public CreateCharacter(string name)
        {
            Name = name;
        }

        #endregion

        #region Public Interface

        public string Name { get; }

        #endregion
    }
}
