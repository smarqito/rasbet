using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class NoSelectionsInGameException : Exception
{
    public NoSelectionsInGameException()
    {
    }

    public NoSelectionsInGameException(string? message) : base(message)
    {
    }

    public NoSelectionsInGameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NoSelectionsInGameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
