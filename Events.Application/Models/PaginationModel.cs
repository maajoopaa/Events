using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Models
{
    public class PaginationModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 10;
    }
}
