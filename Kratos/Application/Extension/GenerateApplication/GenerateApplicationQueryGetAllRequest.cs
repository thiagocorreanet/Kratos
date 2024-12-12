using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetAllRequest
{
    public static string GenerateCodeApplicationQueryGetAllRequest(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetAllRequest = new StringBuilder();

        stringBuilderQueryGetAllRequest.AppendLine("////// Camada Application > Queries > Nome da pasta da sua entidade > Get All > Request");
        stringBuilderQueryGetAllRequest.AppendLine();
        stringBuilderQueryGetAllRequest.AppendLine("using MediatR;");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"public class Query{convertClassForSingle}GetAllRequest : IRequest<IEnumerable<Query{convertClassForSingle}GetAllResponse>> ");
        stringBuilderQueryGetAllRequest.AppendLine("{ ");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"public List<Query{convertClassForSingle}GetAllResponse> ToResponse(List<Core.Entities.{convertClassForSingle}> entity)");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"List<Query{convertClassForSingle}GetAllResponse> query{convertClassForSingle}GetAllResponses = new List<Query{convertClassForSingle}GetAllResponse>();");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine("foreach (var item in entity)");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine($"query{convertClassForSingle}GetAllResponses.Add(new Query{convertClassForSingle}GetAllResponse");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine("Id = item.Id,");

        foreach(var item in getEntities.PropertyRel)
        {
            stringBuilderQueryGetAllRequest.AppendLine($"{item.Name} = item.{item.Name},");
        }
        stringBuilderQueryGetAllRequest.AppendLine(");");
        stringBuilderQueryGetAllRequest.AppendLine("}");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine("}");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine("return query{convertClassForSingle}GetAllResponses;");
        stringBuilderQueryGetAllRequest.AppendLine("}");
        stringBuilderQueryGetAllRequest.AppendLine("}");

        return stringBuilderQueryGetAllRequest.ToString();

    }
}
