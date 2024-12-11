using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetByIdRequest
{
    public static string GenerateCodeApplicationQueryGetByIdRequest(string convertClassForSingle)
    {
        var stringBuilderQueryGetByIdRequest = new StringBuilder();

        stringBuilderQueryGetByIdRequest.AppendLine("////// Camada Application > Queries > Pasta com o nome da sua entidade > Get By Id > Response");
        stringBuilderQueryGetByIdRequest.AppendLine();

        stringBuilderQueryGetByIdRequest.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderQueryGetByIdRequest.AppendLine();

        stringBuilderQueryGetByIdRequest.AppendLine($"public class Query{convertClassForSingle}GetByIdRequest : IRequest<Query{convertClassForSingle}GetByIdResponse>");
        stringBuilderQueryGetByIdRequest.AppendLine("{");
        stringBuilderQueryGetByIdRequest.AppendLine($"public Query{convertClassForSingle}GetByIdRequest(int id)");
        stringBuilderQueryGetByIdRequest.AppendLine("{");
        stringBuilderQueryGetByIdRequest.AppendLine("Id = id;");
        stringBuilderQueryGetByIdRequest.AppendLine("}");
        stringBuilderQueryGetByIdRequest.AppendLine();

        stringBuilderQueryGetByIdRequest.AppendLine(" public int Id { get; set; }");
        stringBuilderQueryGetByIdRequest.AppendLine(" }");

        return stringBuilderQueryGetByIdRequest.ToString();
    }
}
