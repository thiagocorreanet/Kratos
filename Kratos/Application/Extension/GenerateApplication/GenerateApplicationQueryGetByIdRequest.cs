using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetByIdRequest
{
    public static string GenerateCodeApplicationQueryGetByIdRequest(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetByIdRequest = new StringBuilder();

        stringBuilderQueryGetByIdRequest.AppendLine("////// Camada Application > Queries > Pasta com o nome da sua entidade > Get By Id > Response");
        stringBuilderQueryGetByIdRequest.AppendLine();

        // Usings
        stringBuilderQueryGetByIdRequest.AppendLine("using MediatR;");
        stringBuilderQueryGetByIdRequest.AppendLine();

        // Namespace
        stringBuilderQueryGetByIdRequest.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderQueryGetByIdRequest.AppendLine();

        // Inicio da classe
        stringBuilderQueryGetByIdRequest.AppendLine($"public class Query{convertClassForSingle}GetByIdRequest : IRequest<Query{convertClassForSingle}GetByIdResponse>");
        stringBuilderQueryGetByIdRequest.AppendLine("{");
        stringBuilderQueryGetByIdRequest.AppendLine();

        // Criação do construtor
        stringBuilderQueryGetByIdRequest.AppendLine($" public Query{convertClassForSingle}GetByIdRequest(int id)");
        stringBuilderQueryGetByIdRequest.AppendLine("{");
        stringBuilderQueryGetByIdRequest.AppendLine("Id = id;");
        stringBuilderQueryGetByIdRequest.AppendLine("}");
        stringBuilderQueryGetByIdRequest.AppendLine();

        // Propriedade de entrada
        stringBuilderQueryGetByIdRequest.AppendLine(" public int Id { get; set; }");
        stringBuilderQueryGetByIdRequest.AppendLine();

        // Método de conversão DE/PARA 
        stringBuilderQueryGetByIdRequest.AppendLine($"public Query{convertClassForSingle}GetByIdResponse ToResponse(Core.Entities.{convertClassForSingle} entity)");
        stringBuilderQueryGetByIdRequest.AppendLine("{");
        stringBuilderQueryGetByIdRequest.AppendLine();

        stringBuilderQueryGetByIdRequest.AppendLine($"Query{convertClassForSingle}GetByIdResponse response = new Query{convertClassForSingle}GetByIdResponse();");
        stringBuilderQueryGetByIdRequest.AppendLine();

        stringBuilderQueryGetByIdRequest.AppendLine("response.Id = Id;");

        foreach (var item in getEntities.PropertyRel)
        {
            stringBuilderQueryGetByIdRequest.AppendLine($"response.{item.Name} = entity.{item.Name};");
        }

        stringBuilderQueryGetByIdRequest.AppendLine();
        stringBuilderQueryGetByIdRequest.AppendLine(" return response;");
        stringBuilderQueryGetByIdRequest.AppendLine();

        stringBuilderQueryGetByIdRequest.AppendLine("}");
        stringBuilderQueryGetByIdRequest.AppendLine("}");

        return stringBuilderQueryGetByIdRequest.ToString();
    }
}
