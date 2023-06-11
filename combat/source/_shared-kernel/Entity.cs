using System;

namespace EventSourcingDemo.Combat
{
    public abstract class Entity
    {
        #region Creation

        protected Entity(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Implementation

        public Guid Id { get; }

        #endregion

        #region Equality

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other.GetType() != GetType())
                return false;

            return ((Entity) other).Equals(this);
        }

        protected bool Equals(Entity other) => Id.Equals(other.Id);

        public override int GetHashCode() => Id.GetHashCode();

        #endregion

        #region Static Interface

        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null)
                return right is null;

            if (right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right) => !Equals(left, right);

        #endregion
    }
}
