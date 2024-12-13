using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationQueryGetAllHandler
{
    public static string GenerateCodeApplicationQueryGetAllHandler(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetAllHandler = new StringBuilder();

        stringBuilderQueryGetAllHandler.AppendLine("////// Camada Application > Queries > Pasta com o nome da sua entidade > Get All > Handler");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine("using Application.Notification;");
        stringBuilderQueryGetAllHandler.AppendLine("using Core.Abstract;");
        stringBuilderQueryGetAllHandler.AppendLine("using MediatR;");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"public class Query{convertClassForSingle}GetAllHandler : BaseCqrs, IRequestHandler<Query{convertClassForSingle}GetAllRequest, IEnumerable<Query{convertClassForSingle}GetAllResponse>>");
        stringBuilderQueryGetAllHandler.AppendLine("{");
        stringBuilderQueryGetAllHandler.AppendLine($"private readonly I{convertClassForSingle}Repository _repository;");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"public Query{convertClassForSingle}GetAllHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository) : base(notificationError)");
        stringBuilderQueryGetAllHandler.AppendLine("{");
        stringBuilderQueryGetAllHandler.AppendLine(" _repository = repository;");
        stringBuilderQueryGetAllHandler.AppendLine("}");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"public async Task<IEnumerable<Query{convertClassForSingle}GetAllResponse>> Handle(Query{convertClassForSingle}GetAllRequest request, CancellationToken cancellationToken)");
        stringBuilderQueryGetAllHandler.AppendLine("{");
        stringBuilderQueryGetAllHandler.AppendLine($"var get{getEntities.Name} = await _repository.GetAllAsync();");
        stringBuilderQueryGetAllHandler.AppendLine($"return request.ToResponse(get{getEntities.Name}.ToList());");
        stringBuilderQueryGetAllHandler.AppendLine("}");
        stringBuilderQueryGetAllHandler.AppendLine("}");

        return stringBuilderQueryGetAllHandler.ToString();
    }
}
