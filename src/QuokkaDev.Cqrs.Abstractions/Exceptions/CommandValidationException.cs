namespace QuokkaDev.Cqrs.Abstractions.Exceptions
{
    /// <summary>
    /// An exception raised when a command request is invalid
    /// </summary>
    public class CommandValidationException : BaseCqrsException
    {
        public CommandValidationException(string[] errors) : base(errors)
        {
        }

        public CommandValidationException() : base()
        {
        }

        public CommandValidationException(string? message) : base(message)
        {
        }

        public CommandValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
