using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Exceptions
{
    public class SameGameStateException : Exception
    {
        public SameGameStateException()
        {
        }

        public SameGameStateException(string? message) : base(message)
        {
        }

        public SameGameStateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SameGameStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
