using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateInfrastructure;

public class GenerateInfrastructureRepository
{
    public static string GenerateCodeInfrastructureRepository(string convertClassForSingle)
    {
        var stringBuildeDbContextAndDI = new StringBuilder();


        stringBuildeDbContextAndDI.AppendLine("////// Camada Infraestrutura > Dentro da pasta Repositories");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine("using Core.Entities;");
        stringBuildeDbContextAndDI.AppendLine("using Core.Repositories;");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine("namespace Infrastructure.Persistence.Repositories;");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine($"public class {convertClassForSingle}Repository : BaseRepository<{convertClassForSingle}>, I{convertClassForSingle}Repository");
        stringBuildeDbContextAndDI.AppendLine("{");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine($" public {convertClassForSingle}Repository(DbContextBetaUp context) : base(context) {{}}");
        stringBuildeDbContextAndDI.AppendLine("}");



        return stringBuildeDbContextAndDI.ToString();
    }
}
