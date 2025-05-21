using FluentValidation;
using OrderManagementSystem.Application.Commands;
using System.Linq;

namespace OrderManagementSystem.Application.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.CustomerSegment)
                .NotEmpty()
                .MaximumLength(50)
                .Must(segment => new[] { "premium", "regular", "basic" }.Contains(segment.ToLower()))
                .WithMessage("Customer segment must be one of: premium, regular, basic");
        }
    }
} 