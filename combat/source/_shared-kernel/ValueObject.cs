using System.Collections.Generic;
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
}
