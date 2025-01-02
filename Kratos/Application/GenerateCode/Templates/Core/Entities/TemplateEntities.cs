using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Antlr4.StringTemplate;
using Application.Queries.EntityProperty.GetAll;
using Application.Queries.GenerateCode.GetById;
using Microsoft.Extensions.Configuration;

namespace Application.GenerateCode.Templates.Core.Entities;

public class TemplateEntities
{
    private readonly string? _templatePath;

    public TemplateEntities(IConfiguration configuration)
    {
        _templatePath = configuration["TemplateSettings:Core:TemplatePathEntities"];
    }
    
    public async Task<Template> GenerateEntityTemplate(QueryGenerateCodeGetByIdRequest request, string classForSingle, string classForPlural, List<QueryEntityPropertyGetAllResponse> properties)
    {
        if (!File.Exists(_templatePath))
        {
            throw new FileNotFoundException($"Template file not found at path: {_templatePath}");
        }

        var templateContent = await File.ReadAllTextAsync(_templatePath);
        var template = new Template(templateContent);

        var normalizedClassNameSingle = NormalizeName(classForSingle);
        var normalizedClassNamePlural = NormalizeName(classForPlural);

        template.Add("className", normalizedClassNameSingle);

        template.Add("constructorAdd", BuildConstructorAddArgs(properties));
        template.Add("constructorBodyAdd", BuildConstructorBody(properties, onlyRequired: true));
        template.Add("constructorUpdate", BuildConstructorUpdateArgs(properties));
        template.Add("constructorBodyUpdate", BuildConstructorBody(properties, onlyRequired: false));
        template.Add("properties", BuildPropertyDeclarations(properties));


        // Adicionar relacionamentos se necessário
        var relationship = BuildRelationship(properties, normalizedClassNameSingle, normalizedClassNamePlural);
        template.Add("relationships", relationship);

        return template;
    }

    private string BuildConstructorAddArgs(List<QueryEntityPropertyGetAllResponse> properties) =>
        string.Join(", ", properties.Where(p => p.IsRequired).Select(p => $"{p.TypeDataDescription} {p.Name.ToCamelCase()}"));

    private string BuildConstructorBody(List<QueryEntityPropertyGetAllResponse> properties, bool onlyRequired) =>
        string.Join(Environment.NewLine, properties
            .Where(p => !onlyRequired || p.IsRequired)
            .Select(p => $"{p.Name} = {p.Name.ToCamelCase()};"));

    private string BuildConstructorUpdateArgs(List<QueryEntityPropertyGetAllResponse> properties) =>
        string.Join(", ", properties.Select(p => $"{p.TypeDataDescription} {p.Name.ToCamelCase()}"));

    private string BuildPropertyDeclarations(List<QueryEntityPropertyGetAllResponse> properties) =>
        string.Join(Environment.NewLine, properties.Select(p => $"public {p.TypeDataDescription} {p.Name} {{ get; private set; }}"));

    private string BuildRelationship(List<QueryEntityPropertyGetAllResponse> properties, string normalizedClassName, string normalizedClassNamePlural)
    {
        var relationshipProperty = properties.FirstOrDefault(p => p.IsRequiredRel);
        return relationshipProperty != null
            ? $"public List<{normalizedClassName}> {normalizedClassNamePlural}Rel {{ get; private set; }}"
            : string.Empty;
    }

    private static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The name cannot be null or empty.", nameof(name));

        return RemoveAccents(name);
    }

    private static string RemoveAccents(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;

        var normalized = text.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
    }

}

public static class StringExtensions
{
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 2)
            return input.ToLower();

        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static string RemoveWhitespace(this string input)
    {
        return Regex.Replace(input, @"\s+", "");
    }
}
