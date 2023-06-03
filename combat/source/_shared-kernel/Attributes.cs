using System.Collections.Generic;
using System.Diagnostics;

namespace EventSourcingDemo.Combat
{
    [DebuggerDisplay("A:{Attack} D:{Defense} HP:{HitPoints} MA:{MagicAttack} MD:{MagicDefense} S:{Speed}")]
    public class Attributes : ValueObject
    {
        public static readonly Attributes Default = new(0, 0, 0, 0, 0, 0);
        public static readonly Attributes Mario = new(20, 0, 20, 10, 2, 20);

        #region Creation

        public Attributes(
            short attack,
            short defense,
            short hitPoints,
            short magicAttack,
            short magicDefense,
            short speed
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

        public short Attack { get; }
        public short Defense { get; }
        public short HitPoints { get; }
        public short MagicAttack { get; }
        public short MagicDefense { get; }
        public short Speed { get; }

        #endregion

        #region Equality

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Attack;
            yield return Attack;
            yield return Defense;
            yield return HitPoints;
            yield return MagicAttack;
            yield return MagicDefense;
            yield return Speed;
        }

        #endregion

        #region Static Interface

        public static Attributes operator +(Attributes left, Attributes right) =>
            new(
                (short)(left.Attack + right.Attack),
                (short)(left.Defense + right.Defense),
                (short)(left.HitPoints + right.HitPoints),
                (short)(left.MagicAttack + right.MagicAttack),
                (short)(left.MagicDefense + right.MagicDefense),
                (short)(left.Speed + right.Speed)
            );

        #endregion
    }
}
