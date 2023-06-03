using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public abstract class ValueObject
    {
        #region Equality

        public override bool Equals(object other)
        {
            if (other is null)
                return false;

            if (other.GetType() != GetType())
                return false;

            return ((ValueObject)other).GetEqualityComponents()
                .SequenceEqual(GetEqualityComponents());
        }

        public abstract IEnumerable<object> GetEqualityComponents();

        public override int GetHashCode() => GetEqualityComponents().GetHashCode();

        #endregion

        #region Static Interface

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right) => right == left;

        #endregion
    }

    [DebuggerDisplay("A:{Attack} D:{Defense} HP:{HitPoints} MA:{MagicAttack} MD:{MagicDefense} S:{Speed}")]
    public class Attributes : ValueObject
    {
        public static readonly Attributes Default = new(0, 0, 0, 0, 0, 0);

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
                (ushort)(left.Attack + right.Attack),
                (ushort)(left.Defense + right.Defense),
                (ushort)(left.HitPoints + right.HitPoints),
                (ushort)(left.MagicAttack + right.MagicAttack),
                (ushort)(left.MagicDefense + right.MagicDefense),
                (ushort)(left.Speed + right.Speed)
            );

        #endregion
    }
}
