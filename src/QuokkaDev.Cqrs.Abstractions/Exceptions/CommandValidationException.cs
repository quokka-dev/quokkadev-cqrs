using System.Runtime.Serialization;

namespace QuokkaDev.Cqrs.Abstractions.Exceptions
{
    /// <summary>
    /// An exception raised when a command request is invalid
    /// </summary>
    [Serializable]
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

        protected CommandValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
