using Application.Commands.Entity.Create;
using FluentValidation;

namespace Application.Validators.Entity
{
    public class CreateEntitieCommandValidator : AbstractValidator<CreateEntityCommandRequest>
    {
        public CreateEntitieCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field is empty, the email is required.")
                .NotNull().WithMessage("The email is null, please enter a valid email.")
                .MaximumLength(maximumLength: 50).WithMessage(errorMessage: "The field accepts a maximum of 50 characters.");
        }
    }
}
