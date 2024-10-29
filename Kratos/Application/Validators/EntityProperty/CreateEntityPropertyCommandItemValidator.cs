using Application.Commands.EntityProperty.Create;
using FluentValidation;

namespace Application.Validators.EntityProperty;

public class CreateEntityPropertyCommandItemValidator : AbstractValidator<CreateEntityPropertyRequest>
{
    public CreateEntityPropertyCommandItemValidator()
    {
        RuleForEach(x => x.Items).ChildRules(i =>
        {
            i.RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Property name is required.")
                .MaximumLength(50).WithMessage("Property name accepts a maximum of 50 characters.");
            
            i.RuleFor(p => p.Type)
                .NotEmpty().WithMessage("Property type is required.")
                .MaximumLength(50).WithMessage("Property name accepts a maximum of 50 characters.");
            
            i.RuleFor(p => p.EntityId)
                .GreaterThan(0).WithMessage("EntityId invalid.");
        });
    }
}