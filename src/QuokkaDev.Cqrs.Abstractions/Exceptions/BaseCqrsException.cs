using System.Runtime.Serialization;

namespace QuokkaDev.Cqrs.Abstractions.Exceptions
{
    /// <summary>
    /// Base exception for CQRS operations on query and commands
    /// </summary>
    [Serializable]
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

        protected BaseCqrsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Errors = Array.Empty<string>();
        }
    }
}
