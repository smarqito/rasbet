using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class UserWithoutBetsException : Exception
{
    public UserWithoutBetsException()
    {
    }

    public UserWithoutBetsException(string? message) : base(message)
    {
    }

    public UserWithoutBetsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UserWithoutBetsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
