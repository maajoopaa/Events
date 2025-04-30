using Events.Application.Models;
using FluentValidation;

namespace Events.Application.Validators
{
    public class EventRequestValidator : AbstractValidator<EventRequest>
    {
        public EventRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be null");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be null");
            RuleFor(x => x.Venue).NotEmpty().WithMessage("Venue cannot be null");
            RuleFor(x => x.MaxCountOfParticipant).GreaterThanOrEqualTo(1).WithMessage("Max participant count cannot be less then 1");
        }
    }
}
