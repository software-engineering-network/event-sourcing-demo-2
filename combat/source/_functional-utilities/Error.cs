namespace EventSourcingDemo.Combat
{
    public class Error
    {
        #region Creation

        public Error(string code, string message = "whoops")
        {
            Code = code;
            Message = message;
        }

        #endregion

        #region Public Interface

        public string Code { get; }
        public string Message { get; }

        #endregion
    }
}
