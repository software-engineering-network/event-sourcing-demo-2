namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Static Interface

        public static Error NoOp(string message = "") => new($"{nameof(Character)}.NoOp", message);
        public static Error StreamError(string message = "") => new($"{nameof(Character)}.StreamError", message);

        #endregion
    }
}
