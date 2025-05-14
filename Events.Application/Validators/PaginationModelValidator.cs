using Events.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Validators
{
    public class PaginationModelValidator : AbstractValidator<PaginationModel>
    {
        public PaginationModelValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0).WithMessage("Page cannot be less then 0");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("Pagesize cannot be less then 0");
        }
    }
}
