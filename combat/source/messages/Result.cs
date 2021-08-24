using System;

namespace EventSourcingDemo.Combat
{
    public class Result
    {
        #region Creation

        public Result(Guid id, Status status)
        {
            Id = id;
            Status = status;
        }

        #endregion

        #region Public Interface

        public Guid Id { get; }
        public Status Status { get; }

        #endregion
    }

    public enum Status
    {
        Failed,
        Succeeded
    }
}
