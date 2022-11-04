using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class FinishedGamesInvalidException : Exception
{
    public FinishedGamesInvalidException()
    {
    }

    public FinishedGamesInvalidException(string? message) : base(message)
    {
    }

    public FinishedGamesInvalidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected FinishedGamesInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
