using System.Text;
using Application.Notification;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Identity.Client;

namespace Application.Queries.GenerateCode;

public class QueryGenerateCommandHandler : BaseCQRS, IRequestHandler<QueryGenerateCodeRequest, string>
{

    private readonly IEntityRepository _repository;
    
    public QueryGenerateCommandHandler(INotificationError notificationError, IMapper iMapper, IEntityRepository repository) : base(notificationError, iMapper)
    {
        _repository = repository;
    }

    public async Task<string> Handle(QueryGenerateCodeRequest request, CancellationToken cancellationToken)
    {
        var loadEntity = await _repository.GetAllPropertiesAsync(2);

        var stringBuilderEntity = new StringBuilder();

        stringBuilderEntity.AppendLine("namespace Core.Entities;");
        stringBuilderEntity.AppendLine();
        stringBuilderEntity.AppendLine($"public class {loadEntity.Name} : BaseEntity");
        stringBuilderEntity.AppendLine("{");
        stringBuilderEntity.AppendLine();

        // Construtor protegido
        stringBuilderEntity.AppendLine($"protected {loadEntity.Name}()");
        stringBuilderEntity.AppendLine("{ }");
        stringBuilderEntity.AppendLine();

        // Construtor público que aceita propriedades
        stringBuilderEntity.Append($"public {loadEntity.Name}(");
        stringBuilderEntity.Append(string.Join(", ", loadEntity.PropertyRel.Select(p => $"{p.Type} {p.Name.ToLower()}")));
        stringBuilderEntity.AppendLine(")");
        stringBuilderEntity.AppendLine("{");
        foreach (var item in loadEntity.PropertyRel)
        {
            stringBuilderEntity.AppendLine($"    {item.Name} = {item.Name.ToLower()};");
        }
        stringBuilderEntity.AppendLine("}");
        stringBuilderEntity.AppendLine();

        // Propriedades
        foreach (var item in loadEntity.PropertyRel)
        {
            stringBuilderEntity.AppendLine($"public {item.Type} {item.Name} {{ get; private set; }}");
        }

        stringBuilderEntity.AppendLine("}");
        
        return stringBuilderEntity.ToString();

        // return new QueryGenerateCodeResponse()
        // {
        //     CodeEntity = stringBuilderEntity.ToString()
        // };
    }
}