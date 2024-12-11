using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationCommandUpdate
{
    public static string GenerateCodeApplicationCommandUpdate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeCommandUpdate = new StringBuilder();
        string isRequired = string.Empty;


        stringBuildeCommandUpdate.AppendLine("////// Camada de application > Commands pasta com o nome da sua entidade > Pasta Update > Classe request");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("namespace Application.Commands.Entity.Update;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($" public class Update{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine(" public int Id {get; set;}");

        foreach (var property in getEntities.PropertyRel)
        {
            if (!property.IsRequired)
                isRequired = "?";
            else
                isRequired = "";

            stringBuildeCommandUpdate.AppendLine($" public {property.Type}{isRequired} {property.Name} {{get; set;}}");
        }

        stringBuildeCommandUpdate.AppendLine(" }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("////// Camada de application > Commands pasta com o nome da sua entidade > Pasta Update > Handler");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("using Application.Notification;");
        stringBuildeCommandUpdate.AppendLine("using AutoMapper;");
        stringBuildeCommandUpdate.AppendLine("using Core.Repositories;");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine("using Microsoft.Extensions.Logging;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"namespace Application.Commands.{convertClassForSingle}.Update;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public class Update{convertClassForSingle}CommandHandler : BaseCqrs, IRequestHandler<Update{convertClassForSingle}CommandRequest, bool>");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine($"private readonly I{convertClassForSingle}Repository _repository;");
        stringBuildeCommandUpdate.AppendLine($"private readonly ILogger<Update{convertClassForSingle}CommandHandler> _logger;");
        stringBuildeCommandUpdate.AppendLine();
        
        stringBuildeCommandUpdate.AppendLine($"public Update{convertClassForSingle}CommandHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository, ILogger<Update{convertClassForSingle}CommandHandler> logger) : base(notificationError)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine(" _repository = repository;");
        stringBuildeCommandUpdate.AppendLine(" _logger = logger;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();


        stringBuildeCommandUpdate.AppendLine($"public async Task<bool> Handle(Update{convertClassForSingle}CommandRequest request, CancellationToken cancellationToken)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("const bool transactionStared = true;");
        stringBuildeCommandUpdate.AppendLine("try");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine($" var getById{convertClassForSingle} = await _repository.GetByIdAsync(request.Id);");
        stringBuildeCommandUpdate.AppendLine($" if (getById{convertClassForSingle} is null)");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine(" Notify(message: \"Unable to locate the record.\");");
        stringBuildeCommandUpdate.AppendLine("  return false;");
        stringBuildeCommandUpdate.AppendLine("  }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("await _repository.StartTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine();


        stringBuildeCommandUpdate.AppendLine($"_repository.Update(request.ToEntity(request));");
        stringBuildeCommandUpdate.AppendLine("var result = await _repository.SaveChangesAsync();");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("if (!result)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine(" Notify(\"Oops! We couldn't save your record. Please try again.\");");
        stringBuildeCommandUpdate.AppendLine(" await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine(" return false;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine(" await _repository.CommitTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine(" }");
        stringBuildeCommandUpdate.AppendLine(" catch (Exception ex)");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine(" if (transactionStared) await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine(" _logger.LogCritical(\"Ops! We were unable to process your request.Details error: { ErrorMessage}\",  ex.Message);");
        stringBuildeCommandUpdate.AppendLine(" Notify(\"Oops! We were unable to process your request.\");");
        stringBuildeCommandUpdate.AppendLine(" }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("return true;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine("}");



        return stringBuildeCommandUpdate.ToString();
    }
}
