using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateAPI;

public class GenerateAPIEndpoint
{
    public static string GenerateCodeAPIEndpoint(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderApiEndpoint = new StringBuilder();
        stringBuilderApiEndpoint.AppendLine("////// Camada da API > Controllers");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"using Application.Commands.{convertClassForSingle}.Delete;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Commands.{convertClassForSingle}.Create;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Commands.{convertClassForSingle}.Update;");
        stringBuilderApiEndpoint.AppendLine("using Application.Notification;");
        stringBuilderApiEndpoint.AppendLine("using Application.Queries;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderApiEndpoint.AppendLine("using MediatR;");
        stringBuilderApiEndpoint.AppendLine("using Microsoft.AspNetCore.Mvc;");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("namespace API.Controllers;");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"[Route(\"api/{ConvertToKebabCase(convertClassForSingle)}\")]");
        stringBuilderApiEndpoint.AppendLine($"public class {getEntities.Name}Controller : MainController");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("private readonly IMediator _mediator;");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"public {getEntities.Name}Controller(INotificationError notificationError, IMediator mediator) : base(notificationError)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine("_mediator = mediator;");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpGet(\"all\")]");
        stringBuilderApiEndpoint.AppendLine($"public async Task<ActionResult<IEnumerable<Query{convertClassForSingle}GetAllResponse>>> GetAll()");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine($"return Ok(await _mediator.Send(new Query{convertClassForSingle}GetAllRequest()));");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpGet(\"{id:int}\")]");
        stringBuilderApiEndpoint.AppendLine($"public async Task<ActionResult<Query{convertClassForSingle}GetByIdResponse>> GetById(int id)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine($"var getById{convertClassForSingle} = await _mediator.Send(new Query{convertClassForSingle}GetByIdRequest(id));");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"if(getById{convertClassForSingle} is null)");
        stringBuilderApiEndpoint.AppendLine($" return NotFound();");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($" return Ok(getById{convertClassForSingle});");
        stringBuilderApiEndpoint.AppendLine(" }");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpPost]");
        stringBuilderApiEndpoint.AppendLine($" public async Task<ActionResult> Post([FromBody] Create{convertClassForSingle}Request request)");
        stringBuilderApiEndpoint.AppendLine(" {");
        stringBuilderApiEndpoint.AppendLine(" if (!ModelState.IsValid) return CustomResponse(ModelState);");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("await _mediator.Send(request);");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("if (!ValidOperation()) return CustomResponse();");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("return Created();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpPut(\"{id:int}\")]");
        stringBuilderApiEndpoint.AppendLine($"public async Task<ActionResult> Put(int id, Update{convertClassForSingle}Request request)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine("if (id != request.Id)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine("NotifyError(\"Oops! We cannot process your request due to ID integrity errors.\");");
        stringBuilderApiEndpoint.AppendLine("return CustomResponse();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("if (!ModelState.IsValid) return CustomResponse(ModelState);");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("return Ok();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpDelete(\"{id:int}\")]");
        stringBuilderApiEndpoint.AppendLine("public async Task<ActionResult> Delete(int id)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine($"await _mediator.Send(new Delete{convertClassForSingle}Request(id));");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("if (!ValidOperation()) return CustomResponse();");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("return NoContent();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine("}");


        return stringBuilderApiEndpoint.ToString();

    }

    private static string ConvertToKebabCase(string input)
    {
        return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString())).ToLower();
    }
}
