using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationValidatorUpdate
{
    public static string GenerateCodeApplicationValidatorUpdate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderUpdateValidator = new StringBuilder();

        stringBuilderUpdateValidator.AppendLine("////// Camada de application > Validators");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($"using Application.Commands.{convertClassForSingle}.Update;");
        stringBuilderUpdateValidator.AppendLine("using FluentValidation;");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($"namespace Application.Validators.{convertClassForSingle};");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($"public class Update{convertClassForSingle}CommandValidator : AbstractValidator<Update{convertClassForSingle}CommandRequest>");
        stringBuilderUpdateValidator.AppendLine("{");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($" public Update{convertClassForSingle}CommandValidator()");
        stringBuilderUpdateValidator.AppendLine("{");
        stringBuilderUpdateValidator.AppendLine();

        foreach (var property in getEntities.PropertyRel.Where(x => x.IsRequired == true && x.Type.Equals("string")))
        {
            stringBuilderUpdateValidator.AppendLine($"RuleFor(x => x.{property.Name})");
            stringBuilderUpdateValidator.AppendLine(".NotEmpty().WithMessage(\"Field is empty, the email is required.\")");
            stringBuilderUpdateValidator.AppendLine(".NotNull().WithMessage(\"The email is null, please enter a valid email.\")");
            stringBuilderUpdateValidator.AppendLine($".MaximumLength(maximumLength: {property.QuantityCaracter}).WithMessage(errorMessage: \"The field accepts a maximum of {property.QuantityCaracter} characters.\");");
            stringBuilderUpdateValidator.AppendLine();
        }

        stringBuilderUpdateValidator.AppendLine("}");
        stringBuilderUpdateValidator.AppendLine("}");

        return stringBuilderUpdateValidator.ToString();
    }
}
