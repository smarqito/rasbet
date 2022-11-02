using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class BetNotFoundException : Exception
{
    public BetNotFoundException()
    {
    }

    public BetNotFoundException(string? message) : base(message)
    {
    }

    public BetNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
