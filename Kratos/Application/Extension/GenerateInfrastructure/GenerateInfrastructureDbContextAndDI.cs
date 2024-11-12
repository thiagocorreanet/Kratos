using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateInfrastructure;

public class GenerateInfrastructureDbContextAndDI
{
    public static string GenerateCodeInfrastructureDbContextAndDI(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeDbContextAndDI = new StringBuilder();


        stringBuildeDbContextAndDI.AppendLine("////// Camada Infraestrutura > Dentro da pasta Persistence > Na classe DbContext");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine($"public DbSet<{convertClassForSingle}> {getEntities.Name} => Set<{convertClassForSingle}>();");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine("////// Camada Infraestrutura > Dentro da classe de dependency injection > Método AddRepositories");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine($"services.AddScoped<I{convertClassForSingle}Repository, {convertClassForSingle}Repository>();");
        stringBuildeDbContextAndDI.AppendLine();

        return stringBuildeDbContextAndDI.ToString();
    }
}
