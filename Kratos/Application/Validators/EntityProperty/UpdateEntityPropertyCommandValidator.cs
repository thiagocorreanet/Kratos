using Application.Commands.EntityProperty.Update;
using FluentValidation;

namespace Application.Validators.EntityProperty;

public class UpdateEntityPropertyCommandValidator : AbstractValidator<UpdateEntityPropertyRequest>
{
    public UpdateEntityPropertyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Property name is required.")
            .MaximumLength(50).WithMessage("Property name accepts a maximum of 50 characters.");
        
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Property type is required.")
            .MaximumLength(50).WithMessage("Property name accepts a maximum of 50 characters.");
        
        RuleFor(x => x.EntityId)
            .GreaterThan(0).WithMessage("EntityId invalid.");
    }
}