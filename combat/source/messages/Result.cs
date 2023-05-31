using System;

namespace EventSourcingDemo.Combat
{
    public class Result
    {
        #region Creation

        public Result(string message = default)
        {
            Message = message;
            WasFailure = message == string.Empty;
        }

        #endregion

        #region Public Interface

        public string Message { get; }
        public bool WasFailure { get; }
        public bool WasSuccessful => !WasFailure;

        #endregion

        #region Static Interface

        public static Result Failure() => new("failure");
        public static Result Success() => new();

        #endregion
    }

    public class Result<T> : Result
    {
        #region Creation

        private Result(T value)
        {
            Value = value;
        }

        #endregion

        #region Public Interface

        public T Value { get; }
        public Result Bind(Func<T, Result> next) => WasFailure ? this : next(Value);

        #endregion

        #region Static Interface

        public static implicit operator Result<T>(T value) => new(value);

        #endregion

        //public static implicit operator T(Result<T> source) => source.Value;
    }
}
