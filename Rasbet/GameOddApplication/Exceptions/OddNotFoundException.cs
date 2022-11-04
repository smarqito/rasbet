using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Exceptions
{
    public class OddNotFoundException : Exception
    {
        public OddNotFoundException()
        {
        }

        public OddNotFoundException(string? message) : base(message)
        {
        }

        public OddNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OddNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
