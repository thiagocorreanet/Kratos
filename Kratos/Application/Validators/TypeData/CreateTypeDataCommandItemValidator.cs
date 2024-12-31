using Application.Commands.TypeData.Create;
using FluentValidation;

namespace Application.Validators.TypeData;

public class CreateTypeDataCommandItemValidator : AbstractValidator<CreateTypeDataCommandRequest>
{
    public CreateTypeDataCommandItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Field is empty, the name filed is required.")
            .NotNull().WithMessage("The name field is null, please enter a valid text name.")
            .MaximumLength(maximumLength: 50).WithMessage(errorMessage: "The field accepts a maximum of 50 characters.");
    }
}