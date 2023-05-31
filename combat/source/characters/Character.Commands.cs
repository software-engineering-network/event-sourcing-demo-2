using System;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        public record CreateCharacter(string Name) : ICommand;

        public record SetAttributes : ICommand
        {
            #region Creation

            public SetAttributes(
                Guid characterId,
                ushort attack,
                ushort defense,
                ushort hitPoints,
                ushort magicAttack,
                ushort magicDefense,
                ushort speed
            )
            {
                Id = Guid.NewGuid();
                CharacterId = characterId;
                Attributes = new Attributes(attack, defense, hitPoints, magicAttack, magicDefense, speed);
            }

            #endregion

            #region Public Interface

            public Attributes Attributes { get; }
            public Guid CharacterId { get; }
            public Guid Id { get; }

            #endregion
        }
    }
}
