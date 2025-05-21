using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
