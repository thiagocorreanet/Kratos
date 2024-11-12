using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetAllRequest
{
    public static string GenerateCodeApplicationQueryGetAllRequest(string convertClassForSingle)
    {
        var stringBuilderQueryGetAllRequest = new StringBuilder();

        stringBuilderQueryGetAllRequest.AppendLine("////// Camada Application > Queries > Nome da pasta da sua entidade > Get All > Request");
        stringBuilderQueryGetAllRequest.AppendLine();
        stringBuilderQueryGetAllRequest.AppendLine("using MediatR;");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"public class Query{convertClassForSingle}GetAllRequest : IRequest<IEnumerable<Query{convertClassForSingle}GetAllResponse>> {{}}");

        return stringBuilderQueryGetAllRequest.ToString();

    }
}
