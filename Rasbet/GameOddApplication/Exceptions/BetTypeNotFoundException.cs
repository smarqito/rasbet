using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Exceptions
{
    public class BetTypeNotFoundException : Exception
    {
        public BetTypeNotFoundException(string? message) : base(message)
        {
        }
    }
}
