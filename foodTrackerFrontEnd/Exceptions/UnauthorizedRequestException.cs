namespace foodTrackerFrontEnd.Exceptions
{
    [Serializable]
    public class UnauthorizedRequestException : Exception
    {
        public UnauthorizedRequestException() { }

        public UnauthorizedRequestException(string message)
            : base(message) { }

        public UnauthorizedRequestException(string message, Exception inner)
            : base(message, inner) { }
    }
}
