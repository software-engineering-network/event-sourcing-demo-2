using System;

namespace EventSourcingDemo.Combat
{
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
            Attack = attack;
            CharacterId = characterId;
            Defense = defense;
            HitPoints = hitPoints;
            Id = Guid.NewGuid();
            MagicAttack = magicAttack;
            MagicDefense = magicDefense;
            Speed = speed;
        }

        #endregion

        #region Public Interface

        public ushort Attack { get; }
        public Guid CharacterId { get; }
        public ushort Defense { get; }
        public ushort HitPoints { get; }
        public Guid Id { get; }
        public ushort MagicAttack { get; }
        public ushort MagicDefense { get; }
        public ushort Speed { get; }

        public void Deconstruct(
            out Guid characterId,
            out ushort attack,
            out ushort defense,
            out ushort hitPoints,
            out ushort magicAttack,
            out ushort magicDefense,
            out ushort speed
        )
        {
            characterId = CharacterId;
            attack = Attack;
            defense = Defense;
            hitPoints = HitPoints;
            magicAttack = MagicAttack;
            magicDefense = MagicDefense;
            speed = Speed;
        }

        #endregion
    }
}
