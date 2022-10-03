using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthwise.Sdo.Functions.Exceptions
{
    internal class InvalidEventTypeException : Exception
    {
        public InvalidEventTypeException(string message) : base(message) { }
    }
}
