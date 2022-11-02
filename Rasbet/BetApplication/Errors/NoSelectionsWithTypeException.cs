using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class NoSelectionsWithTypeException : Exception
{
    public NoSelectionsWithTypeException()
    {
    }

    public NoSelectionsWithTypeException(string? message) : base(message)
    {
    }

    public NoSelectionsWithTypeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NoSelectionsWithTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
