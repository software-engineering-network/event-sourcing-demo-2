using System;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        public record CreateCharacter(string Name) : ICommand;

        public record SetAttributes(Guid Id, Guid CharacterId, Attributes Attributes) : ICommand;
    }
}
