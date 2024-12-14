using Core.Entities;
using Humanizer;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationCommandDelete
{
    public static string GenerateCodeApplicationCommandDelete(string convertClassForSingle)
    {
        var stringBuildeCommandUpdate = new StringBuilder();


        stringBuildeCommandUpdate.AppendLine("////// Camda de application > Command > Delete Request");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("namespace Application.Commands.Entitie.Delete;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public class Delete{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine($"public Delete{convertClassForSingle}CommandRequest(int id)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine(" Id = id;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("public int Id { get; set; }");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("////// Camda de application > Command > Delete Handler");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("using Application.Notification;");
        stringBuildeCommandUpdate.AppendLine("using Core.Abstract;");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine("using Microsoft.Extensions.Logging;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"namespace Application.Commands.{convertClassForSingle}.Delete;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public class Delete{convertClassForSingle}CommandHandler : BaseCqrs, IRequestHandler<Delete{convertClassForSingle}CommandRequest, bool>");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine($" private readonly I{convertClassForSingle}Repository _repository;");
        stringBuildeCommandUpdate.AppendLine($" private readonly ILogger<Delete{convertClassForSingle}CommandHandler> _logger;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public Delete{convertClassForSingle}CommandHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository, ILogger<Delete{convertClassForSingle}CommandHandler> logger) : base(notificationError)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("  _repository = repository;");
        stringBuildeCommandUpdate.AppendLine("   _logger = logger;");
        stringBuildeCommandUpdate.AppendLine("   }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public async Task<bool> Handle(Delete{convertClassForSingle}CommandRequest request, CancellationToken cancellationToken)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("bool transactionStared = true;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($" var getById{convertClassForSingle} = await _repository.GetByIdAsync(request.Id);");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"if (getById{convertClassForSingle} is null)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("Notify(message: \"Unable to locate the record.\");");
        stringBuildeCommandUpdate.AppendLine("return false;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("try");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("await _repository.StartTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine($"_repository.Delete(getById{convertClassForSingle});");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("var result = await _repository.SaveChangesAsync();");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("if (!result)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine(" Notify(\"Oops! We couldn't save your record. Please try again.\");");
        stringBuildeCommandUpdate.AppendLine(" await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("  return false;");
        stringBuildeCommandUpdate.AppendLine("  }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("await _repository.CommitTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine(" catch (Exception ex)");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine("  if (transactionStared) await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("  _logger.LogCritical($\"ops! We were unable to process your request. Details error: {ex.Message}\");");
        stringBuildeCommandUpdate.AppendLine("  Notify(message: \"Oops! We were unable to process your request.\");");
        stringBuildeCommandUpdate.AppendLine("  }");
        stringBuildeCommandUpdate.AppendLine("  return true;");
        stringBuildeCommandUpdate.AppendLine("  }");
        stringBuildeCommandUpdate.AppendLine("  }");
       

       
        return stringBuildeCommandUpdate.ToString();

    }
}
