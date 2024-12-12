using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateInfrastructure;

public class GenerateInfrastructureConfiguration
{
    public static string GenerateCodeInfrastructureConfiguration(Entity getEntities, string convertClassForSingle)
    {
        string typeCustom = string.Empty;
        int counter = 2;
        var stringBuilderConfiguration = new StringBuilder();

        stringBuilderConfiguration.AppendLine("////// Camada Infraestrutura > Dentro da pasta Persistence > Configuration");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("using Core.Entities;");
        stringBuilderConfiguration.AppendLine("using Microsoft.EntityFrameworkCore;");
        stringBuilderConfiguration.AppendLine("using Microsoft.EntityFrameworkCore.Metadata;");
        stringBuilderConfiguration.AppendLine("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("namespace Infrastructure.Persistence.Configuration;");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine($"public class Configuration{convertClassForSingle} : IEntityTypeConfiguration<{convertClassForSingle}>");
        stringBuilderConfiguration.AppendLine("{");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine($"public void Configure(EntityTypeBuilder<{convertClassForSingle}> builder)");
        stringBuilderConfiguration.AppendLine("{");

        stringBuilderConfiguration.AppendLine($"builder.ToTable(\"{getEntities.Name}\");");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("builder.HasKey(x => x.Id)");
        stringBuilderConfiguration.AppendLine($"    .HasName(\"PK_{getEntities.Name.ToUpper()}\");");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine(" builder.Property(c => c.Id)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnName(\"{convertClassForSingle}Id\")");
        stringBuilderConfiguration.AppendLine($"    .HasColumnOrder(1)");
        stringBuilderConfiguration.AppendLine($"    .ValueGeneratedOnAdd();");
        stringBuilderConfiguration.AppendLine();

        foreach (var property in getEntities.PropertyRel)
        {
            // Obtemos o tipo customizado baseado no mapeamento
            typeCustom = GetMappedType(property.Type, property.QuantityCaracter);

            // Construímos a configuração do mapeamento
            stringBuilderConfiguration.AppendLine($"builder.Property(c => c.{property.Name})");
            stringBuilderConfiguration.AppendLine($"    .HasColumnName(\"{property.Name}\")");
            stringBuilderConfiguration.AppendLine($"    .HasColumnOrder({counter})");
            stringBuilderConfiguration.AppendLine($"    .IsRequired({property.IsRequired.ToString().ToLower()})");
            stringBuilderConfiguration.AppendLine($"    .HasColumnType(\"{typeCustom}\");");
            stringBuilderConfiguration.AppendLine();

            counter++;
        }

        stringBuilderConfiguration.AppendLine("builder.Property(x => x.CreatedAt)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnOrder({counter})");
        stringBuilderConfiguration.AppendLine($"    .IsRequired(true)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnType(\"DATETIME2\")");
        stringBuilderConfiguration.AppendLine($"    .HasDefaultValueSql(\"GETDATE()\")");
        stringBuilderConfiguration.AppendLine($"    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("builder.Property(x => x.AlteredAt)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnOrder({counter + 1})");
        stringBuilderConfiguration.AppendLine($"    .IsRequired(true)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnType(\"DATETIME2\")");
        stringBuilderConfiguration.AppendLine($"    .HasDefaultValueSql(\"GETDATE()\")");
        stringBuilderConfiguration.AppendLine($"    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine();
        stringBuilderConfiguration.AppendLine(" builder.HasIndex(c => c.Id)");
        stringBuilderConfiguration.AppendLine($"     .HasDatabaseName(\"IX_{convertClassForSingle.ToUpper()}_ID\")");
        stringBuilderConfiguration.AppendLine($"     .IsUnique();");
        stringBuilderConfiguration.AppendLine("}");
        stringBuilderConfiguration.AppendLine("}");

        return stringBuilderConfiguration.ToString();
    }

    private static string GetMappedType(string type, int? quantityCaracter = null)
    {
        var mappingOfTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "int", "INT" },
        { "string", "VARCHAR" },
        { "decimal", "DECIMAL" },
        { "bool", "BIT" },
        { "datetime", "DATETIME" }
    };

        // Verifica se o tipo está mapeado
        if (!mappingOfTypes.TryGetValue(type, out var mappedType))
        {
            throw new InvalidOperationException($"O tipo '{type}' não está mapeado.");
        }

        // Se for string, adicionamos o tamanho do campo, se fornecido
        if (type.Equals("string", StringComparison.OrdinalIgnoreCase) && quantityCaracter.HasValue && quantityCaracter > 0)
        {
            return $"{mappedType}({quantityCaracter})";
        }

        return mappedType;
    }

}
