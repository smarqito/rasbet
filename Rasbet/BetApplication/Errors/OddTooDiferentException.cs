using System.Runtime.Serialization;

namespace BetApplication.Errors;

public class OddTooDiferentException : Exception
{
    public OddTooDiferentException()
    {
    }

    public OddTooDiferentException(string? message) : base(message)
    {
    }

    public OddTooDiferentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected OddTooDiferentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
