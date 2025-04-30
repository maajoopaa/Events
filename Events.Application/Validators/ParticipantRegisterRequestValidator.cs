using Events.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Validators
{
    public class ParticipantRegisterRequestValidator : AbstractValidator<ParticipantRegisterRequest>
    {
        public ParticipantRegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname cannot be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lastname cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
