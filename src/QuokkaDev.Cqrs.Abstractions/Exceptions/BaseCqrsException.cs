namespace QuokkaDev.Cqrs.Abstractions.Exceptions
{
    /// <summary>
    /// Base exception for CQRS operations on query and commands
    /// </summary>
    public class BaseCqrsException : ApplicationException
    {
        public IReadOnlyCollection<string> Errors { get; }

        public BaseCqrsException(string[] errors)
        {
            Errors = errors;
        }

        public BaseCqrsException() : this("", null)
        {
        }

        public BaseCqrsException(string? message) : this(message, null)
        {
        }

        public BaseCqrsException(string? message, Exception? innerException) : base(message, innerException)
        {
            Errors = new string[] { "" + message };
        }
    }
}
