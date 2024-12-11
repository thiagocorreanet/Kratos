using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetByIdResponse
{
    public static string GenerateCodeApplicationQueryGetByIdResponse(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetByIdResponse = new StringBuilder();

        stringBuilderQueryGetByIdResponse.AppendLine("////// Camada de application > Queries > Dentro da pasta com nome da sua entidade > GetById > Response");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderQueryGetByIdResponse.AppendLine($"public class Query{convertClassForSingle}GetByIdResponse");
        stringBuilderQueryGetByIdResponse.AppendLine("{");
        stringBuilderQueryGetByIdResponse.AppendLine(" public int Id { get; set; } ");

        foreach (var item in getEntities.PropertyRel)
        {
            stringBuilderQueryGetByIdResponse.AppendLine($" public {item.Type} {item.Name} {{ get; set; }}");
        }

        stringBuilderQueryGetByIdResponse.AppendLine("}");

        return stringBuilderQueryGetByIdResponse.ToString();
    }
}
