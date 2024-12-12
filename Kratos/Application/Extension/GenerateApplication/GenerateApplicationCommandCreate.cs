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
        stringBuildeCommand.AppendLine($"namespace Application.Commands.{convertClassForSingle}.Create;");
        stringBuildeCommand.AppendLine();
        stringBuildeCommand.AppendLine($"public class Create{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommand.AppendLine("{");

        // Adiciona as propriedades com a verificação para string
        foreach (var property in getEntities.PropertyRel.Where(x => x.IsRequired is true))
        {
            var nullSafety = property.Type.Equals("string", StringComparison.OrdinalIgnoreCase) ? " = null!;" : string.Empty;
            stringBuildeCommand.AppendLine($" public {property.Type} {property.Name} {{ get; set; }}{nullSafety}");
        }

        stringBuildeCommand.AppendLine();
        stringBuildeCommand.AppendLine($" public Core.Entities.{convertClassForSingle} ToEntity(Create{convertClassForSingle}CommandRequest request)");
        stringBuildeCommand.AppendLine(" {");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($"var toEntity = new Core.Entities.{convertClassForSingle}(");

        var requiredProperties = getEntities.PropertyRel.Where(x => x.IsRequired is true).ToList();

        for (int i = 0; i < requiredProperties.Count; i++)
        {
            var property = requiredProperties[i];
            var isLast = i == requiredProperties.Count - 1;

            stringBuildeCommand.AppendLine(isLast
                ? $" {property.Name} = request.{property.Name}"
                : $" {property.Name} = request.{property.Name},");
        }

        stringBuildeCommand.AppendLine(");");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("return toEntity;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("}");
        
        stringBuildeCommand.AppendLine("}");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("////// Camada de application > Commands pasta com o nome da sua entidade > Pasta Create > Classe Handler");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("using Application.Notification;");
        stringBuildeCommand.AppendLine("using Core.Abstract;");
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

        stringBuildeCommand.AppendLine($"public Create{convertClassForSingle}CommandHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository, ILogger<Create{convertClassForSingle}CommandHandler> logger) : base(notificationError)");
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

        stringBuildeCommand.AppendLine("  _repository.Add(request.ToEntity(request));");
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
