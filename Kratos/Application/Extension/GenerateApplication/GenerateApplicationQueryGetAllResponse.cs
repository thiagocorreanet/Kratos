using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetAllResponse
{
    public static string GenerateCodeApplicationQueryGetAllResponse(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetAll = new StringBuilder();
        stringBuilderQueryGetAll.AppendLine("////// Camada de application > Queries > Dentro da pasta com nome da sua entidade > GetAll > Response");
        stringBuilderQueryGetAll.AppendLine();

        stringBuilderQueryGetAll.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAll.AppendLine();

        stringBuilderQueryGetAll.AppendLine($"public class Query{convertClassForSingle}GetAllResponse");
        stringBuilderQueryGetAll.AppendLine("{");
        stringBuilderQueryGetAll.AppendLine(" public int Id { get; set; }");

        foreach (var property in getEntities.PropertyRel)
        {
            stringBuilderQueryGetAll.AppendLine($" public {property.Type} {property.Name} {{ get; set; }}");
        }

        stringBuilderQueryGetAll.AppendLine("}");

        return stringBuilderQueryGetAll.ToString();
    }
}
