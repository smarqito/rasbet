using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class UserBalanceTooLowException : Exception
{
    public UserBalanceTooLowException()
    {
    }

    public UserBalanceTooLowException(string? message) : base(message)
    {
    }

    public UserBalanceTooLowException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UserBalanceTooLowException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
