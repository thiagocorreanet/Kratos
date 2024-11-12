using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateCore;

public static class GenerateEntity
{
    public static string GenerateCodeEntity(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderEntity = new StringBuilder();

        stringBuilderEntity.AppendLine("////// Camada Core > Pasta Entities");
        stringBuilderEntity.AppendLine();

        stringBuilderEntity.AppendLine("namespace Core.Entities;");
        stringBuilderEntity.AppendLine();
        stringBuilderEntity.AppendLine($"public class {convertClassForSingle} : BaseEntity");
        stringBuilderEntity.AppendLine("{");
        stringBuilderEntity.AppendLine();

        stringBuilderEntity.AppendLine($"protected {convertClassForSingle}()");
        stringBuilderEntity.AppendLine("{ }");
        stringBuilderEntity.AppendLine();

        stringBuilderEntity.Append($"public {convertClassForSingle}(");
        stringBuilderEntity.Append(string.Join(", ", getEntities.PropertyRel.Select(p => $"{p.Type} {ToCamelCase(p.Name)}")));
        stringBuilderEntity.AppendLine(")");
        stringBuilderEntity.AppendLine("{");
        foreach (var item in getEntities.PropertyRel)
        {
            stringBuilderEntity.AppendLine($"    {item.Name} = {ToCamelCase(item.Name)};");
        }
        stringBuilderEntity.AppendLine("}");
        stringBuilderEntity.AppendLine();

        foreach (var item in getEntities.PropertyRel)
        {
            stringBuilderEntity.AppendLine($"public {item.Type} {item.Name} {{ get; private set; }}");
        }

        stringBuilderEntity.AppendLine("}");

        return stringBuilderEntity.ToString();
    }

    private static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        if (input == input.ToLower())
        {
            return input;
        }

        var words = System.Text.RegularExpressions.Regex.Split(input, @"(?<!^)(?=[A-Z])");

        var result = new StringBuilder(words[0].ToLower());
        for (int i = 1; i < words.Length; i++)
        {
            result.Append(char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower());
        }

        return result.ToString();
    }
}
