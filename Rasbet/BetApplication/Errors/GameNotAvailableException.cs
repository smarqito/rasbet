using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BetApplication.Errors;

public class GameNotAvailableException : Exception
{
    public GameNotAvailableException()
    {
    }

    public GameNotAvailableException(string? message) : base(message)
    {
    }

    public GameNotAvailableException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected GameNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
