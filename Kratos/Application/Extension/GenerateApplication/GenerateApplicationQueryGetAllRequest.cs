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

        // Usings
        stringBuilderQueryGetAllRequest.AppendLine("using MediatR;");
        stringBuilderQueryGetAllRequest.AppendLine();

        // Namspace
        stringBuilderQueryGetAllRequest.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAllRequest.AppendLine();

        // Criação da classe
        stringBuilderQueryGetAllRequest.AppendLine($"public class Query{convertClassForSingle}GetAllRequest : IRequest<IEnumerable<Query{convertClassForSingle}GetAllResponse>>");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine();

        // Criação do Método de retorno
        stringBuilderQueryGetAllRequest.AppendLine($"public List<Query{convertClassForSingle}GetAllResponse> ToResponse(List<Core.Entities.{convertClassForSingle}> entity)");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine();

        // Criação da lista para o retorno
        stringBuilderQueryGetAllRequest.AppendLine($" List<Query{convertClassForSingle}GetAllResponse> query{convertClassForSingle}GetAllResponses = new List<Query{convertClassForSingle}GetAllResponse>();");
        stringBuilderQueryGetAllRequest.AppendLine();

        // Inicio do foreach De/Para
        stringBuilderQueryGetAllRequest.AppendLine("foreach (var item in entity)");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine($"query{convertClassForSingle}GetAllResponses.Add(new Query{convertClassForSingle}GetAllResponse");
        stringBuilderQueryGetAllRequest.AppendLine("{");
        stringBuilderQueryGetAllRequest.AppendLine("Id = item.Id,");

        var requiredProperties = getEntities.PropertyRel.ToList();
        for (int i = 0; i < requiredProperties.Count; i++)
        {
            var property = requiredProperties[i];
            var isLast = i == requiredProperties.Count - 1;

            stringBuilderQueryGetAllRequest.AppendLine(isLast
                ? $" {property.Name} = item.{property.Name}"
                : $" {property.Name} = item.{property.Name},");
        }

        // finalização do foreach
        stringBuilderQueryGetAllRequest.AppendLine("});");
        stringBuilderQueryGetAllRequest.AppendLine("}");
        stringBuilderQueryGetAllRequest.AppendLine();
        stringBuilderQueryGetAllRequest.AppendLine($"return query{convertClassForSingle}GetAllResponses;");
        stringBuilderQueryGetAllRequest.AppendLine("}");
        stringBuilderQueryGetAllRequest.AppendLine("}");

        return stringBuilderQueryGetAllRequest.ToString();

    }
}
