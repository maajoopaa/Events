using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Exceptions
{
    public class ClientException : Exception
    {

        public ClientException(string message, int statusCode)
            : base(message)
        {
            Message = message;
            StatusCode = statusCode;
        }
        public string Message { get; set; }

        public int StatusCode { get; set; }
    }
}
