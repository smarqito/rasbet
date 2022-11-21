using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class BetsInTheSameGameException : Exception
{
    public BetsInTheSameGameException()
    {
    }

    public BetsInTheSameGameException(string? message) : base(message)
    {
    }

    public BetsInTheSameGameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BetsInTheSameGameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
