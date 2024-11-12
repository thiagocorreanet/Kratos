using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationCommandCreate
{
    public static string GenerateCodeApplicationCommandCreate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeCommand = new StringBuilder();


        stringBuildeCommand.AppendLine("////// Camada de application > Commands pasta com o nome da sua entidade > Pasta Create > Classe request");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("using MediatR;");
        stringBuildeCommand.AppendLine("namespace Application.Commands.Entity.Create;");
        stringBuildeCommand.AppendLine();
        stringBuildeCommand.AppendLine($"public class Create{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommand.AppendLine("{");

        foreach (var property in getEntities.PropertyRel)
        {
            stringBuildeCommand.AppendLine($" public {property.Type} {property.Name} {{get; set; }} = null!;");
        }

        stringBuildeCommand.AppendLine("}");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("////// Camada de application > Commands pasta com o nome da sua entidade > Pasta Create > Classe Handler");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("using Application.Notification;");
        stringBuildeCommand.AppendLine("using AutoMapper;");
        stringBuildeCommand.AppendLine("using Core.Repositories;");
        stringBuildeCommand.AppendLine("using MediatR;");
        stringBuildeCommand.AppendLine("using Microsoft.Extensions.Logging;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($"namespace Application.Commands.{convertClassForSingle}.Create;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($"public class Create{convertClassForSingle}CommandHandler : BaseCqrs, IRequestHandler<Create{convertClassForSingle}CommandRequest, bool>");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($"private readonly I{convertClassForSingle}Repository _repository;");
        stringBuildeCommand.AppendLine($"private ILogger<Create{convertClassForSingle}CommandHandler> _logger;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($"public Create{convertClassForSingle}CommandHandler(INotificationError notificationError, IMapper iMapper, I{convertClassForSingle}Repository repository, ILogger<Create{convertClassForSingle}CommandHandler> logger) : base(notificationError, iMapper)");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine("_repository = repository;");
        stringBuildeCommand.AppendLine("_logger = logger;");
        stringBuildeCommand.AppendLine("}");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($" public async Task<bool> Handle(Create{convertClassForSingle}CommandRequest request, CancellationToken cancellationToken)");
        stringBuildeCommand.AppendLine(" {");
        stringBuildeCommand.AppendLine(" const bool transactionStared = true;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("try");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("await _repository.StartTransactionAsync();");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($" _repository.Add(await SimpleMapping<Core.Entities.{convertClassForSingle}>(request));");
        stringBuildeCommand.AppendLine($"  var result = await _repository.SaveChangesAsync();");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("if (!result)");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine(" Notify(message: \"Oops! We couldn't save your record. Please try again.\");");
        stringBuildeCommand.AppendLine("  await _repository.RollbackTransactionAsync();");
        stringBuildeCommand.AppendLine("  return false;");
        stringBuildeCommand.AppendLine("  }");
        stringBuildeCommand.AppendLine("   await _repository.CommitTransactionAsync();");
        stringBuildeCommand.AppendLine("   }");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine(" catch (Exception ex)");
        stringBuildeCommand.AppendLine(" {");
        stringBuildeCommand.AppendLine(" if (transactionStared) await _repository.RollbackTransactionAsync();");
        stringBuildeCommand.AppendLine(" _logger.LogCritical(\"Ops! We were unable to process your request.Details error: { ErrorMessage}\",  ex.Message);");
        stringBuildeCommand.AppendLine(" Notify(\"Oops! We were unable to process your request.\");");
        stringBuildeCommand.AppendLine(" }");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("return true;");
        stringBuildeCommand.AppendLine("}");
        stringBuildeCommand.AppendLine("}");





        return stringBuildeCommand.ToString();
    }

}
