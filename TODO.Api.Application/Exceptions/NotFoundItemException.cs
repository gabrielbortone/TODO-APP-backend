namespace TODO.Api.Application.Exceptions
{
    public class NotFoundItemException : Exception
    {
        public NotFoundItemException() : base("The requested item was not found.")
        {
        }
        public NotFoundItemException(string message) : base(message)
        {
        }
        public NotFoundItemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
