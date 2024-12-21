using Application.Commands.Project.Update;
using FluentValidation;

namespace Application.Validators.Project;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommandRequest>
{

    public UpdateProjectCommandValidator()
    {

        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Field is empty, the email is required.")
        .NotNull().WithMessage("The email is null, please enter a valid email.")
        .MaximumLength(maximumLength: 50).WithMessage(errorMessage: "The field accepts a maximum of 50 characters.");

    }
}