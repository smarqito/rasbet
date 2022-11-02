using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class BetTooLowException : Exception
{
    public BetTooLowException()
    {
    }

    public BetTooLowException(string? message) : base(message)
    {
    }

    public BetTooLowException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BetTooLowException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
