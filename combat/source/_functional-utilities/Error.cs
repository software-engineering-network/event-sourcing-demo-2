using System;

namespace EventSourcingDemo.Combat
{
    public class Error : IEquatable<Error>
    {
        #region Creation

        public Error(string code, string message = "whoops")
        {
            Code = code;
            Message = message;
        }

        #endregion

        #region Implementation

        public string Code { get; }
        public string Message { get; }

        #endregion

        #region Equality

        public bool Equals(Error other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Code == other.Code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Error) obj);
        }

        public override int GetHashCode() => HashCode.Combine(Code, Message);

        #endregion

        #region Static Interface

        public static bool operator ==(Error left, Error right) => Equals(left, right);

        public static bool operator !=(Error left, Error right) => !Equals(left, right);

        #endregion
    }
}
