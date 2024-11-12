using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetByIdResponse
{
    public static string GenerateCodeApplicationQueryGetByIdResponse(string convertClassForSingle)
    {
        var stringBuilderQueryGetByIdResponse = new StringBuilder();

        stringBuilderQueryGetByIdResponse.AppendLine("////// Camada Application > Queries > Pasta com o nome da sua entidade > Get By Id > Response");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine($"public class Query{convertClassForSingle}GetByIdRequest : IRequest<Query{convertClassForSingle}GetByIdResponse>");
        stringBuilderQueryGetByIdResponse.AppendLine("{");
        stringBuilderQueryGetByIdResponse.AppendLine($"public Query{convertClassForSingle}GetByIdRequest(int id)");
        stringBuilderQueryGetByIdResponse.AppendLine("{");
        stringBuilderQueryGetByIdResponse.AppendLine("Id = id;");
        stringBuilderQueryGetByIdResponse.AppendLine("}");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine(" public int Id { get; set; }");
        stringBuilderQueryGetByIdResponse.AppendLine(" }");

        return stringBuilderQueryGetByIdResponse.ToString();
    }
}
