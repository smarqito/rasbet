using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class SelectionNotExistException : Exception
{
    public SelectionNotExistException()
    {
    }

    public SelectionNotExistException(string? message) : base(message)
    {
    }

    public SelectionNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected SelectionNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
