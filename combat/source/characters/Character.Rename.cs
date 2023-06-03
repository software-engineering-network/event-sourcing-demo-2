namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Creation

        private Character(Character source, string name) : this(source)
        {
            Name = name;
        }

        #endregion

        #region Implementation

        public Result<CharacterRenamed> Rename(string name) => new CharacterRenamed(name);

        #endregion

        #region Static Interface

        private static Character Apply(Character target, CharacterRenamed @event) => new(target, @event.Name);

        #endregion
    }

    public record RenameCharacter(string Name) : Command;

    public record CharacterRenamed(string Name) : Event;
}
