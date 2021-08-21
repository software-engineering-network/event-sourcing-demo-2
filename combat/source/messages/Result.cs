namespace EventSourcingDemo.Combat
{
    public class Result
    {
        #region Creation

        public Result(Status status)
        {
            Status = status;
        }

        #endregion

        #region Public Interface

        public Status Status { get; }

        #endregion
    }

    public enum Status
    {
        Failed,
        Succeeded
    }
}
