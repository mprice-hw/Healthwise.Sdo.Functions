using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdo.functions.Exceptions
{
    internal class InvalidEventTypeException : Exception
    {
        public InvalidEventTypeException(string message) : base(message) { }
    }
}
