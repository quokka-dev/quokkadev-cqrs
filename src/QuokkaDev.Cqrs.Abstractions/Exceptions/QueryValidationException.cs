using System.Runtime.Serialization;

namespace QuokkaDev.Cqrs.Abstractions.Exceptions
{
    /// <summary>
    /// An exception raised when a query request is invalid
    /// </summary>
    [Serializable]
    public class QueryValidationException : BaseCqrsException
    {
        public QueryValidationException(string[] errors) : base(errors)
        {
        }

        public QueryValidationException() : base()
        {
        }

        public QueryValidationException(string? message) : base(message)
        {
        }

        public QueryValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QueryValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
