using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Antlr4.StringTemplate;
using Microsoft.Extensions.Configuration;

namespace Application.GenerateCode.Templates.Core.Abstract;

public class TemplateAbstract
{
    private readonly string? _templatePath;


    public TemplateAbstract(IConfiguration configuration)
    {
        _templatePath = configuration["TemplateSettings:Core:TemplatePathAbstract"];
    }

    public async Task<Template> GenerateAbstractTemplate(string classForSingle)
    {
        if (!File.Exists(_templatePath))
        {
            throw new FileNotFoundException($"Template file not found at path: {_templatePath}");
        }
        
        var templateContent = await File.ReadAllTextAsync(_templatePath);
        var template = new Template(templateContent);
        
        var normalizedClassNameSingle = NormalizeName(classForSingle);
        
        template.Add("className", normalizedClassNameSingle);
        template.Add("baseRepository", $"IBaseRepository<{normalizedClassNameSingle}>");
        
        return template;
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

