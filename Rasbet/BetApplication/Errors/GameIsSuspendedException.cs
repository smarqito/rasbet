using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class GameIsSuspendedException : Exception
{
    public GameIsSuspendedException()
    {
    }

    public GameIsSuspendedException(string? message) : base(message)
    {
    }

    public GameIsSuspendedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected GameIsSuspendedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
