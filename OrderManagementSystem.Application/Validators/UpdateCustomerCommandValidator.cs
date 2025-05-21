using FluentValidation;
using OrderManagementSystem.Application.Commands;

namespace OrderManagementSystem.Application.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Customer ID is required");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required and must not exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .WithMessage("A valid email address is required");

            RuleFor(x => x.CustomerSegment)
                .NotEmpty()
                .Must(segment => new[] { "premium", "regular", "basic" }.Contains(segment.ToLower()))
                .WithMessage("Customer segment must be one of: premium, regular, basic");
        }
    }
} 