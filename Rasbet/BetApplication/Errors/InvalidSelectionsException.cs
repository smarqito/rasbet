using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class InvalidSelectionsException : Exception
{
    public InvalidSelectionsException()
    {
    }

    public InvalidSelectionsException(string? message) : base(message)
    {
    }

    public InvalidSelectionsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidSelectionsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
