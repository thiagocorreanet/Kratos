using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationValidatorCreate
{
    public static string GenerateCodeApplicationValidatorCreate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderCreateValidator = new StringBuilder();
        stringBuilderCreateValidator.AppendLine("////// Camada Application > Pasta Validators > Pasta com o nome da sua entidade");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($"using Application.Commands.{convertClassForSingle}.Create;");
        stringBuilderCreateValidator.AppendLine("using FluentValidation;");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($"namespace Application.Validators.{convertClassForSingle};");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($"public class Create{convertClassForSingle}CommandItemValidator : AbstractValidator<Create{convertClassForSingle}Request>");
        stringBuilderCreateValidator.AppendLine("{");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($" public Create{convertClassForSingle}CommandItemValidator()");
        stringBuilderCreateValidator.AppendLine("{");
        stringBuilderCreateValidator.AppendLine();

        foreach (var property in getEntities.PropertyRel.Where(x => x.IsRequired == true && x.Type.Equals("string")))
        {
            stringBuilderCreateValidator.AppendLine($"RuleFor(x => x.{property.Name})");
            stringBuilderCreateValidator.AppendLine(".NotEmpty().WithMessage(\"Field is empty, the email is required.\")");
            stringBuilderCreateValidator.AppendLine(".NotNull().WithMessage(\"The email is null, please enter a valid email.\")");
            stringBuilderCreateValidator.AppendLine($".MaximumLength(maximumLength: {property.QuantityCaracter}).WithMessage(errorMessage: \"The field accepts a maximum of {property.QuantityCaracter} characters.\");");
            stringBuilderCreateValidator.AppendLine();
        }

        stringBuilderCreateValidator.AppendLine("}");
        stringBuilderCreateValidator.AppendLine("}");

        return stringBuilderCreateValidator.ToString();
    }
}
