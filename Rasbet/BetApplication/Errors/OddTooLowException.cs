using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class OddTooLowException : Exception
{
    public OddTooLowException()
    {
    }

    public OddTooLowException(string? message) : base(message)
    {
    }

    public OddTooLowException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected OddTooLowException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
