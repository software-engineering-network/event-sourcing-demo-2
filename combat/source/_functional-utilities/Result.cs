using System;

namespace EventSourcingDemo.Combat
{
    public class Result
    {
        #region Creation

        public Result(Error error = default)
        {
            Error = error;
        }

        #endregion

        #region Implementation

        public Error Error { get; }
        public bool WasFailure => !WasSuccessful;
        public bool WasSuccessful => Error is null;

        public Result Bind(Func<Result> next) => WasFailure ? Error : next();
        public Result<T> Bind<T>(Func<Result<T>> next) => WasFailure ? Error : next();

        #endregion

        #region Static Interface

        public static Result Success() => new();
        public static implicit operator Result(Error error) => new(error);

        #endregion
    }

    public class Result<T> : Result
    {
        #region Creation

        private Result(T value)
        {
            Value = value;
        }

        private Result(Error error) : base(error)
        {
        }

        #endregion

        #region Implementation

        public T Value { get; }
        public Result Bind(Func<T, Result> next) => WasFailure ? this : next(Value);
        public Result<T2> Bind<T2>(Func<T, Result<T2>> next) => WasFailure ? Error : next(Value);
        public T OnError(Func<T> onError) => WasFailure ? onError() : Value;

        #endregion

        #region Static Interface

        public static implicit operator Result<T>(T value) => new(value);
        public static implicit operator Result<T>(Error error) => new(error);
        public static implicit operator T(Result<T> source) => source.Value;

        #endregion
    }
}
