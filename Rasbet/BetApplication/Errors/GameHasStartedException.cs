using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class GameHasStartedException : Exception
{
    public GameHasStartedException()
    {
    }

    public GameHasStartedException(string? message) : base(message)
    {
    }

    public GameHasStartedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected GameHasStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
