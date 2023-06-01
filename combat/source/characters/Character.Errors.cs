namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Static Interface

        public static Error CannotDuplicateCharacter(string message = "") =>
            new($"{nameof(Character)}.CannotDuplicateCharacter", message);

        public static Error StreamError(string message = "") => new($"{nameof(Character)}.StreamError", message);

        #endregion
    }
}
