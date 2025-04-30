using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Models
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = false;  

        public string? Error { get; set; }

        public T? Data { get; set; }
    }
}
