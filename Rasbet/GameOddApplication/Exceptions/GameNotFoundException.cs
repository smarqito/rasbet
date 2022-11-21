using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(string? message) : base(message)
        {
        }
    }
}
