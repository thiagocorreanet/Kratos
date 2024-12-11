using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetByIdHandler
{
    public static string GenerateCodeApplicationQueryGetByIdHandler(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetByIdHandler = new StringBuilder();

        stringBuilderQueryGetByIdHandler.AppendLine("////// Camada Application > Queries > Pasta com o nome da sua entidade > Get By Id > Handler");
        stringBuilderQueryGetByIdHandler.AppendLine();

        stringBuilderQueryGetByIdHandler.AppendLine("using Application.Notification;");
        stringBuilderQueryGetByIdHandler.AppendLine("using Core.Abstract;");
        stringBuilderQueryGetByIdHandler.AppendLine("using MediatR;");
        stringBuilderQueryGetByIdHandler.AppendLine();

        stringBuilderQueryGetByIdHandler.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderQueryGetByIdHandler.AppendLine();

        stringBuilderQueryGetByIdHandler.AppendLine($"public class Query{convertClassForSingle}GetByIdHandler : BaseCqrs, IRequestHandler<Query{convertClassForSingle}GetByIdRequest, Query{convertClassForSingle}GetByIdResponse>");
        stringBuilderQueryGetByIdHandler.AppendLine("{");
        stringBuilderQueryGetByIdHandler.AppendLine();

        stringBuilderQueryGetByIdHandler.AppendLine($" private readonly I{convertClassForSingle}Repository _repository;");
        stringBuilderQueryGetByIdHandler.AppendLine();

        stringBuilderQueryGetByIdHandler.AppendLine($"public Query{convertClassForSingle}GetByIdHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository) : base(notificationError)");
        stringBuilderQueryGetByIdHandler.AppendLine("{");
        stringBuilderQueryGetByIdHandler.AppendLine("_repository = repository;");
        stringBuilderQueryGetByIdHandler.AppendLine("}");
        stringBuilderQueryGetByIdHandler.AppendLine();

        stringBuilderQueryGetByIdHandler.AppendLine($" public async Task<Query{convertClassForSingle}GetByIdResponse> Handle(Query{convertClassForSingle}GetByIdRequest request, CancellationToken cancellationToken)");
        stringBuilderQueryGetByIdHandler.AppendLine(" {");
        stringBuilderQueryGetByIdHandler.AppendLine($" var get{convertClassForSingle}ById = await _repository.GetByIdAsync(request.Id);");
        stringBuilderQueryGetByIdHandler.AppendLine($" return request.ToModel(get{convertClassForSingle}ById);");
        stringBuilderQueryGetByIdHandler.AppendLine(" }");
        stringBuilderQueryGetByIdHandler.AppendLine(" }");

      
        return stringBuilderQueryGetByIdHandler.ToString();
    }
}
