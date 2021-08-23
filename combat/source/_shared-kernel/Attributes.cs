using System.Diagnostics;

namespace EventSourcingDemo.Combat
{
    [DebuggerDisplay("A:{Attack} D:{Defense} HP:{HitPoints} MA:{MagicAttack} MD:{MagicDefense} S:{Speed}")]
    public class Attributes
    {
        #region Creation

        public Attributes(
            ushort attack,
            ushort defense,
            ushort hitPoints,
            ushort magicAttack,
            ushort magicDefense,
            ushort speed
        )
        {
            Attack = attack;
            Defense = defense;
            HitPoints = hitPoints;
            MagicAttack = magicAttack;
            MagicDefense = magicDefense;
            Speed = speed;
        }

        #endregion

        #region Public Interface

        public ushort Attack { get; }
        public ushort Defense { get; }
        public ushort HitPoints { get; }
        public ushort MagicAttack { get; }
        public ushort MagicDefense { get; }
        public ushort Speed { get; }

        #endregion

        #region Static Interface

        public static Attributes operator +(Attributes left, Attributes right) =>
            new(
                (ushort) (left.Attack + right.Attack),
                (ushort) (left.Defense + right.Defense),
                (ushort) (left.HitPoints + right.HitPoints),
                (ushort) (left.MagicAttack + right.MagicAttack),
                (ushort) (left.MagicDefense + right.MagicDefense),
                (ushort) (left.Speed + right.Speed)
            );

        #endregion
    }
}
